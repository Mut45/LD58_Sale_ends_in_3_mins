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
}
