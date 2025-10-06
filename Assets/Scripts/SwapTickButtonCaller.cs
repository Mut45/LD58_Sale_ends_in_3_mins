using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// StoreUI HandleSwapTick调用器
/// 功能：
/// 1. 提供按钮接口，可以拖拽按钮组件
/// 2. 按下按钮后调用StoreUI的HandleSwapTick()方法
/// 3. 不修改原有代码，完全独立运行
/// </summary>
public class SwapTickButtonCaller : MonoBehaviour
{
    [Header("按钮设置")]
    [SerializeField] private Button targetButton;                // 目标按钮（可拖拽）
    
    [Header("StoreUI设置")]
    [SerializeField] private StoreUI storeUI;                   // StoreUI组件引用
    [SerializeField] private bool autoFindStoreUI = true;       // 是否自动查找StoreUI
    
    [Header("音效设置")]
    [SerializeField] private bool playClickSound = true;        // 是否播放点击音效
    [SerializeField] private bool playSuccessSound = true;      // 是否播放成功音效
    
    [Header("调试设置")]
    [SerializeField] private bool debugMode = false;            // 调试模式
    [SerializeField] private bool logCallDetails = true;        // 是否记录调用详情
    
    private void Start()
    {
        InitializeComponents();
    }
    
    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitializeComponents()
    {
        // 自动查找StoreUI组件
        if (autoFindStoreUI && storeUI == null)
        {
            FindStoreUI();
        }
        
        // 设置按钮点击事件
        SetupButtonEvent();
        
        if (debugMode)
        {
            Debug.Log($"[SwapTickButtonCaller] 初始化完成 - 按钮: {(targetButton != null ? "已设置" : "未设置")}, StoreUI: {(storeUI != null ? "已找到" : "未找到")}");
        }
    }
    
    /// <summary>
    /// 自动查找StoreUI组件
    /// </summary>
    private void FindStoreUI()
    {
        // 首先在当前GameObject上查找
        storeUI = GetComponent<StoreUI>();
        
        // 如果没找到，在场景中查找
        if (storeUI == null)
        {
            storeUI = FindObjectOfType<StoreUI>();
        }
        
        // 如果还没找到，尝试通过名称查找
        if (storeUI == null)
        {
            GameObject storeUIObj = GameObject.Find("StoreUI");
            if (storeUIObj != null)
            {
                storeUI = storeUIObj.GetComponent<StoreUI>();
            }
        }
        
        if (storeUI != null && debugMode)
        {
            Debug.Log($"[SwapTickButtonCaller] 自动找到StoreUI: {storeUI.gameObject.name}");
        }
        else if (storeUI == null)
        {
            Debug.LogWarning("[SwapTickButtonCaller] 未找到StoreUI组件，请手动设置");
        }
    }
    
    /// <summary>
    /// 设置按钮点击事件
    /// </summary>
    private void SetupButtonEvent()
    {
        if (targetButton != null)
        {
            // 移除所有现有监听器
            targetButton.onClick.RemoveAllListeners();
            
            // 添加新的点击事件
            targetButton.onClick.AddListener(OnButtonClicked);
            
            if (debugMode)
            {
                Debug.Log($"[SwapTickButtonCaller] 按钮点击事件已设置: {targetButton.gameObject.name}");
            }
        }
        else
        {
            Debug.LogWarning("[SwapTickButtonCaller] 目标按钮未设置，无法设置点击事件");
        }
    }
    
    /// <summary>
    /// 按钮点击事件处理
    /// </summary>
    public void OnButtonClicked()
    {
        if (debugMode)
        {
            Debug.Log("[SwapTickButtonCaller] 按钮被点击，准备调用HandleSwapTick()");
        }
        
        // 播放点击音效
        if (playClickSound)
        {
            PlayClickSound();
        }
        
        // 调用StoreUI的HandleSwapTick方法
        CallHandleSwapTick();
    }
    
