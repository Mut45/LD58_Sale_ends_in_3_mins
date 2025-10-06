using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Start Scene UI Manager
/// 功能：按下按钮传送到Shop_Page
/// </summary>
public class StartSceneUIManage : MonoBehaviour
{
    
    [Header("按钮设置")]
    [SerializeField] private Button shopButton;                  // 商店按钮
    
    [Header("场景设置")]
    [SerializeField] private string shopSceneName = "Shop_Page"; // 商店场景名称

    public ShoppingAudioTrigger shoppingAudioTrigger;
    private void Start()
    {
        SetupButton();
    }
    
    /// <summary>
    /// 设置按钮点击事件
    /// </summary>
    private void SetupButton()
    {
        if (shopButton != null)
        {
            shopButton.onClick.RemoveAllListeners();
            shopButton.onClick.AddListener(OnShopButtonClicked);
        }
    }
    
    /// <summary>
    /// 商店按钮点击事件
    /// </summary>
    public void OnShopButtonClicked()
    {
        if(shoppingAudioTrigger != null)
        {
            shoppingAudioTrigger.TriggerClickedSound();
        }
        SceneManager.LoadScene(shopSceneName);
    }
}
