using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// LibraryItem颜色提取器和透明度控制器
/// 功能：
/// 1. 自动从游戏图标提取颜色并应用到背景
/// 2. 提供接口控制playtime相关文本的透明度
/// </summary>
public class LibraryItemColorExtractor : MonoBehaviour
{
    [Header("颜色提取设置")]
    [SerializeField] private Image backgroundImage;           // 背景图片组件
    [SerializeField] private Image iconImage;                 // 图标图片组件
    [SerializeField] private float colorExtractionDelay = 0.1f; // 颜色提取延迟（确保图标已加载）
    [SerializeField] private float colorIntensity = 0.3f;    // 颜色强度（0-1，值越小背景越暗）
    [SerializeField] private bool enableColorExtraction = true; // 是否启用颜色提取
    
    [Header("透明度控制")]
    [SerializeField] private Text playtimeText;              // Playtime文本
    [SerializeField] private Text recentPlaytimeText;        // RecentPlaytime文本  
    [SerializeField] private Text totalPlaytimeText;         // TotalPlaytime文本
    
    [Header("默认透明度设置")]
    [Range(0f, 1f)]
    [SerializeField] private float defaultPlaytimeAlpha = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float defaultRecentPlaytimeAlpha = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float defaultTotalPlaytimeAlpha = 1f;
    
    private Color originalBackgroundColor;
    private bool isInitialized = false;
    
    private void Start()
    {
        InitializeComponents();
        StartCoroutine(ExtractColorAfterDelay());
    }
    
    private void InitializeComponents()
    {
        // 如果没有手动设置背景图片，尝试自动查找
        if (backgroundImage == null)
        {
            backgroundImage = GetComponent<Image>();
            if (backgroundImage == null)
            {
                // 尝试查找名为"Background"的子对象
                Transform backgroundTransform = transform.Find("Background");
                if (backgroundTransform != null)
                {
                    backgroundImage = backgroundTransform.GetComponent<Image>();
                }
            }
        }
        
        // 如果没有手动设置图标图片，尝试自动查找
        if (iconImage == null)
        {
            // 尝试查找名为"GameIcon"或"Icon"的子对象
            Transform iconTransform = transform.Find("GameIcon");
            if (iconTransform == null)
            {
                iconTransform = transform.Find("Icon");
            }
            if (iconTransform != null)
            {
                iconImage = iconTransform.GetComponent<Image>();
            }
        }
        
        // 如果没有手动设置playtime文本，尝试自动查找
        if (playtimeText == null)
        {
            Transform playtimeTransform = transform.Find("Playtime");
            if (playtimeTransform != null)
            {
                playtimeText = playtimeTransform.GetComponent<Text>();
            }
        }
        
        if (recentPlaytimeText == null)
        {
            Transform recentPlaytimeTransform = transform.Find("RecentPlaytime");
            if (recentPlaytimeTransform != null)
            {
                recentPlaytimeText = recentPlaytimeTransform.GetComponent<Text>();
            }
        }
        
        if (totalPlaytimeText == null)
        {
            Transform totalPlaytimeTransform = transform.Find("TotalPlaytime");
            if (totalPlaytimeTransform != null)
            {
                totalPlaytimeText = totalPlaytimeTransform.GetComponent<Text>();
            }
        }
        
        // 保存原始背景颜色
        if (backgroundImage != null)
        {
            originalBackgroundColor = backgroundImage.color;
        }
        
        // 设置默认透明度
        SetPlaytimeAlpha(defaultPlaytimeAlpha);
        SetRecentPlaytimeAlpha(defaultRecentPlaytimeAlpha);
        SetTotalPlaytimeAlpha(defaultTotalPlaytimeAlpha);
        
        isInitialized = true;
    }
    
    private IEnumerator ExtractColorAfterDelay()
    {
        yield return new WaitForSeconds(colorExtractionDelay);
        ExtractColorFromIcon();
    }
    
    /// <summary>
    /// 从图标提取颜色并应用到背景
    /// </summary>
    private void ExtractColorFromIcon()
    {
        if (!enableColorExtraction || backgroundImage == null || iconImage == null)
        {
            return;
        }
        
        if (iconImage.sprite == null)
        {
            Debug.LogWarning("Icon sprite is null, cannot extract color");
            return;
        }
        
        // 从图标的sprite提取主要颜色
        Color extractedColor = ExtractDominantColor(iconImage.sprite);
        
        // 应用颜色到背景，使用指定的强度
        Color backgroundColor = new Color(
            extractedColor.r * colorIntensity,
            extractedColor.g * colorIntensity,
            extractedColor.b * colorIntensity,
            originalBackgroundColor.a
        );
        
        backgroundImage.color = backgroundColor;
        
        Debug.Log($"Extracted color from icon: {extractedColor}, Applied to background: {backgroundColor}");
    }
    
