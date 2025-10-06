using UnityEngine;

/// <summary>
/// 购物音效管理器
/// 功能：
/// 1. 管理购物车和购买相关的音效播放
/// 2. 提供简单的接口来设置和播放音效
/// </summary>
public class ShoppingAudioManager : MonoBehaviour
{
    [Header("音效设置")]
    [SerializeField] private AudioClip addToCartSound;        // 加入购物车音效
    [SerializeField] private AudioClip purchaseSound;         // 直接购买音效
    [SerializeField] private AudioClip cartPurchaseSound;     // 购物车购买音效
    [SerializeField] private AudioClip clickedSound;          // 点击音效
    [SerializeField] private AudioClip successSound;          // 成功音效
    [SerializeField] private AudioClip winSound;             // 胜利音效
    [SerializeField] private AudioClip failSound;            // 失败音效
    
    [Header("音频源设置")]
    [SerializeField] private AudioSource audioSource;        // 音频播放源
    [SerializeField] private float volume = 1f;              // 音量大小
    [SerializeField] private bool playOnAwake = false;      // 是否在开始时播放
    
    private static ShoppingAudioManager instance;
    
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ShoppingAudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShoppingAudioManager>();
                if (instance == null)
                {
                    GameObject audioManagerObject = new GameObject("ShoppingAudioManager");
                    instance = audioManagerObject.AddComponent<ShoppingAudioManager>();
                    DontDestroyOnLoad(audioManagerObject);
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        // 确保只有一个实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSource();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // 如果还没有初始化，在这里初始化
        if (audioSource == null)
        {
            InitializeAudioSource();
        }
    }
    
