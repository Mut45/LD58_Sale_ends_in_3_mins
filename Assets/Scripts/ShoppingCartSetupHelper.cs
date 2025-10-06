using UnityEngine;

// 购物车设置助手 - 帮助快速设置购物车系统
public class ShoppingCartSetupHelper : MonoBehaviour
{
    [Header("自动设置选项")]
    [SerializeField] private bool autoCreateShoppingCart = true;
    [SerializeField] private bool autoCreateGameSceneManager = true;
    [SerializeField] private bool autoCreateGameLibrary = true;
    [SerializeField] private bool autoCreateGameDataManager = true;
    
    [Header("场景名称配置")]
    [SerializeField] private string storeSceneName = "Shop_Page";
    [SerializeField] private string cartSceneName = "Cart_Page";
    [SerializeField] private string librarySceneName = "Library_Page";
    
    [ContextMenu("设置购物车系统")]
    public void SetupShoppingCartSystem()
    {
        Debug.Log("开始设置购物车系统...");
        
        // 检查并创建必要的管理器
        CheckAndCreateShoppingCart();
        CheckAndCreateGameSceneManager();
        CheckAndCreateGameLibrary();
        CheckAndCreateGameDataManager();
        
        Debug.Log("购物车系统设置完成！");
        PrintSystemStatus();
    }
    
    private void CheckAndCreateShoppingCart()
    {
        if (ShoppingCart.Instance == null)
        {
            if (autoCreateShoppingCart)
            {
                GameObject shoppingCartObj = new GameObject("ShoppingCart");
                shoppingCartObj.AddComponent<ShoppingCart>();
                Debug.Log("✅ 已创建 ShoppingCart 对象");
            }
            else
            {
                Debug.LogError("❌ ShoppingCart.Instance 为空！请在场景中创建 ShoppingCart 对象。");
            }
        }
        else
        {
            Debug.Log("✅ ShoppingCart 已存在");
        }
    }
    
    private void CheckAndCreateGameSceneManager()
    {
        if (GameSceneManager.Instance == null)
        {
            if (autoCreateGameSceneManager)
            {
                GameObject sceneManagerObj = new GameObject("GameSceneManager");
                GameSceneManager sceneManager = sceneManagerObj.AddComponent<GameSceneManager>();
                
                // 设置场景名称
                var sceneManagerType = typeof(GameSceneManager);
                var storeSceneField = sceneManagerType.GetField("storeSceneName", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var cartSceneField = sceneManagerType.GetField("cartSceneName", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var librarySceneField = sceneManagerType.GetField("librarySceneName", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (storeSceneField != null) storeSceneField.SetValue(sceneManager, storeSceneName);
                if (cartSceneField != null) cartSceneField.SetValue(sceneManager, cartSceneName);
                if (librarySceneField != null) librarySceneField.SetValue(sceneManager, librarySceneName);
                
                Debug.Log("✅ 已创建 GameSceneManager 对象");
            }
            else
            {
                Debug.LogError("❌ GameSceneManager.Instance 为空！请在场景中创建 GameSceneManager 对象。");
            }
        }
        else
        {
            Debug.Log("✅ GameSceneManager 已存在");
        }
    }
    
    private void CheckAndCreateGameLibrary()
    {
        if (GameLibrary.Instance == null)
        {
            if (autoCreateGameLibrary)
            {
                GameObject libraryObj = new GameObject("GameLibrary");
                libraryObj.AddComponent<GameLibrary>();
                Debug.Log("✅ 已创建 GameLibrary 对象");
            }
            else
            {
                Debug.LogError("❌ GameLibrary.Instance 为空！请在场景中创建 GameLibrary 对象。");
            }
        }
        else
        {
            Debug.Log("✅ GameLibrary 已存在");
        }
    }
    
    private void CheckAndCreateGameDataManager()
    {
        if (GameDataManager.Instance == null)
        {
            if (autoCreateGameDataManager)
            {
                GameObject dataManagerObj = new GameObject("GameDataManager");
                dataManagerObj.AddComponent<GameDataManager>();
                Debug.Log("✅ 已创建 GameDataManager 对象");
            }
            else
            {
                Debug.LogError("❌ GameDataManager.Instance 为空！请在场景中创建 GameDataManager 对象。");
            }
        }
        else
        {
            Debug.Log("✅ GameDataManager 已存在");
        }
    }
    
    [ContextMenu("检查系统状态")]
    public void PrintSystemStatus()
    {
        Debug.Log("=== 购物车系统状态检查 ===");
        Debug.Log($"ShoppingCart.Instance: {(ShoppingCart.Instance != null ? "✅ 存在" : "❌ 为空")}");
        Debug.Log($"GameSceneManager.Instance: {(GameSceneManager.Instance != null ? "✅ 存在" : "❌ 为空")}");
        Debug.Log($"GameLibrary.Instance: {(GameLibrary.Instance != null ? "✅ 存在" : "❌ 为空")}");
        Debug.Log($"GameDataManager.Instance: {(GameDataManager.Instance != null ? "✅ 存在" : "❌ 为空")}");
        
        if (ShoppingCart.Instance != null)
        {
            Debug.Log($"购物车当前状态: {ShoppingCart.Instance.GetCartCount()} 个游戏");
        }
        Debug.Log("========================");
    }
    
    [ContextMenu("测试购物车功能")]
    public void TestShoppingCartFunction()
    {
        if (ShoppingCart.Instance == null)
        {
            Debug.LogError("❌ 无法测试：ShoppingCart.Instance 为空！");
            return;
        }
        
        // 创建测试游戏数据
        GameData testGame = new GameData(999, "测试游戏", null, 5.0f, 100f, 20f, "测试类型");
        
        Debug.Log("开始测试购物车功能...");
        
        // 测试添加
        bool addResult = ShoppingCart.Instance.AddToCart(testGame);
        Debug.Log($"添加测试: {(addResult ? "✅ 成功" : "❌ 失败")}");
        
        // 测试移除
        bool removeResult = ShoppingCart.Instance.RemoveFromCart(testGame);
        Debug.Log($"移除测试: {(removeResult ? "✅ 成功" : "❌ 失败")}");
        
        Debug.Log("购物车功能测试完成！");
    }
}



