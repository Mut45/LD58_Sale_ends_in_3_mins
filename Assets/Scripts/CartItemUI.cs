using UnityEngine;
using UnityEngine.UI;

public class CartItemUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image gameIconImage;
    [SerializeField] private Text gameTitleText;
    [SerializeField] private Text originalPriceText;
    [SerializeField] private Text discountText;
    [SerializeField] private Text finalPriceText;
    [SerializeField] private Button removeButton;
    
    [Header("Visual Settings")]
    [SerializeField] private Color originalPriceColor = Color.gray;
    [SerializeField] private Color finalPriceColor = Color.white;
    [SerializeField] private Color discountColor = Color.green;
    
    private GameData currentGameData;
    private int cartIndex = -1; // 在购物车中的索引
    
    private void Start()
    {
        InitializeUI();
    }
    
    private void InitializeUI()
    {
        // 设置移除按钮点击事件
        if (removeButton != null)
        {
            removeButton.onClick.RemoveAllListeners();
            removeButton.onClick.AddListener(OnRemoveButtonClicked);
        }
    }
    
    public void SetGameData(GameData gameData, int index = -1)
    {
        currentGameData = gameData;
        cartIndex = index;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (currentGameData == null) return;
        
        // 更新游戏图标
        if (gameIconImage != null)
            gameIconImage.sprite = currentGameData.gameIcon;
        
        // 更新游戏标题
        if (gameTitleText != null)
            gameTitleText.text = currentGameData.title;
        
        // 更新价格信息
        UpdatePriceDisplay();
    }
    
    private void UpdatePriceDisplay()
    {
        if (currentGameData == null) return;
        
        // 显示原价（带删除线效果）
        if (originalPriceText != null)
        {
            if (currentGameData.discount > 0)
            {
                originalPriceText.text = $"¥{currentGameData.originalPrice:F2}";
                originalPriceText.color = originalPriceColor;
                originalPriceText.gameObject.SetActive(true);
            }
            else
            {
                originalPriceText.gameObject.SetActive(false);
            }
        }
        
        // 显示折扣信息
        if (discountText != null)
        {
            if (currentGameData.discount > 0)
            {
                discountText.text = $"-{currentGameData.discount:F0}%";
                discountText.color = discountColor;
                discountText.gameObject.SetActive(true);
            }
            else
            {
                discountText.gameObject.SetActive(false);
            }
        }
        
        // 显示最终价格
        if (finalPriceText != null)
        {
            finalPriceText.text = $"¥{currentGameData.FinalPrice:F2}";
            finalPriceText.color = finalPriceColor;
        }
    }
    
    public void OnRemoveButtonClicked()
    {
        if (currentGameData != null && ShoppingCart.Instance != null)
        {
            // 优先使用索引移除，如果没有索引则使用游戏数据移除
            if (cartIndex >= 0)
            {
                ShoppingCart.Instance.RemoveFromCartByIndex(cartIndex);
                Debug.Log($"从购物车移除: {currentGameData.title} (索引: {cartIndex})");
            }
            else
            {
                ShoppingCart.Instance.RemoveFromCart(currentGameData);
                Debug.Log($"从购物车移除: {currentGameData.title}");
            }
        }
    }
    
    // 获取当前游戏数据
    public GameData GetGameData()
    {
        return currentGameData;
    }
}

