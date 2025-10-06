using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 为传统Unity UI系统提供Steam风格的样式设置
/// 适用于Canvas + Image + Text + Button的UI系统
/// </summary>
public static class SteamStyleForTraditionalUI
{
    // Steam颜色定义
    public static class Colors
    {
        // Steam品牌色
        public static readonly Color Blue = new Color(0.102f, 0.624f, 1f, 1f);           // #1A9FFF
        public static readonly Color BlueHi = new Color(0f, 0.733f, 1f, 1f);            // #00BBFF
        public static readonly Color Green = new Color(0.357f, 0.639f, 0.169f, 1f);    // #5ba32b
        public static readonly Color GreenHi = new Color(0.349f, 0.749f, 0.251f, 1f);  // #59BF40
        public static readonly Color Red = new Color(0.851f, 0.255f, 0.149f, 1f);      // #D94126
        public static readonly Color RedHi = new Color(0.933f, 0.337f, 0.231f, 1f);    // #EE563B
        
        // Steam系统灰色
        public static readonly Color SystemDarkestGrey = new Color(0.055f, 0.078f, 0.106f, 1f);  // #0E141B
        public static readonly Color SystemDarkerGrey = new Color(0.137f, 0.149f, 0.180f, 1f);   // #23262E
        public static readonly Color SystemDarkGrey = new Color(0.239f, 0.267f, 0.314f, 1f);     // #3D4450
        public static readonly Color SystemGrey = new Color(0.404f, 0.439f, 0.482f, 1f);         // #67707B
        public static readonly Color SystemLightGrey = new Color(0.545f, 0.573f, 0.604f, 1f);    // #8B929A
        
        // Steam商店色
        public static readonly Color StoreDarkestGrey = new Color(0f, 0.059f, 0.094f, 1f);       // #000F18
        public static readonly Color StoreDarkerGrey = new Color(0.106f, 0.157f, 0.235f, 1f);    // #1B2838
        public static readonly Color StoreDarkGrey = new Color(0.165f, 0.278f, 0.369f, 1f);      // #2A475E
        
        // 文本颜色
        public static readonly Color LightFg = new Color(1f, 1f, 1f, 0.8f);                      // rgba(255, 255, 255, 0.8)
        public static readonly Color TextPrimary = new Color(1f, 1f, 1f, 0.9f);
        public static readonly Color TextSecondary = new Color(0.545f, 0.573f, 0.604f, 1f);
        public static readonly Color TextMuted = new Color(0.404f, 0.439f, 0.482f, 1f);
    }
    