    /// <summary>
    /// 调用StoreUI的HandleSwapTick方法
    /// </summary>
    private void CallHandleSwapTick()
    {
        if (storeUI == null)
        {
            Debug.LogError("[SwapTickButtonCaller] StoreUI组件为空，无法调用HandleSwapTick()");
            return;
        }
        
        try
        {
            if (logCallDetails)
            {
                Debug.Log($"[SwapTickButtonCaller] 正在调用 {storeUI.gameObject.name}.HandleSwapTick()");
            }
            
            // 使用反射调用HandleSwapTick方法
            var method = typeof(StoreUI).GetMethod("HandleSwapTick", 
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
            
            if (method != null)
            {
                method.Invoke(storeUI, null);
                
                if (logCallDetails)
                {
                    Debug.Log("[SwapTickButtonCaller] HandleSwapTick() 调用成功");
                }
                
                // 播放成功音效
                if (playSuccessSound)
                {
                    PlaySuccessSound();
                }
            }
            else
            {
                Debug.LogError("[SwapTickButtonCaller] 在StoreUI中未找到HandleSwapTick()方法");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SwapTickButtonCaller] 调用HandleSwapTick()时发生错误: {e.Message}");
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
        else if (debugMode)
        {
            Debug.LogWarning("[SwapTickButtonCaller] ShoppingAudioManager未找到，无法播放点击音效");
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
        else if (debugMode)
        {
            Debug.LogWarning("[SwapTickButtonCaller] ShoppingAudioManager未找到，无法播放成功音效");
        }
    }
    
    #region 公共接口
    
    /// <summary>
    /// 手动触发HandleSwapTick调用
    /// </summary>
    public void TriggerHandleSwapTick()
    {
        if (debugMode)
        {
            Debug.Log("[SwapTickButtonCaller] 手动触发HandleSwapTick调用");
        }
        CallHandleSwapTick();
    }
    
    /// <summary>
    /// 设置目标按钮
    /// </summary>
    /// <param name="button">按钮组件</param>
    public void SetTargetButton(Button button)
    {
        targetButton = button;
        SetupButtonEvent();
        Debug.Log($"[SwapTickButtonCaller] 设置目标按钮: {(button != null ? button.gameObject.name : "null")}");
    }
    
    /// <summary>
    /// 设置StoreUI组件
    /// </summary>
    /// <param name="storeUIComponent">StoreUI组件</param>
    public void SetStoreUI(StoreUI storeUIComponent)
    {
        storeUI = storeUIComponent;
        Debug.Log($"[SwapTickButtonCaller] 设置StoreUI: {(storeUIComponent != null ? storeUIComponent.gameObject.name : "null")}");
    }
    
    /// <summary>
    /// 启用/禁用自动查找StoreUI
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetAutoFindStoreUI(bool enable)
    {
        autoFindStoreUI = enable;
        if (enable && storeUI == null)
        {
            FindStoreUI();
        }
        Debug.Log($"[SwapTickButtonCaller] 自动查找StoreUI: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用点击音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetClickSoundEnabled(bool enable)
    {
        playClickSound = enable;
        Debug.Log($"[SwapTickButtonCaller] 点击音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 启用/禁用成功音效
    /// </summary>
    /// <param name="enable">是否启用</param>
    public void SetSuccessSoundEnabled(bool enable)
    {
        playSuccessSound = enable;
        Debug.Log($"[SwapTickButtonCaller] 成功音效: {(enable ? "启用" : "禁用")}");
    }
    
    /// <summary>
    /// 设置调试模式
    /// </summary>
    /// <param name="enable">是否启用调试模式</param>
    public void SetDebugMode(bool enable)
    {
        debugMode = enable;
        Debug.Log($"[SwapTickButtonCaller] 调试模式: {(enable ? "启用" : "禁用")}");
    }
    
    #endregion
    
    #region 调试和测试方法
    
    /// <summary>
    /// 测试HandleSwapTick调用
    /// </summary>
    [ContextMenu("测试HandleSwapTick调用")]
    public void TestHandleSwapTick()
    {
        Debug.Log("[SwapTickButtonCaller] 开始测试HandleSwapTick调用...");
        CallHandleSwapTick();
    }
    
    /// <summary>
    /// 重新查找StoreUI
    /// </summary>
    [ContextMenu("重新查找StoreUI")]
    public void RefreshStoreUI()
    {
        storeUI = null;
        FindStoreUI();
        Debug.Log($"[SwapTickButtonCaller] 重新查找StoreUI: {(storeUI != null ? "成功" : "失败")}");
    }
    
    /// <summary>
    /// 检查组件状态
    /// </summary>
    [ContextMenu("检查组件状态")]
    public void CheckComponentStatus()
    {
        Debug.Log("=== SwapTickButtonCaller 组件状态 ===");
        Debug.Log($"目标按钮: {(targetButton != null ? targetButton.gameObject.name : "未设置")}");
        Debug.Log($"StoreUI: {(storeUI != null ? storeUI.gameObject.name : "未找到")}");
        Debug.Log($"自动查找StoreUI: {(autoFindStoreUI ? "启用" : "禁用")}");
        Debug.Log($"点击音效: {(playClickSound ? "启用" : "禁用")}");
        Debug.Log($"成功音效: {(playSuccessSound ? "启用" : "禁用")}");
        Debug.Log($"调试模式: {(debugMode ? "启用" : "禁用")}");
        Debug.Log($"音效管理器: {(ShoppingAudioManager.Instance != null ? "正常" : "未找到")}");
        
        // 检查HandleSwapTick方法是否存在
        if (storeUI != null)
        {
            var method = typeof(StoreUI).GetMethod("HandleSwapTick", 
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
            Debug.Log($"HandleSwapTick方法: {(method != null ? "存在" : "不存在")}");
        }
        
        Debug.Log("=====================================");
    }
    
    #endregion
    
    private void OnValidate()
    {
        // 在编辑器中实时更新设置
        if (Application.isPlaying)
        {
            SetupButtonEvent();
        }
    }
}
