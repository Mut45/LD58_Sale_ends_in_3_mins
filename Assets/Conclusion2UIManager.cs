using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Start Scene UI Manager
/// 功能：按下按钮传送到Shop_Page
/// </summary>
public class StartSceneUIManager : MonoBehaviour
{

    [Header("按钮设置")]
    [SerializeField] private Button shopButton1;                  // 商店按钮

    [Header("场景设置")]
    [SerializeField] private string Ending1 = "Ending1"; // 商店场景名称

    public ShoppingAudioTrigger shoppingAudioTrigger1;
    private void Start()
    {
        SetupButton1();
    }

    /// <summary>
    /// 设置按钮点击事件
    /// </summary>
    private void SetupButton1()
    {
        if (shopButton1 != null)
        {
            shopButton1.onClick.RemoveAllListeners();
            shopButton1.onClick.AddListener(OnShopButtonClicked1);
        }
    }

    /// <summary>
    /// 商店按钮点击事件
    /// </summary>
    public void OnShopButtonClicked1()
    {
        if (shoppingAudioTrigger1 != null)
        {
            shoppingAudioTrigger1.TriggerClickedSound();
        }
        SceneManager.LoadScene(Ending1);
    }
}