    /// <summary>
    /// 初始化音频源
    /// </summary>
    private void InitializeAudioSource()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // 设置音频源属性
        audioSource.playOnAwake = playOnAwake;
        audioSource.volume = volume;
        audioSource.spatialBlend = 0f; // 2D音效
    }
    
    #region 音效播放接口
    
    /// <summary>
    /// 播放加入购物车音效
    /// </summary>
    public void PlayAddToCartSound()
    {
        PlaySound(addToCartSound);
    }
    
    /// <summary>
    /// 播放直接购买音效
    /// </summary>
    public void PlayPurchaseSound()
    {
        PlaySound(purchaseSound);
    }
    
    /// <summary>
    /// 播放购物车购买音效
    /// </summary>
    public void PlayCartPurchaseSound()
    {
        PlaySound(cartPurchaseSound);
    }
    
    /// <summary>
    /// 播放点击音效
    /// </summary>
    public void PlayClickedSound()
    {
        PlaySound(clickedSound);
    }
    
    /// <summary>
    /// 播放成功音效
    /// </summary>
    public void PlaySuccessSound()
    {
        PlaySound(successSound);
    }
    
    /// <summary>
    /// 播放胜利音效
    /// </summary>
    public void PlayWinSound()
    {
        PlaySound(winSound);
    }
    
    /// <summary>
    /// 播放失败音效
    /// </summary>
    public void PlayFailSound()
    {
        PlaySound(failSound);
    }
    
    /// <summary>
    /// 通用音效播放方法
    /// </summary>
    /// <param name="clip">要播放的音效</param>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
            Debug.Log($"播放音效: {clip.name}");
        }
        else
        {
            if (clip == null)
            {
                Debug.LogWarning("音效文件为空，无法播放");
            }
            if (audioSource == null)
            {
                Debug.LogWarning("音频源为空，无法播放音效");
            }
        }
    }
    
    #endregion
    
    #region 音效设置接口
    
    /// <summary>
    /// 设置加入购物车音效
    /// </summary>
    /// <param name="clip">音效文件</param>
    public void SetAddToCartSound(AudioClip clip)
    {
        addToCartSound = clip;
        Debug.Log($"设置加入购物车音效: {(clip != null ? clip.name : "无")}");
    }
    
    /// <summary>
    /// 设置直接购买音效
    /// </summary>
    /// <param name="clip">音效文件</param>
    public void SetPurchaseSound(AudioClip clip)
    {
        purchaseSound = clip;
        Debug.Log($"设置直接购买音效: {(clip != null ? clip.name : "无")}");
    }
    
    /// <summary>
    /// 设置购物车购买音效
    /// </summary>
    /// <param name="clip">音效文件</param>
    public void SetCartPurchaseSound(AudioClip clip)
    {
        cartPurchaseSound = clip;
        Debug.Log($"设置购物车购买音效: {(clip != null ? clip.name : "无")}");
    }
    
    /// <summary>
    /// 设置点击音效
    /// </summary>
    /// <param name="clip">音效文件</param>
    public void SetClickedSound(AudioClip clip)
    {
        clickedSound = clip;
        Debug.Log($"设置点击音效: {(clip != null ? clip.name : "无")}");
    }
    
    /// <summary>
    /// 设置成功音效
    /// </summary>
    /// <param name="clip">音效文件</param>
    public void SetSuccessSound(AudioClip clip)
    {
        successSound = clip;
        Debug.Log($"设置成功音效: {(clip != null ? clip.name : "无")}");
    }
    
    /// <summary>
    /// 设置胜利音效
    /// </summary>
    /// <param name="clip">音效文件</param>
    public void SetWinSound(AudioClip clip)
    {
        winSound = clip;
        Debug.Log($"设置胜利音效: {(clip != null ? clip.name : "无")}");
    }
    
    /// <summary>
    /// 设置失败音效
    /// </summary>
    /// <param name="clip">音效文件</param>
    public void SetFailSound(AudioClip clip)
    {
        failSound = clip;
        Debug.Log($"设置失败音效: {(clip != null ? clip.name : "无")}");
    }
    
    /// <summary>
    /// 设置音量
    /// </summary>
    /// <param name="newVolume">音量大小 (0-1)</param>
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        Debug.Log($"设置音量为: {volume}");
    }
    
    /// <summary>
    /// 获取当前音量
    /// </summary>
    /// <returns>当前音量值</returns>
    public float GetVolume()
    {
        return volume;
    }
    
    #endregion
    
    #region 音效状态检查
    
    /// <summary>
    /// 检查加入购物车音效是否已设置
    /// </summary>
    /// <returns>是否已设置</returns>
    public bool HasAddToCartSound()
    {
        return addToCartSound != null;
    }
    
    /// <summary>
    /// 检查直接购买音效是否已设置
    /// </summary>
    /// <returns>是否已设置</returns>
    public bool HasPurchaseSound()
    {
        return purchaseSound != null;
    }
    
    /// <summary>
    /// 检查购物车购买音效是否已设置
    /// </summary>
    /// <returns>是否已设置</returns>
    public bool HasCartPurchaseSound()
    {
        return cartPurchaseSound != null;
    }
    
    /// <summary>
    /// 检查点击音效是否已设置
    /// </summary>
    /// <returns>是否已设置</returns>
    public bool HasClickedSound()
    {
        return clickedSound != null;
    }
    
    /// <summary>
    /// 检查成功音效是否已设置
    /// </summary>
    /// <returns>是否已设置</returns>
    public bool HasSuccessSound()
    {
        return successSound != null;
    }
    
    /// <summary>
    /// 检查胜利音效是否已设置
    /// </summary>
    /// <returns>是否已设置</returns>
    public bool HasWinSound()
    {
        return winSound != null;
    }
    
    /// <summary>
    /// 检查失败音效是否已设置
    /// </summary>
    /// <returns>是否已设置</returns>
    public bool HasFailSound()
    {
        return failSound != null;
    }
    
    /// <summary>
    /// 检查音频源是否可用
    /// </summary>
    /// <returns>是否可用</returns>
    public bool IsAudioSourceReady()
    {
        return audioSource != null;
    }
    
    #endregion
    
    #region 调试和测试方法
    
    /// <summary>
    /// 测试播放加入购物车音效
    /// </summary>
    [ContextMenu("测试加入购物车音效")]
    public void TestAddToCartSound()
    {
        PlayAddToCartSound();
    }
    
    /// <summary>
    /// 测试播放直接购买音效
    /// </summary>
    [ContextMenu("测试直接购买音效")]
    public void TestPurchaseSound()
    {
        PlayPurchaseSound();
    }
    
    /// <summary>
    /// 测试播放购物车购买音效
    /// </summary>
    [ContextMenu("测试购物车购买音效")]
    public void TestCartPurchaseSound()
    {
        PlayCartPurchaseSound();
    }
    
    /// <summary>
    /// 测试播放点击音效
    /// </summary>
    [ContextMenu("测试点击音效")]
    public void TestClickedSound()
    {
        PlayClickedSound();
    }
    
    /// <summary>
    /// 测试播放成功音效
    /// </summary>
    [ContextMenu("测试成功音效")]
    public void TestSuccessSound()
    {
        PlaySuccessSound();
    }
    
    /// <summary>
    /// 测试播放胜利音效
    /// </summary>
    [ContextMenu("测试胜利音效")]
    public void TestWinSound()
    {
        PlayWinSound();
    }
    
    /// <summary>
    /// 测试播放失败音效
    /// </summary>
    [ContextMenu("测试失败音效")]
    public void TestFailSound()
    {
        PlayFailSound();
    }
    
    /// <summary>
    /// 打印当前音效设置状态
    /// </summary>
    [ContextMenu("打印音效状态")]
    public void PrintAudioStatus()
    {
        Debug.Log("=== 购物音效管理器状态 ===");
        Debug.Log($"加入购物车音效: {(HasAddToCartSound() ? addToCartSound.name : "未设置")}");
        Debug.Log($"直接购买音效: {(HasPurchaseSound() ? purchaseSound.name : "未设置")}");
        Debug.Log($"购物车购买音效: {(HasCartPurchaseSound() ? cartPurchaseSound.name : "未设置")}");
        Debug.Log($"点击音效: {(HasClickedSound() ? clickedSound.name : "未设置")}");
        Debug.Log($"成功音效: {(HasSuccessSound() ? successSound.name : "未设置")}");
        Debug.Log($"胜利音效: {(HasWinSound() ? winSound.name : "未设置")}");
        Debug.Log($"失败音效: {(HasFailSound() ? failSound.name : "未设置")}");
        Debug.Log($"音频源状态: {(IsAudioSourceReady() ? "正常" : "异常")}");
        Debug.Log($"当前音量: {volume}");
        Debug.Log("========================");
    }
    
    #endregion
    
    private void OnValidate()
    {
        // 在编辑器中实时更新音量
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
