using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class CartPageUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject emptyCartPanel;
    [SerializeField] private Text emptyCartText;
    [SerializeField] private Button continueShoppingButton;
    
    [Header("Cart Items Container")]
    [SerializeField] private Transform cartItemsContainer;
    [SerializeField] private GameObject cartItemPrefab;
    
    [Header("Order Summary")]
    [SerializeField] private Text estimatedTotalText;
    [SerializeField] private Button proceedToCheckoutButton;
    
    [Header("注意：按钮位置现在保持固定")]
    [SerializeField] private string layoutNote = "按钮位置不再自动移动，请在Unity编辑器中手动设置按钮位置";
    
    [Header("Scene Names")]
    [SerializeField] private string storeSceneName = "StoreScene";
    [SerializeField] private string checkoutSceneName = "CheckoutScene";
    
    private List<CartItemUI> cartItemUIs = new List<CartItemUI>();
    private ShoppingCart shoppingCart;
    public ShoppingAudioTrigger shoppingAudioTrigger;
    
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
        // 取消监听
        ShoppingCart.OnCartChanged -= UpdateCartDisplay;
    }
    

    
    private void InitializeUI()
    {
        // 设置继续购物按钮点击事件
        if (continueShoppingButton != null)
        {
            continueShoppingButton.onClick.RemoveAllListeners();
            continueShoppingButton.onClick.AddListener(OnContinueShoppingClicked);
        }
        
        // 设置跳转支付按钮点击事件
        if (proceedToCheckoutButton != null)
        {
            
            proceedToCheckoutButton.onClick.RemoveAllListeners();
            proceedToCheckoutButton.onClick.AddListener(OnProceedToCheckoutClicked);


        }
        
        // 确保按钮始终显示
        if (continueShoppingButton != null)
            continueShoppingButton.gameObject.SetActive(true);
        
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
        
        UpdateEstimatedTotal();
    }
    
    private void ShowEmptyCart()
    {
        // 显示空购物车状态
        if (emptyCartPanel != null)
            emptyCartPanel.SetActive(true);
        
        // 隐藏所有购物车项目
        foreach (var itemUI in cartItemUIs)
        {
            if (itemUI != null)
                itemUI.gameObject.SetActive(false);
        }
        
        // 确保返回商城按钮始终显示
        if (continueShoppingButton != null)
            continueShoppingButton.gameObject.SetActive(true);
        
        // 禁用结算按钮（空购物车时）
        if (proceedToCheckoutButton != null)
            proceedToCheckoutButton.interactable = false;
    }
    
    private void ShowCartItems(List<GameData> cartItems)
    {
        // 隐藏空购物车提示
        if (emptyCartPanel != null)
            emptyCartPanel.SetActive(false);
        
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
            }
        }
        
        // 强制刷新布局（VerticalLayoutGroup会自动排列）
        LayoutRebuilder.ForceRebuildLayoutImmediate(cartItemsContainer.GetComponent<RectTransform>());
        
        // 隐藏多余的UI项目
        for (int i = cartItems.Count; i < cartItemUIs.Count; i++)
        {
            if (cartItemUIs[i] != null)
                cartItemUIs[i].gameObject.SetActive(false);
        }
        
        // 确保返回商城按钮始终显示
        if (continueShoppingButton != null)
            continueShoppingButton.gameObject.SetActive(true);
        
        // 启用结算按钮（有商品时）
        if (proceedToCheckoutButton != null)
            proceedToCheckoutButton.interactable = true;
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
            // 使用VerticalLayoutGroup自动布局，不需要手动设置位置
            cartItemUIs.Add(itemUI);
            
            // 强制刷新布局
            LayoutRebuilder.ForceRebuildLayoutImmediate(cartItemsContainer.GetComponent<RectTransform>());
        }
        else
        {
            Debug.LogError("CartItemUI component not found on prefab!");
        }
    }
    
    // SetupCartItemLayout方法已移除，现在使用VerticalLayoutGroup自动布局
    
    // UpdateContinueShoppingButtonPosition方法已移除，按钮位置现在保持固定
    
    // GetLastCartItemYPosition方法已移除，按钮位置现在保持固定
    
    private void UpdateEstimatedTotal()
    {
        if (estimatedTotalText == null || shoppingCart == null) return;
        
        float total = shoppingCart.GetTotalPrice();
        estimatedTotalText.text = $"Estimated Total:{total*0.01:F2}$";
        
        // 根据是否有物品来启用/禁用支付按钮
        if (proceedToCheckoutButton != null)
        {
            proceedToCheckoutButton.interactable = total > 0;
        }
    }
    
    private void OnContinueShoppingClicked()
    {
        // 返回商城页面
        Debug.Log("返回商城页面 - 按钮被点击");
        
        if (GameSceneManager.Instance != null)
        {
            Debug.Log("GameSceneManager found, 正在加载商城场景...");
            GameSceneManager.Instance.LoadStoreScene();
        }
        else
        {
            Debug.LogError("GameSceneManager not found! 请确保场景中有GameSceneManager对象");
        }
    }
    
    private void OnProceedToCheckoutClicked()
    {
        if (shoppingCart == null || shoppingCart.GetCartCount() == 0)
        {
            Debug.Log("购物车为空，无法支付");
            return;
        }
        
        // 直接购买所有游戏（简化流程，实际项目中可以跳转到支付页面）
        Debug.Log("开始购买所有游戏");
        
        shoppingCart.PurchaseAllGames();
        
        // 显示购买成功提示
        Debug.Log("购买完成！游戏已添加到游戏库");
        if (shoppingAudioTrigger != null)
        {
            shoppingAudioTrigger.TriggerCartPurchaseSound();
        }
        // 可以选择跳转到游戏库页面
        // if (GameSceneManager.Instance != null)
        // {
        //     GameSceneManager.Instance.LoadLibraryScene();
        // }
    }
    
    // 公共方法，用于外部调用更新显示
    public void RefreshCartDisplay()
    {
        UpdateCartDisplay();
    }
    
    // 重新排列所有购物车项目
    [ContextMenu("重新排列购物车项目")]
    public void RearrangeCartItems()
    {
        Debug.Log("开始重新排列购物车项目...");
        
        if (shoppingCart == null) return;
        
        // 使用VerticalLayoutGroup，只需要强制刷新布局
        LayoutRebuilder.ForceRebuildLayoutImmediate(cartItemsContainer.GetComponent<RectTransform>());
        
        //UpdateContinueShoppingButtonPosition(shoppingCart.GetCartCount() == 0);
        Debug.Log("购物车项目重新排列完成");
    }
}
