using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NewCartPageUI : MonoBehaviour
{
    [Header("固定UI元素")]
    [SerializeField] private Button backToStoreButton;     // 返回商城按钮
    [SerializeField] private Button checkoutButton;        // 结算按钮
    [SerializeField] private Text totalPriceText;          // 总价显示
    [SerializeField] private Text emptyCartText;           // 空购物车提示
    
    [Header("动态内容区域")]
    [SerializeField] private Transform cartItemsContainer; // 购物车项目容器
    [SerializeField] private GameObject cartItemPrefab;    // 购物车项目预制体
    
    [Header("布局设置")]
    [SerializeField] private float itemSpacing = 20f;      // 项目间隔
    [SerializeField] private float containerTopMargin = 100f; // 容器顶部边距
    [SerializeField] private float containerBottomMargin = 200f; // 容器底部边距
    
    [Header("场景名称")]
    [SerializeField] private string storeSceneName = "Shop_Page";
    [SerializeField] private string checkoutSceneName = "CheckoutScene";
    
    private List<CartItemUI> cartItemUIs = new List<CartItemUI>();
    private ShoppingCart shoppingCart;
    
    private void Start()
    {
        shoppingCart = ShoppingCart.Instance;
        if (shoppingCart == null)
        {
            Debug.LogError("ShoppingCart instance not found!");
            return;
        }
        
        InitializeUI();
        UpdateCartDisplay();
        
        // 监听购物车变化
        ShoppingCart.OnCartChanged += UpdateCartDisplay;
    }
    
    private void OnDestroy()
    {
        ShoppingCart.OnCartChanged -= UpdateCartDisplay;
    }
    
    private void InitializeUI()
    {
        // 设置按钮事件
        if (backToStoreButton != null)
        {
            backToStoreButton.onClick.RemoveAllListeners();
            backToStoreButton.onClick.AddListener(OnBackToStoreClicked);
        }
        
        if (checkoutButton != null)
        {
            checkoutButton.onClick.RemoveAllListeners();
            checkoutButton.onClick.AddListener(OnCheckoutClicked);
        }
        
        // 初始状态显示空购物车
        ShowEmptyCart();
    }
    
    private void UpdateCartDisplay()
    {
        if (shoppingCart == null) return;
        
        var cartItems = shoppingCart.GetCartItems();
        
        if (cartItems.Count == 0)
        {
            ShowEmptyCart();
        }
        else
        {
            ShowCartItems(cartItems);
        }
        
        UpdateTotalPrice();
    }
    
    private void ShowEmptyCart()
    {
        // 显示空购物车提示
        if (emptyCartText != null)
            emptyCartText.gameObject.SetActive(true);
        
        // 隐藏所有购物车项目
        foreach (var itemUI in cartItemUIs)
        {
            if (itemUI != null)
                itemUI.gameObject.SetActive(false);
        }
        
        // 禁用结算按钮
        if (checkoutButton != null)
            checkoutButton.interactable = false;
    }
    
    private void ShowCartItems(List<GameData> cartItems)
    {
        // 隐藏空购物车提示
        if (emptyCartText != null)
            emptyCartText.gameObject.SetActive(false);
        
        // 确保有足够的UI项目
        while (cartItemUIs.Count < cartItems.Count)
        {
            CreateNewCartItemUI();
        }
        
        // 更新现有UI项目
        for (int i = 0; i < cartItems.Count; i++)
        {
            if (cartItemUIs[i] != null)
            {
                cartItemUIs[i].SetGameData(cartItems[i], i);
                cartItemUIs[i].gameObject.SetActive(true);
                
                // 设置位置
                SetItemPosition(cartItemUIs[i].gameObject, i);
            }
        }
        
        // 隐藏多余的UI项目
        for (int i = cartItems.Count; i < cartItemUIs.Count; i++)
        {
            if (cartItemUIs[i] != null)
                cartItemUIs[i].gameObject.SetActive(false);
        }
        
        // 启用结算按钮
        if (checkoutButton != null)
            checkoutButton.interactable = true;
    }
    
    private void CreateNewCartItemUI()
    {
        if (cartItemPrefab == null || cartItemsContainer == null)
        {
            Debug.LogError("Cart item prefab or container not assigned!");
            return;
        }
        
        GameObject newItem = Instantiate(cartItemPrefab, cartItemsContainer);
        CartItemUI itemUI = newItem.GetComponent<CartItemUI>();
        
        if (itemUI != null)
        {
            cartItemUIs.Add(itemUI);
        }
        else
        {
            Debug.LogError("CartItemUI component not found on prefab!");
        }
    }
    
    private void SetItemPosition(GameObject item, int index)
    {
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        if (rectTransform == null) return;
        
        // 设置锚点为左上角
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(0, 1);
        
        // 计算位置：从容器顶部开始，向下排列
        float itemHeight = rectTransform.rect.height > 0 ? rectTransform.rect.height : 120f;
        float yPosition = -containerTopMargin - (index * (itemHeight + itemSpacing));
        
        rectTransform.anchoredPosition = new Vector2(0, yPosition);
        rectTransform.sizeDelta = new Vector2(0, itemHeight);
    }
    
    private void UpdateTotalPrice()
    {
        if (totalPriceText != null && shoppingCart != null)
        {
            float total = shoppingCart.GetTotalPrice();
            totalPriceText.text = $"总计: ¥{total:F2}";
        }
    }
    
    private void OnBackToStoreClicked()
    {
        Debug.Log("返回商城页面");
        
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadStoreScene();
        }
        else
        {
            Debug.LogError("GameSceneManager not found!");
        }
    }
    
    private void OnCheckoutClicked()
    {
        if (shoppingCart == null || shoppingCart.GetCartCount() == 0)
        {
            Debug.Log("购物车为空，无法结算");
            return;
        }
        
        Debug.Log("跳转到结算页面");
        
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadCheckoutScene();
        }
        else
        {
            Debug.LogError("GameSceneManager not found!");
        }
    }
    
    // 公共方法，用于外部调用更新显示
    public void RefreshCartDisplay()
    {
        UpdateCartDisplay();
    }
    
    // 调试方法
    [ContextMenu("重新排列购物车项目")]
    public void RearrangeCartItems()
    {
        if (shoppingCart == null) return;
        
        var cartItems = shoppingCart.GetCartItems();
        for (int i = 0; i < cartItems.Count && i < cartItemUIs.Count; i++)
        {
            if (cartItemUIs[i] != null && cartItemUIs[i].gameObject.activeInHierarchy)
            {
                SetItemPosition(cartItemUIs[i].gameObject, i);
            }
        }
        
        Debug.Log("购物车项目重新排列完成");
    }
}



