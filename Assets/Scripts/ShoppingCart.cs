using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShoppingCart : MonoBehaviour
{
    public static ShoppingCart Instance { get; private set; }
    
    [Header("Cart Settings")]
    [SerializeField] private int maxCartItems = 10; // 购物车最大容量
    
    private List<GameData> cartItems = new List<GameData>();
    
    // 购物车变化事件
    public static System.Action OnCartChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // 添加游戏到购物车
    public bool AddToCart(GameData gameData)
    {
        if (gameData == null)
        {
            Debug.LogWarning("尝试添加空的游戏数据到购物车");
            return false;
        }
        
        // 注释掉重复检查，允许重复添加到购物车
        // 检查游戏是否已在购物车中
        // if (IsInCart(gameData.id))
        // {
        //     Debug.LogWarning($"游戏 {gameData.title} 已在购物车中，无法重复添加");
        //     return false;
        // }
        
        // 注释掉已购买检查，允许已购买的游戏添加到购物车
        // 检查游戏是否已购买
        // if (GameLibrary.Instance != null && GameLibrary.Instance.HasGame(gameData.id))
        // {
        //     Debug.LogWarning($"游戏 {gameData.title} 已经购买过了，无法添加到购物车");
        //     return false;
        // }
        
        // 检查购物车是否已满
        if (cartItems.Count >= maxCartItems)
        {
            Debug.LogWarning($"购物车已满，最多只能添加 {maxCartItems} 个游戏");
            return false;
        }
        
        // 添加到购物车
        cartItems.Add(gameData);
        Debug.Log($"游戏已添加到购物车: {gameData.title} (购物车中有 {cartItems.Count} 个游戏)");
        
        // 触发购物车变化事件
        OnCartChanged?.Invoke();
        
        return true;
    }
    
    // 从购物车移除游戏（移除第一个匹配的游戏）
    public bool RemoveFromCart(GameData gameData)
    {
        if (gameData == null)
        {
            Debug.LogWarning("尝试从购物车移除空的游戏数据");
            return false;
        }
        
        var itemToRemove = cartItems.FirstOrDefault(item => item.id == gameData.id);
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
            Debug.Log($"游戏已从购物车移除: {gameData.title} (购物车中有 {cartItems.Count} 个游戏)");
            
            // 触发购物车变化事件
            OnCartChanged?.Invoke();
            return true;
        }
        
        Debug.LogWarning($"游戏 {gameData.title} 不在购物车中");
        return false;
    }
    
    // 从购物车移除指定索引的游戏
    public bool RemoveFromCartByIndex(int index)
    {
        if (index >= 0 && index < cartItems.Count)
        {
            var removedItem = cartItems[index];
            cartItems.RemoveAt(index);
            Debug.Log($"游戏已从购物车移除: {removedItem.title} (索引: {index})");
            
            // 触发购物车变化事件
            OnCartChanged?.Invoke();
            return true;
        }
        
        Debug.LogWarning($"无效的索引: {index}，购物车中只有 {cartItems.Count} 个游戏");
        return false;
    }
    
    // 清空购物车
    public void ClearCart()
    {
        int itemCount = cartItems.Count;
        cartItems.Clear();
        Debug.Log($"购物车已清空，移除了 {itemCount} 个游戏");
        
        // 触发购物车变化事件
        OnCartChanged?.Invoke();
    }
    
    // 购买购物车中的所有游戏
    public void PurchaseAllGames()
    {
        if (cartItems.Count == 0)
        {
            Debug.LogWarning("购物车为空，无法购买");
            return;
        }
        
        // 将所有游戏添加到游戏库
        if (GameLibrary.Instance != null)
        {
            foreach (var game in cartItems)
            {
                GameLibrary.Instance.AddGame(game);
            }
            
            Debug.Log($"成功购买 {cartItems.Count} 个游戏并添加到游戏库");
        }
        else
        {
            Debug.LogError("GameLibrary instance not found!");
        }
        
        // 清空购物车
        ClearCart();
    }
    
    // 获取购物车中的游戏列表
    public List<GameData> GetCartItems()
    {
        return new List<GameData>(cartItems);
    }
    
    // 获取购物车中游戏数量
    public int GetCartCount()
    {
        return cartItems.Count;
    }
    
    // 计算购物车总价
    public float GetTotalPrice()
    {
        float total = 0f;
        foreach (var item in cartItems)
        {
            total += item.FinalPrice;
        }
        return total;
    }
    
    // 检查游戏是否在购物车中
    public bool IsInCart(int gameId)
    {
        return cartItems.Any(item => item.id == gameId);
    }
    
    // 检查购物车是否为空
    public bool IsEmpty()
    {
        return cartItems.Count == 0;
    }
    
    // 检查购物车是否已满
    public bool IsFull()
    {
        return cartItems.Count >= maxCartItems;
    }
    
    // 获取购物车容量信息
    public string GetCartCapacityInfo()
    {
        return $"{cartItems.Count}/{maxCartItems}";
    }
    
    // 获取特定游戏的数量
    public int GetGameCount(int gameId)
    {
        return cartItems.Count(item => item.id == gameId);
    }
    
    // 获取购物车统计信息
    public Dictionary<int, int> GetCartStatistics()
    {
        var stats = new Dictionary<int, int>();
        foreach (var item in cartItems)
        {
            if (stats.ContainsKey(item.id))
                stats[item.id]++;
            else
                stats[item.id] = 1;
        }
        return stats;
    }
    
    // 调试方法：打印购物车内容
    [ContextMenu("Print Cart Contents")]
    public void PrintCartContents()
    {
        Debug.Log("=== 购物车内容 ===");
        if (cartItems.Count == 0)
        {
            Debug.Log("购物车为空");
        }
        else
        {
            for (int i = 0; i < cartItems.Count; i++)
            {
                var item = cartItems[i];
                Debug.Log($"{i + 1}. {item.title} - ¥{item.FinalPrice:F2}");
            }
            Debug.Log($"总计: ¥{GetTotalPrice():F2}");
            
            // 显示统计信息
            var stats = GetCartStatistics();
            Debug.Log("--- 游戏统计 ---");
            foreach (var stat in stats)
            {
                var gameData = cartItems.First(item => item.id == stat.Key);
                Debug.Log($"{gameData.title}: {stat.Value} 个");
            }
        }
        Debug.Log("================");
    }
}
