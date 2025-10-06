using UnityEngine;
using UnityEngine.UI;

// 购物车功能测试助手
public class CartTestHelper : MonoBehaviour
{
    [Header("Test UI")]
    [SerializeField] private Button testAddButton;
    [SerializeField] private Button testRemoveButton;
    [SerializeField] private Button testClearButton;
    [SerializeField] private Text cartStatusText;
    [SerializeField] private Text cartCountText;
    
    [Header("Test Game Data")]
    [SerializeField] private Sprite testGameIcon;
    
    private GameData testGame;
    
    private void Start()
    {
        InitializeTestGame();
        SetupTestButtons();
        UpdateDisplay();
        
        // 监听购物车变化
        ShoppingCart.OnCartChanged += UpdateDisplay;
    }
    
    private void OnDestroy()
    {
        ShoppingCart.OnCartChanged -= UpdateDisplay;
    }
    
    private void InitializeTestGame()
    {
        // 创建测试游戏数据
        testGame = new GameData(
            id: 999,
            title: "测试游戏",
            gameIcon: testGameIcon,
            rating: 5.0f,
            originalPrice: 100f,
            discount: 20f,
            type: "测试类型"
        );
    }
    
    private void SetupTestButtons()
    {
        if (testAddButton != null)
        {
            testAddButton.onClick.RemoveAllListeners();
            testAddButton.onClick.AddListener(TestAddToCart);
        }
        
        if (testRemoveButton != null)
        {
            testRemoveButton.onClick.RemoveAllListeners();
            testRemoveButton.onClick.AddListener(TestRemoveFromCart);
        }
        
        if (testClearButton != null)
        {
            testClearButton.onClick.RemoveAllListeners();
            testClearButton.onClick.AddListener(TestClearCart);
        }
    }
    
    private void UpdateDisplay()
    {
        if (ShoppingCart.Instance == null)
        {
            if (cartStatusText != null)
                cartStatusText.text = "购物车实例未找到";
            if (cartCountText != null)
                cartCountText.text = "0";
            return;
        }
        
        int count = ShoppingCart.Instance.GetCartCount();
        float total = ShoppingCart.Instance.GetTotalPrice();
        
        if (cartCountText != null)
            cartCountText.text = count.ToString();
        
        if (cartStatusText != null)
        {
            if (count == 0)
            {
                cartStatusText.text = "购物车为空";
            }
            else
            {
                cartStatusText.text = $"购物车中有 {count} 个游戏\n总价: ¥{total:F2}";
            }
        }
        
        // 更新按钮状态
        if (testRemoveButton != null)
            testRemoveButton.interactable = ShoppingCart.Instance.IsInCart(testGame.id);
        
        if (testClearButton != null)
            testClearButton.interactable = !ShoppingCart.Instance.IsEmpty();
    }
    
    private void TestAddToCart()
    {
        if (ShoppingCart.Instance != null)
        {
            bool success = ShoppingCart.Instance.AddToCart(testGame);
            if (success)
            {
                Debug.Log("✅ 测试：成功添加游戏到购物车");
            }
            else
            {
                Debug.Log("❌ 测试：添加游戏到购物车失败");
            }
        }
        else
        {
            Debug.LogError("❌ 测试：ShoppingCart.Instance 为空");
        }
    }
    
    private void TestRemoveFromCart()
    {
        if (ShoppingCart.Instance != null)
        {
            bool success = ShoppingCart.Instance.RemoveFromCart(testGame);
            if (success)
            {
                Debug.Log("✅ 测试：成功从购物车移除游戏");
            }
            else
            {
                Debug.Log("❌ 测试：从购物车移除游戏失败");
            }
        }
        else
        {
            Debug.LogError("❌ 测试：ShoppingCart.Instance 为空");
        }
    }
    
    private void TestClearCart()
    {
        if (ShoppingCart.Instance != null)
        {
            ShoppingCart.Instance.ClearCart();
            Debug.Log("✅ 测试：成功清空购物车");
        }
        else
        {
            Debug.LogError("❌ 测试：ShoppingCart.Instance 为空");
        }
    }
    
    // 在Inspector中显示当前购物车状态
    [ContextMenu("Print Cart Status")]
    public void PrintCartStatus()
    {
        if (ShoppingCart.Instance != null)
        {
            ShoppingCart.Instance.PrintCartContents();
        }
        else
        {
            Debug.LogError("ShoppingCart.Instance 为空");
        }
    }
}



