using UnityEngine;

/// <summary>
/// 购物音效触发器
/// 用于在现有的购买流程中自动播放音效
/// 可以挂载到任何需要播放购物音效的GameObject上
/// </summary>
public class ShoppingAudioTrigger : MonoBehaviour
{
    [Header("音效触发设置")]
    [SerializeField] private bool enableAddToCartSound = true;    // 是否启用加入购物车音效
    [SerializeField] private bool enablePurchaseSound = true;    // 是否启用直接购买音效
    [SerializeField] private bool enableCartPurchaseSound = true; // 是否启用购物车购买音效
    [SerializeField] private bool enableClickedSound = true;     // 是否启用点击音效
    [SerializeField] private bool enableSuccessSound = true;     // 是否启用成功音效
    [SerializeField] private bool enableWinSound = true;          // 是否启用胜利音效
    [SerializeField] private bool enableFailSound = true;        // 是否启用失败音效
    
    [Header("触发条件")]
    [SerializeField] private bool playOnStart = false;           // 是否在开始时播放测试音效
    [SerializeField] private bool debugMode = false;            // 调试模式
    
    private void Start()
    {
        // 如果启用测试模式，播放测试音效
        if (playOnStart)
        {
            TestAudioSounds();
        }
        
        // 检查音效管理器是否存在
        if (ShoppingAudioManager.Instance == null)
        {
            Debug.LogWarning("ShoppingAudioManager 未找到，请确保场景中存在音效管理器");
        }
    }
    
    #region 音效触发方法
    
    /// <summary>
    /// 触发加入购物车音效
    /// </summary>
    public void TriggerAddToCartSound()
    {
        if (enableAddToCartSound && ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlayAddToCartSound();
            
            if (debugMode)
            {
                Debug.Log($"[{gameObject.name}] 触发加入购物车音效");
            }
        }
    }
    
    /// <summary>
    /// 触发直接购买音效
    /// </summary>
    public void TriggerPurchaseSound()
    {
        if (enablePurchaseSound && ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlayPurchaseSound();
            
            if (debugMode)
            {
                Debug.Log($"[{gameObject.name}] 触发直接购买音效");
            }
        }
    }
    
    /// <summary>
    /// 触发购物车购买音效
    /// </summary>
    public void TriggerCartPurchaseSound()
    {
        if (enableCartPurchaseSound && ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlayCartPurchaseSound();
            
            if (debugMode)
            {
                Debug.Log($"[{gameObject.name}] 触发购物车购买音效");
            }
        }
    }
    
    /// <summary>
    /// 触发点击音效
    /// </summary>
    public void TriggerClickedSound()
    {
        if (enableClickedSound && ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlayClickedSound();
            
            if (debugMode)
            {
                Debug.Log($"[{gameObject.name}] 触发点击音效");
            }
        }
    }
    
    /// <summary>
    /// 触发成功音效
    /// </summary>
    public void TriggerSuccessSound()
    {
        if (enableSuccessSound && ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlaySuccessSound();
            
            if (debugMode)
            {
                Debug.Log($"[{gameObject.name}] 触发成功音效");
            }
        }
    }
    
    /// <summary>
    /// 触发胜利音效
    /// </summary>
    public void TriggerWinSound()
    {
        if (enableWinSound && ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlayWinSound();
            
            if (debugMode)
            {
                Debug.Log($"[{gameObject.name}] 触发胜利音效");
            }
        }
    }
    
    /// <summary>
    /// 触发失败音效
    /// </summary>
    public void TriggerFailSound()
    {
        if (enableFailSound && ShoppingAudioManager.Instance != null)
        {
            ShoppingAudioManager.Instance.PlayFailSound();
            
            if (debugMode)
            {
                Debug.Log($"[{gameObject.name}] 触发失败音效");
            }
        }
    }
    
    #endregion
    
    #region 设置方法
    