    /// <summary>
    /// 从sprite提取主要颜色
    /// </summary>
    private Color ExtractDominantColor(Sprite sprite)
    {
        if (sprite == null || sprite.texture == null)
        {
            return Color.gray;
        }
        
        Texture2D texture = sprite.texture;
        Color[] pixels = texture.GetPixels();
        
        if (pixels.Length == 0)
        {
            return Color.gray;
        }
        
        // 计算平均颜色
        float r = 0, g = 0, b = 0;
        int validPixels = 0;
        
        for (int i = 0; i < pixels.Length; i++)
        {
            Color pixel = pixels[i];
            // 跳过完全透明的像素
            if (pixel.a > 0.1f)
            {
                r += pixel.r;
                g += pixel.g;
                b += pixel.b;
                validPixels++;
            }
        }
        
        if (validPixels > 0)
        {
            r /= validPixels;
            g /= validPixels;
            b /= validPixels;
        }
        else
        {
            return Color.gray;
        }
        
        return new Color(r, g, b, 1f);
    }
    
    #region 透明度控制接口
    
    /// <summary>
    /// 设置Playtime文本的透明度
    /// </summary>
    /// <param name="alpha">透明度值 (0-1)</param>
    public void SetPlaytimeAlpha(float alpha)
    {
        if (playtimeText != null)
        {
            Color color = playtimeText.color;
            color.a = Mathf.Clamp01(alpha);
            playtimeText.color = color;
        }
    }
    
    /// <summary>
    /// 设置RecentPlaytime文本的透明度
    /// </summary>
    /// <param name="alpha">透明度值 (0-1)</param>
    public void SetRecentPlaytimeAlpha(float alpha)
    {
        if (recentPlaytimeText != null)
        {
            Color color = recentPlaytimeText.color;
            color.a = Mathf.Clamp01(alpha);
            recentPlaytimeText.color = color;
        }
    }
    
    /// <summary>
    /// 设置TotalPlaytime文本的透明度
    /// </summary>
    /// <param name="alpha">透明度值 (0-1)</param>
    public void SetTotalPlaytimeAlpha(float alpha)
    {
        if (totalPlaytimeText != null)
        {
            Color color = totalPlaytimeText.color;
            color.a = Mathf.Clamp01(alpha);
            totalPlaytimeText.color = color;
        }
    }
    
    /// <summary>
    /// 同时设置所有playtime文本的透明度
    /// </summary>
    /// <param name="alpha">透明度值 (0-1)</param>
    public void SetAllPlaytimeAlpha(float alpha)
    {
        SetPlaytimeAlpha(alpha);
        SetRecentPlaytimeAlpha(alpha);
        SetTotalPlaytimeAlpha(alpha);
    }
    
    #endregion
    
    #region 颜色提取控制接口
    
    /// <summary>
    /// 启用/禁用颜色提取功能
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetColorExtractionEnabled(bool enable)
    {
        enableColorExtraction = enable;
        if (!enable && backgroundImage != null)
        {
            // 如果禁用，恢复原始背景颜色
            backgroundImage.color = originalBackgroundColor;
        }
        else if (enable)
        {
            // 如果启用，重新提取颜色
            ExtractColorFromIcon();
        }
    }
    
    /// <summary>
    /// 设置颜色强度
    /// </summary>
    /// <param name="intensity">颜色强度 (0-1)</param>
    public void SetColorIntensity(float intensity)
    {
        colorIntensity = Mathf.Clamp01(intensity);
        if (enableColorExtraction)
        {
            ExtractColorFromIcon();
        }
    }
    
    /// <summary>
    /// 手动触发颜色提取
    /// </summary>
    public void RefreshColorExtraction()
    {
        ExtractColorFromIcon();
    }
    
    #endregion
    
    #region 获取当前状态
    
    /// <summary>
    /// 获取当前Playtime文本的透明度
    /// </summary>
    public float GetPlaytimeAlpha()
    {
        return playtimeText != null ? playtimeText.color.a : 0f;
    }
    
    /// <summary>
    /// 获取当前RecentPlaytime文本的透明度
    /// </summary>
    public float GetRecentPlaytimeAlpha()
    {
        return recentPlaytimeText != null ? recentPlaytimeText.color.a : 0f;
    }
    
    /// <summary>
    /// 获取当前TotalPlaytime文本的透明度
    /// </summary>
    public float GetTotalPlaytimeAlpha()
    {
        return totalPlaytimeText != null ? totalPlaytimeText.color.a : 0f;
    }
    
    /// <summary>
    /// 获取当前背景颜色
    /// </summary>
    public Color GetBackgroundColor()
    {
        return backgroundImage != null ? backgroundImage.color : Color.clear;
    }
    
    #endregion
    
    private void OnValidate()
    {
        // 在编辑器中实时更新透明度
        if (Application.isPlaying && isInitialized)
        {
            SetPlaytimeAlpha(defaultPlaytimeAlpha);
            SetRecentPlaytimeAlpha(defaultRecentPlaytimeAlpha);
            SetTotalPlaytimeAlpha(defaultTotalPlaytimeAlpha);
        }
    }
}
