using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Start Scene UI Manager
/// ���ܣ����°�ť���͵�Shop_Page
/// </summary>
public class StartSceneUIManager : MonoBehaviour
{

    [Header("��ť����")]
    [SerializeField] private Button shopButton1;                  // �̵갴ť

    [Header("��������")]
    [SerializeField] private string Ending1 = "Ending1"; // �̵곡������

    public ShoppingAudioTrigger shoppingAudioTrigger1;
    private void Start()
    {
        SetupButton1();
    }

    /// <summary>
    /// ���ð�ť����¼�
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
    /// �̵갴ť����¼�
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
