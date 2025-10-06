using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI References")]
    [SerializeField] private Image gameIconImage;
    [SerializeField] private GameObject hoverPanel;
    [SerializeField] private Text titleText;
    [SerializeField] private Text ratingText;
    [SerializeField] private Text priceText;
    [SerializeField] private Text discountText;
    [SerializeField] private Text finalPriceText;
    [SerializeField] private Text typeText;
    public StoreUI storeUI;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button addToCartButton;
    
    [Header("Visual Effects")]
    [SerializeField] private Image cardBackground;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color purchasedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    [SerializeField] private CurrencyManager currencyManager;
    
    private GameData gameData;
    private bool isPurchased = false;
    
    void Start()
    {
        hoverPanel.SetActive(false);
    }
    public void SetGameData(GameData data)
    {
        gameData = data;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        Debug.Log("Update UI called");
        if (gameData == null) {
            Debug.Log("game data is null.");
            return; 
        }
        
        // 更新游戏图标
        if (gameIconImage != null)
            gameIconImage.sprite = gameData.gameIcon;
        
        // 更新悬停面板信息
        if (titleText != null)
            titleText.text = gameData.title;
        
        if (ratingText != null)
            ratingText.text = $"Rating: {gameData.rating:F1}";
        
        if (priceText != null)
            priceText.text = $"${gameData.originalPrice*0.01:F2}";

        if (discountText != null)
        {
            float discountOff = 1 - gameData.discount;
            discountText.text = gameData.discount > 0 ? $"-{discountOff*100:F0}%" : "-0%";
        }
            
        
        if (finalPriceText != null)
            finalPriceText.text = $"${gameData.FinalPrice*0.01:F2}";
        
        if (typeText != null)
            typeText.text = $" {gameData.type}";
        
        // 设置按钮点击事件
        if (purchaseButton != null)
        {
            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(OnPurchaseClicked);
        }
        
        if (addToCartButton != null)
        {
            addToCartButton.onClick.RemoveAllListeners();
            addToCartButton.onClick.AddListener(OnAddToCartClicked);
        }
        
        // 检查是否已购买
        CheckPurchaseStatus();
    }
    
    private void CheckPurchaseStatus()
    {
        if (GameLibrary.Instance != null && gameData != null)
        {
            isPurchased = GameLibrary.Instance.HasGame(gameData.id);
            UpdateVisualState();
            Debug.Log($"GameCard {gameData.title} 购买状态检查: {isPurchased}");
        }
    }
    
    private void UpdateVisualState()
    {
        if (cardBackground != null)
        {
            cardBackground.color = isPurchased ? purchasedColor : normalColor;
        }
        
        // 如果已购买，禁用按钮
        if (purchaseButton != null)
            purchaseButton.interactable = !isPurchased;
        
        if (addToCartButton != null)
            addToCartButton.interactable = !isPurchased;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverPanel != null && !isPurchased)
        {
            hoverPanel.SetActive(true);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverPanel != null)
        {
            hoverPanel.SetActive(false);
        }
    }
    
    private void OnPurchaseClicked()
    {
        if (gameData != null) // 注释掉 !isPurchased 检查，允许重复购买
        {
            // 添加到游戏库
            Debug.Log("Purchase clicked");
            if (GameLibrary.Instance != null)
            {
                GameLibrary.Instance.AddGame(gameData);
                storeUI.Purchase(gameData);

                if (hoverPanel != null)
                    hoverPanel.SetActive(false);
                Debug.Log($"Current Balance: {CurrencyManager.Instance.BalanceInCents}");
                Debug.Log($"已购买游戏: {gameData.title}，游戏库现在有 {GameLibrary.Instance.GetOwnedGames().Count} 个游戏");
            }
            else
            {
                Debug.Log("Clicked but no Gamelibrary instance");

            }
        }
        else
        {
            Debug.Log("Game data not found.");
        }
    }
    
    private void OnAddToCartClicked()
    {
        if (gameData != null) // 注释掉 !isPurchased 检查，允许重复添加到购物车
        {
            if (ShoppingCart.Instance != null)
            {
                bool success = ShoppingCart.Instance.AddToCart(gameData);
                if (success)
                {
                    Debug.Log($"游戏已添加到购物车: {gameData.title}");
                }
                else
                {
                    Debug.Log($"无法添加游戏 {gameData.title} 到购物车（可能已在购物车中或购物车已满）");
                }
            }
        }
        // 注释掉已购买检查
        // else if (isPurchased)
        // {
        //     Debug.Log($"游戏 {gameData.title} 已经购买过了，无法添加到购物车");
        // }
    }
    
    private void OnEnable()
    {
        CheckPurchaseStatus();
    }
    
    // 公共方法，用于外部刷新购买状态
    public void RefreshPurchaseStatus()
    {
        CheckPurchaseStatus();
    }
}



