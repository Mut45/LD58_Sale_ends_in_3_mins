using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    public static CurrencyDisplay Instance;
    [SerializeField] private Text balanceText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTextBalance();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void UpdateTextBalance()
    {
        if (balanceText == null)
        {
            return;
        }
        balanceText.text = $"${CurrencyManager.Instance.BalanceInCents*0.01:F2}";
    }
    
    // 公共方法：强制刷新余额显示（场景进入时调用）
    public void RefreshBalanceDisplay()
    {
        // 重新查找balanceText
        FindAndBindBalanceText();
        // 更新显示
        UpdateTextBalance();
        Debug.Log($"Balance display refreshed. Current balance: ${CurrencyManager.Instance.BalanceInCents*0.01:F2}");
    }
    
    // 自动查找并绑定balanceText
    private void FindAndBindBalanceText()
    {
        // 方法1: 通过名称查找
        GameObject balanceObj = GameObject.Find("BalanceText");
        if (balanceObj != null)
        {
            Text foundText = balanceObj.GetComponent<Text>();
            if (foundText != null)
            {
                balanceText = foundText;
                Debug.Log($"Found balanceText by name 'BalanceText': {balanceObj.name}");
                return;
            }
        }
        
        // 方法2: 查找所有Text组件，寻找可能包含"balance"或"money"的文本
        Text[] allTexts = FindObjectsOfType<Text>();
        foreach (Text text in allTexts)
        {
            if (text.name.ToLower().Contains("balance") || 
                text.name.ToLower().Contains("money") ||
                text.name.ToLower().Contains("currency") ||
                (text.text.Contains("$") && text.text.Length <= 10)) // 可能是金额格式
            {
                balanceText = text;
                Debug.Log($"Found potential balanceText by content: {text.name}");
                return;
            }
        }
        
        Debug.LogWarning("Could not find balanceText in the new scene. Balance will not be displayed.");
    }
}
