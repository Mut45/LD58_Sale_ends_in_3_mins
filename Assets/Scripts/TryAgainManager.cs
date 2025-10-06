using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// TryAgain功能管理器
/// 功能：
/// 1. 处理TryAgain按钮点击事件
/// 2. 切换到shop_scene场景
/// 3. 提供音效支持
/// </summary>
public class TryAgainManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button tryAgainButton;              // TryAgain按钮
    
    [Header("场景设置")]
    [SerializeField] private string shopSceneName = "shop_scene"; // 商店场景名称
    
    [Header("音效设置")]
    [SerializeField] private bool playClickSound = true;        // 是否播放点击音效
    [SerializeField] private bool playSuccessSound = true;      // 是否播放成功音效
    
    [Header("切换设置")]
    [SerializeField] private float switchDelay = 0.5f;          // 切换延迟时间
    [SerializeField] private bool showLoadingScreen = false;   // 是否显示加载画面
    
    private void Start()
    {
        InitializeTryAgainButton();
    }
    
    /// <summary>
    /// 初始化TryAgain按钮
    /// </summary>
    private void InitializeTryAgainButton()
    {
        // 如果没有手动设置按钮，尝试自动查找
        if (tryAgainButton == null)
        {
            tryAgainButton = GetComponent<Button>();
            if (tryAgainButton == null)
            {
                // 尝试查找名为"TryAgain"的子对象
                Transform tryAgainTransform = transform.Find("TryAgain");
                if (tryAgainTransform != null)
                {
                    tryAgainButton = tryAgainTransform.GetComponent<Button>();
                }
            }
        }
        
        // 设置按钮点击事件
        if (tryAgainButton != null)
        {
            tryAgainButton.onClick.RemoveAllListeners();
            tryAgainButton.onClick.AddListener(OnTryAgainClicked);
            Debug.Log("TryAgain按钮已初始化");
        }
        else
        {
            Debug.LogWarning("TryAgain按钮未找到，请手动设置");
        }
    }
    
    /// <summary>
    /// TryAgain按钮点击事件处理
    /// </summary>
    public void OnTryAgainClicked()
    {
        Debug.Log("TryAgain按钮被点击");
        
        // 播放点击音效
        if (playClickSound)
        {
            PlayClickSound();
        }
        
        // 延迟切换场景
        Invoke(nameof(SwitchToShopScene), switchDelay);
    }
    
    /// <summary>
    /// 切换到商店场景
    /// </summary>
    private void SwitchToShopScene()
    {
        Debug.Log($"准备切换到场景: {shopSceneName}");
        
        try
        {
            // 播放成功音效
            if (playSuccessSound)
            {
                PlaySuccessSound();
            }
            
            // 显示加载画面（如果启用）
            if (showLoadingScreen)
            {
                ShowLoadingScreen();
            }
            
            // 切换场景
            SceneManager.LoadScene(shopSceneName);
            Debug.Log($"成功切换到场景: {shopSceneName}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"切换场景失败: {e.Message}");
            Debug.LogError($"请确保场景 '{shopSceneName}' 已添加到Build Settings中");
        }
    }
    
    /// <summary>
    /// 播放点击音效
    /// </summary>
    private void PlayClickSound()
    {
        if (ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlayClickedSound();
        }
        else
        {
            Debug.LogWarning("ShoppingAudioManager未找到，无法播放点击音效");
        }
    }
    
    /// <summary>
    /// 播放成功音效
    /// </summary>
    private void PlaySuccessSound()
    {
        if (ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlaySuccessSound();
        }
        else
        {
            Debug.LogWarning("ShoppingAudioManager未找到，无法播放成功音效");
        }
    }
    
    /// <summary>
    /// 显示加载画面
    /// </summary>
    private void ShowLoadingScreen()
    {
        // 这里可以添加加载画面的显示逻辑
        Debug.Log("显示加载画面");
    }
    
    #region 公共接口
    
    /// <summary>
    /// 手动触发TryAgain功能
    /// </summary>
    public void TriggerTryAgain()
    {
        OnTryAgainClicked();
    }
    
    /// <summary>
    /// 设置商店场景名称
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    public void SetShopSceneName(string sceneName)
    {
        shopSceneName = sceneName;
        Debug.Log($"设置商店场景名称为: {sceneName}");
    }
    
    /// <summary>
    /// 设置切换延迟时间
    /// </summary>
    /// <param name="delay">延迟时间（秒）</param>
    public void SetSwitchDelay(float delay)
    {
        switchDelay = Mathf.Max(0f, delay);
        Debug.Log($"设置切换延迟时间为: {switchDelay}秒");
    }
    
    /// <summary>
    /// 启用/禁用点击音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetClickSoundEnabled(bool enable)
    {
        playClickSound = enable;
        Debug.Log($"点击音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用成功音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetSuccessSoundEnabled(bool enable)
    {
        playSuccessSound = enable;
        Debug.Log($"成功音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用加载画面
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetLoadingScreenEnabled(bool enable)
    {
        showLoadingScreen = enable;
        Debug.Log($"加载画面: {(enable ? "启用" : "禁用")}");
    }
    
    #endregion
    
    #region 调试和测试方法
    
    /// <summary>
    /// 测试TryAgain功能
    /// </summary>
    [ContextMenu("测试TryAgain功能")]
    public void TestTryAgain()
    {
        Debug.Log("开始测试TryAgain功能...");
        OnTryAgainClicked();
    }
    
    /// <summary>
    /// 检查场景是否存在
    /// </summary>
    [ContextMenu("检查场景是否存在")]
    public void CheckSceneExists()
    {
        bool sceneExists = false;
        
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            
            if (sceneName == shopSceneName)
            {
                sceneExists = true;
                break;
            }
        }
        
        if (sceneExists)
        {
            Debug.Log($"场景 '{shopSceneName}' 存在于Build Settings中");
        }
        else
        {
            Debug.LogError($"场景 '{shopSceneName}' 不存在于Build Settings中！");
            Debug.LogError("请确保场景已添加到File > Build Settings > Scenes In Build中");
        }
    }
    
    /// <summary>
    /// 打印当前设置状态
    /// </summary>
    [ContextMenu("打印设置状态")]
    public void PrintSettings()
    {
        Debug.Log("=== TryAgain管理器设置状态 ===");
        Debug.Log($"TryAgain按钮: {(tryAgainButton != null ? "已设置" : "未设置")}");
        Debug.Log($"商店场景名称: {shopSceneName}");
        Debug.Log($"切换延迟时间: {switchDelay}秒");
        Debug.Log($"点击音效: {(playClickSound ? "启用" : "禁用")}");
        Debug.Log($"成功音效: {(playSuccessSound ? "启用" : "禁用")}");
        Debug.Log($"加载画面: {(showLoadingScreen ? "启用" : "禁用")}");
        Debug.Log($"音效管理器状态: {(ShoppingAudioManager.Instance != null ? "正常" : "未找到")}");
        Debug.Log("================================");
    }
    
    #endregion
    
    private void OnValidate()
    {
        // 在编辑器中实时更新设置
        if (Application.isPlaying)
        {
            InitializeTryAgainButton();
        }
    }
}