    /// <summary>
    /// 应用Steam风格到按钮
    /// </summary>
    public static void ApplySteamButtonStyle(Button button, SteamButtonType buttonType = SteamButtonType.Blue)
    {
        if (button == null) return;
        
        // 设置按钮颜色
        ColorBlock colors = button.colors;
        switch (buttonType)
        {
            case SteamButtonType.Blue:
                colors.normalColor = Colors.Blue;
                colors.highlightedColor = Colors.BlueHi;
                colors.pressedColor = new Color(Colors.Blue.r * 0.8f, Colors.Blue.g * 0.8f, Colors.Blue.b * 0.8f, 1f);
                break;
            case SteamButtonType.Green:
                colors.normalColor = Colors.Green;
                colors.highlightedColor = Colors.GreenHi;
                colors.pressedColor = new Color(Colors.Green.r * 0.8f, Colors.Green.g * 0.8f, Colors.Green.b * 0.8f, 1f);
                break;
            case SteamButtonType.Red:
                colors.normalColor = Colors.Red;
                colors.highlightedColor = Colors.RedHi;
                colors.pressedColor = new Color(Colors.Red.r * 0.8f, Colors.Red.g * 0.8f, Colors.Red.b * 0.8f, 1f);
                break;
        }
        
        colors.colorMultiplier = 1f;
        colors.fadeDuration = 0.1f;
        button.colors = colors;
        
        // 设置文本颜色
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.color = Color.white;
            buttonText.fontSize = 14;
        }
    }
    
    /// <summary>
    /// 应用Steam风格到文本
    /// </summary>
    public static void ApplySteamTextStyle(Text text, SteamTextType textType = SteamTextType.Primary)
    {
        if (text == null) return;
        
        switch (textType)
        {
            case SteamTextType.Primary:
                text.color = Colors.TextPrimary;
                text.fontSize = 14;
                break;
            case SteamTextType.Secondary:
                text.color = Colors.TextSecondary;
                text.fontSize = 12;
                break;
            case SteamTextType.Muted:
                text.color = Colors.TextMuted;
                text.fontSize = 12;
                break;
        }
    }
    
    /// <summary>
    /// 应用Steam风格到图片背景
    /// </summary>
    public static void ApplySteamImageStyle(Image image, SteamImageType imageType = SteamImageType.Card)
    {
        if (image == null) return;
        
        switch (imageType)
        {
            case SteamImageType.Card:
                image.color = Colors.StoreDarkGrey;
                break;
            case SteamImageType.Panel:
                image.color = Colors.SystemDarkGrey;
                break;
            case SteamImageType.Background:
                image.color = Colors.StoreDarkestGrey;
                break;
        }
    }
    
    /// <summary>
    /// 批量应用Steam风格到游戏卡片
    /// </summary>
    public static void ApplySteamCardStyle(GameObject cardObject, bool hasHoverEffect = true)
    {
        if (cardObject == null) return;
        
        // 设置卡片背景
        Image cardBackground = cardObject.GetComponent<Image>();
        if (cardBackground != null)
        {
            ApplySteamImageStyle(cardBackground, SteamImageType.Card);
        }
        
        // 设置所有文本样式
        Text[] texts = cardObject.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            // 根据文本名称或内容判断类型
            if (text.name.ToLower().Contains("title") || text.name.ToLower().Contains("name"))
            {
                ApplySteamTextStyle(text, SteamTextType.Primary);
            }
            else if (text.name.ToLower().Contains("price"))
            {
                ApplySteamTextStyle(text, SteamTextType.Primary);
                text.fontSize = 16;
            }
            else if (text.name.ToLower().Contains("description") || text.name.ToLower().Contains("dev"))
            {
                ApplySteamTextStyle(text, SteamTextType.Secondary);
            }
            else
            {
                ApplySteamTextStyle(text, SteamTextType.Muted);
            }
        }
        
        // 设置所有按钮样式
        Button[] buttons = cardObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.name.ToLower().Contains("purchase") || button.name.ToLower().Contains("buy"))
            {
                ApplySteamButtonStyle(button, SteamButtonType.Blue);
            }
            else if (button.name.ToLower().Contains("remove") || button.name.ToLower().Contains("delete"))
            {
                ApplySteamButtonStyle(button, SteamButtonType.Red);
            }
            else if (button.name.ToLower().Contains("add") || button.name.ToLower().Contains("cart"))
            {
                ApplySteamButtonStyle(button, SteamButtonType.Green);
            }
            else
            {
                ApplySteamButtonStyle(button, SteamButtonType.Blue);
            }
        }
        
        // 添加悬停效果组件（如果需要）
        if (hasHoverEffect && cardObject.GetComponent<SteamCardHoverEffect>() == null)
        {
            cardObject.AddComponent<SteamCardHoverEffect>();
        }
    }
    
    /// <summary>
    /// 批量应用Steam风格到整个Canvas
    /// </summary>
    public static void ApplySteamStyleToCanvas(Canvas canvas)
    {
        if (canvas == null) return;
        
        // 设置Canvas背景
        Image canvasBackground = canvas.GetComponent<Image>();
        if (canvasBackground != null)
        {
            ApplySteamImageStyle(canvasBackground, SteamImageType.Background);
        }
        
        // 递归应用样式到所有子对象
        ApplySteamStyleRecursively(canvas.gameObject);
    }
    
    /// <summary>
    /// 递归应用Steam风格
    /// </summary>
    private static void ApplySteamStyleRecursively(GameObject obj)
    {
        // 处理当前对象的组件
        Image image = obj.GetComponent<Image>();
        Text text = obj.GetComponent<Text>();
        Button button = obj.GetComponent<Button>();
        
        if (image != null && !obj.name.ToLower().Contains("icon"))
        {
            ApplySteamImageStyle(image, SteamImageType.Panel);
        }
        
        if (text != null)
        {
            ApplySteamTextStyle(text, SteamTextType.Primary);
        }
        
        if (button != null)
        {
            ApplySteamButtonStyle(button, SteamButtonType.Blue);
        }
        
        // 递归处理子对象
        foreach (Transform child in obj.transform)
        {
            ApplySteamStyleRecursively(child.gameObject);
        }
    }
}

/// <summary>
/// Steam按钮类型枚举
/// </summary>
public enum SteamButtonType
{
    Blue,
    Green,
    Red
}

/// <summary>
/// Steam文本类型枚举
/// </summary>
public enum SteamTextType
{
    Primary,
    Secondary,
    Muted
}

/// <summary>
/// Steam图片类型枚举
/// </summary>
public enum SteamImageType
{
    Card,
    Panel,
    Background
}

/// <summary>
/// Steam卡片悬停效果组件
/// </summary>
public class SteamCardHoverEffect : MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    private Image cardImage;
    private Color originalColor;
    private Color hoverColor;
    
    void Start()
    {
        cardImage = GetComponent<Image>();
        if (cardImage != null)
        {
            originalColor = cardImage.color;
            hoverColor = new Color(originalColor.r * 1.2f, originalColor.g * 1.2f, originalColor.b * 1.2f, originalColor.a);
        }
    }
    
    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (cardImage != null)
        {
            cardImage.color = hoverColor;
        }
    }
    
    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (cardImage != null)
        {
            cardImage.color = originalColor;
        }
    }
}

