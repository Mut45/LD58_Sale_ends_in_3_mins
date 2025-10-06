using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Steam风格管理器 - 用于管理整个项目的Steam风格样式
/// 将这个脚本添加到您的Canvas或场景管理器上
/// </summary>
public class SteamStyleManager : MonoBehaviour
{
    [Header("自动应用设置")]
    [SerializeField] private bool autoApplyOnStart = true;
    [SerializeField] private bool applyToAllCanvas = true;
    
    [Header("手动指定对象")]
    [SerializeField] private Canvas[] targetCanvas;
    [SerializeField] private GameObject[] gameCards;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Text[] texts;
    [SerializeField] private Image[] images;
    
    [Header("样式设置")]
    [SerializeField] private bool enableHoverEffects = true;
    [SerializeField] private SteamButtonType defaultButtonType = SteamButtonType.Blue;
    [SerializeField] private SteamTextType defaultTextType = SteamTextType.Primary;
    
    void Start()
    {
        if (autoApplyOnStart)
        {
            ApplySteamStyles();
        }
    }
    
    /// <summary>
    /// 应用Steam风格到所有指定对象
    /// </summary>
    [ContextMenu("应用Steam风格")]
    public void ApplySteamStyles()
    {
        Debug.Log("开始应用Steam风格...");
        
        if (applyToAllCanvas)
        {
            // 应用样式到所有Canvas
            Canvas[] allCanvas = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in allCanvas)
            {
                SteamStyleForTraditionalUI.ApplySteamStyleToCanvas(canvas);
                Debug.Log($"已应用Steam风格到Canvas: {canvas.name}");
            }
        }
        else
        {
            // 只应用样式到指定的Canvas
            foreach (Canvas canvas in targetCanvas)
            {
                if (canvas != null)
                {
                    SteamStyleForTraditionalUI.ApplySteamStyleToCanvas(canvas);
                    Debug.Log($"已应用Steam风格到指定Canvas: {canvas.name}");
                }
            }
        }
        
        // 应用样式到游戏卡片
        foreach (GameObject card in gameCards)
        {
            if (card != null)
            {
                SteamStyleForTraditionalUI.ApplySteamCardStyle(card, enableHoverEffects);
                Debug.Log($"已应用Steam风格到游戏卡片: {card.name}");
            }
        }
        
        // 应用样式到按钮
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                SteamStyleForTraditionalUI.ApplySteamButtonStyle(button, defaultButtonType);
                Debug.Log($"已应用Steam风格到按钮: {button.name}");
            }
        }
        
        // 应用样式到文本
        foreach (Text text in texts)
        {
            if (text != null)
            {
                SteamStyleForTraditionalUI.ApplySteamTextStyle(text, defaultTextType);
                Debug.Log($"已应用Steam风格到文本: {text.name}");
            }
        }
        
        // 应用样式到图片
        foreach (Image image in images)
        {
            if (image != null)
            {
                SteamStyleForTraditionalUI.ApplySteamImageStyle(image, SteamImageType.Panel);
                Debug.Log($"已应用Steam风格到图片: {image.name}");
            }
        }
        
        Debug.Log("Steam风格应用完成！");
    }
    
    /// <summary>
    /// 清除所有Steam风格（恢复默认）
    /// </summary>
    [ContextMenu("清除Steam风格")]
    public void ClearSteamStyles()
    {
        Debug.Log("清除Steam风格...");
        
        // 清除所有Canvas的样式
        Canvas[] allCanvas = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvas)
        {
            ClearCanvasStyles(canvas);
        }
        
        Debug.Log("Steam风格已清除！");
    }
    
    /// <summary>
    /// 清除Canvas样式
    /// </summary>
    private void ClearCanvasStyles(Canvas canvas)
    {
        // 恢复默认颜色
        Image canvasBackground = canvas.GetComponent<Image>();
        if (canvasBackground != null)
        {
            canvasBackground.color = Color.white;
        }
        
        // 递归清除所有子对象样式
        ClearStylesRecursively(canvas.gameObject);
    }
    
    /// <summary>
    /// 递归清除样式
    /// </summary>
    private void ClearStylesRecursively(GameObject obj)
    {
        Image image = obj.GetComponent<Image>();
        Text text = obj.GetComponent<Text>();
        Button button = obj.GetComponent<Button>();
        
        if (image != null)
        {
            image.color = Color.white;
        }
        
        if (text != null)
        {
            text.color = Color.black;
        }
        
        if (button != null)
        {
            // 恢复按钮默认颜色
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
            colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            button.colors = colors;
        }
        
        // 移除悬停效果组件
        SteamCardHoverEffect hoverEffect = obj.GetComponent<SteamCardHoverEffect>();
        if (hoverEffect != null)
        {
            DestroyImmediate(hoverEffect);
        }
        
        // 递归处理子对象
        foreach (Transform child in obj.transform)
        {
            ClearStylesRecursively(child.gameObject);
        }
    }
    
    /// <summary>
    /// 动态添加Steam风格到新创建的对象
    /// </summary>
    public static void ApplySteamStyleToNewObject(GameObject newObject)
    {
        if (newObject == null) return;
        
        // 检查对象类型并应用相应样式
        Button button = newObject.GetComponent<Button>();
        Text text = newObject.GetComponent<Text>();
        Image image = newObject.GetComponent<Image>();
        
        if (button != null)
        {
            SteamStyleForTraditionalUI.ApplySteamButtonStyle(button);
        }
        
        if (text != null)
        {
            SteamStyleForTraditionalUI.ApplySteamTextStyle(text);
        }
        
        if (image != null && !newObject.name.ToLower().Contains("icon"))
        {
            SteamStyleForTraditionalUI.ApplySteamImageStyle(image);
        }
        
        Debug.Log($"已为新对象应用Steam风格: {newObject.name}");
    }
    
    /// <summary>
    /// 在编辑器中预览Steam风格
    /// </summary>
    [ContextMenu("预览Steam风格")]
    public void PreviewSteamStyles()
    {
        #if UNITY_EDITOR
        Debug.Log("预览Steam风格效果...");
        
        // 在编辑器中应用样式
        ApplySteamStyles();
        
        // 标记场景为已修改
        UnityEditor.EditorUtility.SetDirty(gameObject);
        #endif
    }
}

