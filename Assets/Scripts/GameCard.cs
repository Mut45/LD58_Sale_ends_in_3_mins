using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

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
    
    [Header("Hover Effects")]
    [SerializeField] private float fadeInDuration = 0.3f;
    [SerializeField] private float fadeOutDuration = 0.3f;
    
    private GameData gameData;
    private bool isPurchased = false;
    public ShoppingAudioTrigger shoppingAudioTrigger;
    private Coroutine currentFadeCoroutine;
    
    void Start()
    {
        hoverPanel.SetActive(true);
        // 设置初始透明度为0
        SetHoverPanelAlpha(0f);
    }
    public void SetGameData(GameData data)
    {
        Debug.Log($"SetGameData called on {gameObject.name} with data: {(data != null ? data.title : "NULL")}");
        gameData = data;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        Debug.Log($"Update UI called for {gameData?.title ?? "Unknown"}");
        if (gameData == null) {
            Debug.Log("game data is null.");
            return; 
        }
        
        Debug.Log($"Updating UI for game: {gameData.title}, ID: {gameData.id}");
        
        // 更新游戏图标
        if (gameIconImage != null)
            gameIconImage.sprite = gameData.gameIcon;
        
        // 更新悬停面板信息
        Debug.Log($"Checking UI components - titleText: {titleText != null}, ratingText: {ratingText != null}, priceText: {priceText != null}");
        
        if (titleText != null)
        {
            titleText.text = gameData.title;
            Debug.Log($"Set title text to: {gameData.title}");
        }
        else
        {
            Debug.LogWarning("titleText is null!");
        }
        
        if (ratingText != null)
        {
            ratingText.text = $"Rating: {gameData.rating:F1}";
            Debug.Log($"Set rating text to: Rating: {gameData.rating:F1}");
        }
        else
        {
            Debug.LogWarning("ratingText is null!");
        }
        
        if (priceText != null)
        {
            priceText.text = $"${gameData.originalPrice*0.01:F2}";
            Debug.Log($"Set price text to: ${gameData.originalPrice*0.01:F2}");
        }
        else
        {
            Debug.LogWarning("priceText is null!");
        }

        if (discountText != null)
        {
            float discountOff = 1 - gameData.discount;
            discountText.text = gameData.discount > 0 ? $"-{discountOff*100:F0}%" : "-0%";
            Debug.Log($"Set discount text to: {discountText.text}");
        }
        else
        {
            Debug.LogWarning("discountText is null!");
        }
            
        
        if (finalPriceText != null)
        {
            finalPriceText.text = $"${gameData.FinalPrice*0.01:F2}";
            Debug.Log($"Set final price text to: ${gameData.FinalPrice*0.01:F2}");
        }
        else
        {
            Debug.LogWarning("finalPriceText is null!");
        }
        
        if (typeText != null)
        {
            typeText.text = $" {gameData.type}";
            Debug.Log($"Set type text to: {gameData.type}");
        }
        else
        {
            Debug.LogWarning("typeText is null!");
        }
        
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
        Debug.Log($"OnPointerEnter called for {gameData?.title ?? "Unknown"}");
        
        if (hoverPanel != null)
        {
            // 停止当前的淡入淡出协程
            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }
            
            // 开始淡入效果
            currentFadeCoroutine = StartCoroutine(FadeHoverPanel(0f, 1f, fadeInDuration));
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverPanel != null)
        {
            // 停止当前的淡入淡出协程
            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }
            
            // 开始淡出效果
            currentFadeCoroutine = StartCoroutine(FadeHoverPanel(1f, 0f, fadeOutDuration));
        }
    }
    
    // 淡入淡出协程
    private IEnumerator FadeHoverPanel(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetHoverPanelAlpha(alpha);
            yield return null;
        }
        
        // 确保最终值正确
        SetHoverPanelAlpha(endAlpha);
        currentFadeCoroutine = null;
    }
    
    // 设置悬停面板的透明度
    private void SetHoverPanelAlpha(float alpha)
    {
        if (hoverPanel == null) return;
        
        // 获取所有需要设置透明度的组件
        CanvasGroup canvasGroup = hoverPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = hoverPanel.AddComponent<CanvasGroup>();
        }
        
        canvasGroup.alpha = alpha;
    }
    
    private void OnPurchaseClicked()
    {
        if (gameData != null) // 注释掉 !isPurchased 检查，允许重复购买
        {
            // 添加到游戏库
            Debug.Log("Purchase clicked");
            if(shoppingAudioTrigger != null)
            {
                shoppingAudioTrigger.TriggerPurchaseSound();
            }
            if (GameLibrary.Instance != null)
            {
                bool sucess = storeUI.Purchase(gameData);
                if (sucess)
                {
                    GameLibrary.Instance.AddGame(gameData);
                    Debug.Log($"已购买游戏: {gameData.title}，游戏库现在有 {GameLibrary.Instance.GetOwnedGames().Count} 个游戏");
                }
                // 移除直接禁用hoverPanel的代码，让悬停效果继续工作
                Debug.Log($"Current Balance: {CurrencyManager.Instance.BalanceInCents}");
                
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
                if (shoppingAudioTrigger != null)
                {
                    shoppingAudioTrigger.TriggerAddToCartSound();
                }
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