    /// <summary>
    /// 启用/禁用加入购物车音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetAddToCartSoundEnabled(bool enable)
    {
        enableAddToCartSound = enable;
        Debug.Log($"[{gameObject.name}] 加入购物车音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用直接购买音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetPurchaseSoundEnabled(bool enable)
    {
        enablePurchaseSound = enable;
        Debug.Log($"[{gameObject.name}] 直接购买音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用购物车购买音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetCartPurchaseSoundEnabled(bool enable)
    {
        enableCartPurchaseSound = enable;
        Debug.Log($"[{gameObject.name}] 购物车购买音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用点击音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetClickedSoundEnabled(bool enable)
    {
        enableClickedSound = enable;
        Debug.Log($"[{gameObject.name}] 点击音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用成功音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetSuccessSoundEnabled(bool enable)
    {
        enableSuccessSound = enable;
        Debug.Log($"[{gameObject.name}] 成功音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用胜利音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetWinSoundEnabled(bool enable)
    {
        enableWinSound = enable;
        Debug.Log($"[{gameObject.name}] 胜利音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用失败音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetFailSoundEnabled(bool enable)
    {
        enableFailSound = enable;
        Debug.Log($"[{gameObject.name}] 失败音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 设置调试模式
    /// </summary>
    /// <param name="enable">是否启用调试模式</param>
    public void SetDebugMode(bool enable)
    {
        debugMode = enable;
        Debug.Log($"[{gameObject.name}] 调试模式: {(enable ? "启用" : "禁用")}");
    }
    
    #endregion
    
    #region 测试和调试方法
    
    /// <summary>
    /// 测试播放所有音效
    /// </summary>
    [ContextMenu("测试所有音效")]
    public void TestAudioSounds()
    {
        Debug.Log($"[{gameObject.name}] 开始测试音效...");
        
        if (ShoppingAudioManager.Instance != null)
        {
            // 测试加入购物车音效
            if (enableAddToCartSound)
            {
                Debug.Log("测试加入购物车音效");
                ShoppingAudioManager.Instance.PlayAddToCartSound();
            }
            
            // 延迟测试购买音效
            Invoke(nameof(TestPurchaseSound), 1f);
            // 延迟测试购物车购买音效
            Invoke(nameof(TestCartPurchaseSound), 2f);
            // 延迟测试点击音效
            Invoke(nameof(TestClickedSound), 3f);
            // 延迟测试成功音效
            Invoke(nameof(TestSuccessSound), 4f);
            // 延迟测试胜利音效
            Invoke(nameof(TestWinSound), 5f);
            // 延迟测试失败音效
            Invoke(nameof(TestFailSound), 6f);
        }
        else
        {
            Debug.LogError("ShoppingAudioManager 未找到，无法测试音效");
        }
    }
    
    /// <summary>
    /// 测试直接购买音效（延迟调用）
    /// </summary>
    private void TestPurchaseSound()
    {
        if (enablePurchaseSound)
        {
            Debug.Log("测试直接购买音效");
            ShoppingAudioManager.Instance.PlayPurchaseSound();
        }
    }
    
    /// <summary>
    /// 测试购物车购买音效（延迟调用）
    /// </summary>
    private void TestCartPurchaseSound()
    {
        if (enableCartPurchaseSound)
        {
            Debug.Log("测试购物车购买音效");
            ShoppingAudioManager.Instance.PlayCartPurchaseSound();
        }
    }
    
    /// <summary>
    /// 测试点击音效（延迟调用）
    /// </summary>
    private void TestClickedSound()
    {
        if (enableClickedSound)
        {
            Debug.Log("测试点击音效");
            ShoppingAudioManager.Instance.PlayClickedSound();
        }
    }
    
    /// <summary>
    /// 测试成功音效（延迟调用）
    /// </summary>
    private void TestSuccessSound()
    {
        if (enableSuccessSound)
        {
            Debug.Log("测试成功音效");
            ShoppingAudioManager.Instance.PlaySuccessSound();
        }
    }
    
    /// <summary>
    /// 测试胜利音效（延迟调用）
    /// </summary>
    private void TestWinSound()
    {
        if (enableWinSound)
        {
            Debug.Log("测试胜利音效");
            ShoppingAudioManager.Instance.PlayWinSound();
        }
    }
    
    /// <summary>
    /// 测试失败音效（延迟调用）
    /// </summary>
    private void TestFailSound()
    {
        if (enableFailSound)
        {
            Debug.Log("测试失败音效");
            ShoppingAudioManager.Instance.PlayFailSound();
        }
    }
    
    /// <summary>
    /// 打印当前设置状态
    /// </summary>
    [ContextMenu("打印设置状态")]
    public void PrintSettings()
    {
        Debug.Log($"=== {gameObject.name} 音效触发器设置 ===");
        Debug.Log($"加入购物车音效: {(enableAddToCartSound ? "启用" : "禁用")}");
        Debug.Log($"直接购买音效: {(enablePurchaseSound ? "启用" : "禁用")}");
        Debug.Log($"购物车购买音效: {(enableCartPurchaseSound ? "启用" : "禁用")}");
        Debug.Log($"点击音效: {(enableClickedSound ? "启用" : "禁用")}");
        Debug.Log($"成功音效: {(enableSuccessSound ? "启用" : "禁用")}");
        Debug.Log($"胜利音效: {(enableWinSound ? "启用" : "禁用")}");
        Debug.Log($"失败音效: {(enableFailSound ? "启用" : "禁用")}");
        Debug.Log($"调试模式: {(debugMode ? "启用" : "禁用")}");
        Debug.Log($"音效管理器状态: {(ShoppingAudioManager.Instance != null ? "正常" : "未找到")}");
        Debug.Log("=====================================");
    }
    
    #endregion
}
