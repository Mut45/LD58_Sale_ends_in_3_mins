using UnityEngine;
using UnityEngine.UI;

// 购买功能测试助手
public class PurchaseTestHelper : MonoBehaviour
{
    [Header("Test UI")]
    [SerializeField] private Button testPurchaseButton;
    [SerializeField] private Button testAddToCartButton;
    [SerializeField] private Button testCheckLibraryButton;
    [SerializeField] private Text statusText;
    
    [Header("Test Game Data")]
    [SerializeField] private Sprite testGameIcon;
    
    private GameData testGame;
    
    private void Start()
    {
        InitializeTestGame();
        SetupTestButtons();
        UpdateDisplay();
    }
    
    private void InitializeTestGame()
    {
        // 创建测试游戏数据
        testGame = new GameData(
            id: 888,
            title: "测试购买游戏",
            gameIcon: testGameIcon,
            rating: 5.0f,
            originalPrice: 50f,
            discount: 10f,
            type: "测试类型"
        );
    }
    
    private void SetupTestButtons()
    {
        if (testPurchaseButton != null)
        {
            testPurchaseButton.onClick.RemoveAllListeners();
            testPurchaseButton.onClick.AddListener(TestDirectPurchase);
        }
        
        if (testAddToCartButton != null)
        {
            testAddToCartButton.onClick.RemoveAllListeners();
            testAddToCartButton.onClick.AddListener(TestAddToCart);
        }
        
        if (testCheckLibraryButton != null)
        {
            testCheckLibraryButton.onClick.RemoveAllListeners();
            testCheckLibraryButton.onClick.AddListener(TestCheckLibrary);
        }
    }
    
    private void UpdateDisplay()
    {
        if (statusText != null)
        {
            string info = "=== 购买测试状态 ===\n";
            
            if (GameLibrary.Instance != null)
            {
                int libraryCount = GameLibrary.Instance.GetOwnedGames().Count;
                info += $"游戏库数量: {libraryCount}\n";
                
                bool hasGame = GameLibrary.Instance.HasGame(testGame.id);
                info += $"测试游戏是否已购买: {hasGame}\n";
            }
            else
            {
                info += "GameLibrary.Instance 为空\n";
            }
            
            if (ShoppingCart.Instance != null)
            {
                int cartCount = ShoppingCart.Instance.GetCartCount();
                info += $"购物车数量: {cartCount}\n";
                
                bool inCart = ShoppingCart.Instance.IsInCart(testGame.id);
                info += $"测试游戏是否在购物车: {inCart}\n";
            }
            else
            {
                info += "ShoppingCart.Instance 为空\n";
            }
            
            statusText.text = info;
        }
    }
    
    private void TestDirectPurchase()
    {
        if (GameLibrary.Instance != null)
        {
            GameLibrary.Instance.AddGame(testGame);
            Debug.Log("✅ 测试：直接购买游戏");
            UpdateDisplay();
        }
        else
        {
            Debug.LogError("❌ 测试：GameLibrary.Instance 为空");
        }
    }
    
    private void TestAddToCart()
    {
        if (ShoppingCart.Instance != null)
        {
            bool success = ShoppingCart.Instance.AddToCart(testGame);
            Debug.Log($"{(success ? "✅" : "❌")} 测试：添加到购物车 {(success ? "成功" : "失败")}");
            UpdateDisplay();
        }
        else
        {
            Debug.LogError("❌ 测试：ShoppingCart.Instance 为空");
        }
    }
    
    private void TestCheckLibrary()
    {
        UpdateDisplay();
        Debug.Log("=== 强制刷新状态显示 ===");
    }
}


