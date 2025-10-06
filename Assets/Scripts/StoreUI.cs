using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Data.Common;

public class StoreUI : MonoBehaviour
{
    [Header("Game Cards")]
    [SerializeField] private List<GameCard> gameCards = new List<GameCard>();
    
    [Header("Navigation Buttons")]
    [SerializeField] private Button cartButton;
    [SerializeField] private Button libraryButton;
    
    [Header("Cart Button UI")]
    [SerializeField] private Text cartCountText; // 显示购物车中商品数量
    
    [Header("Sample Game Data (for testing)")]
    [SerializeField] private List<Sprite> sampleGameIcons = new List<Sprite>();
    
    [Header("Scene Names")]
    [SerializeField] private string cartSceneName = "CartScene";
    [SerializeField] private string librarySceneName = "LibraryScene";
    
    private void Start()
    {
        Debug.Log("StoreUI Start() called");
        InitializeButtons();
        Debug.Log("InitializeButtons completed");
        
        // 使用协程延迟执行SetupGames，确保GameManager先完成初始化
        StartCoroutine(DelayedSetupGames());
        
        UpdateCartCount();
        Debug.Log("UpdateCartCount completed");

        GameLibrary.OnLibraryChanged += RefreshGameCards;
        ShoppingCart.OnCartChanged += RefreshGameCards;
        Debug.Log("StoreUI Start() finished");
    }
    
    private System.Collections.IEnumerator DelayedSetupGames()
    {
        // 等待一帧，确保GameManager.Start()执行完毕
        yield return null;
        
        // 如果GameManager还没有准备好，继续等待
        while (GameManager.Instance == null || GameManager.Instance.currentGameDefinitions == null || GameManager.Instance.currentGameDefinitions.Count == 0)
        {
            Debug.Log("Waiting for GameManager to initialize...");
            yield return new WaitForSeconds(0.1f);
        }
        
        Debug.Log("GameManager ready, setting up games");
        SetupGames();
        Debug.Log("DelayedSetupGames completed");
    }
    
    private void OnDestroy()
    {
        // 取消监听
        GameLibrary.OnLibraryChanged -= RefreshGameCards;
        ShoppingCart.OnCartChanged -= RefreshGameCards;
    }
    
    public bool Purchase(GameData game)
    {
        float price = game.originalPrice * game.discount; 
        Debug.Log($"original price for ${game.originalPrice}!");
        bool success = CurrencyManager.Instance.Spend(price);
        CurrencyDisplay.Instance.UpdateTextBalance();
        if (success)
        {
            Debug.Log($"Purchased {game.id} for ${price}!");
            return true;
        }
        else
        {
            Debug.Log("Not enough balance!");
            return false;
        }
    }
    private void InitializeButtons()
    {
        // 设置购物车按钮
        if (cartButton != null)
        {
            cartButton.onClick.RemoveAllListeners();
            cartButton.onClick.AddListener(OpenShoppingCart);
        }

        // 设置游戏库按钮
        if (libraryButton != null)
        {
            libraryButton.onClick.RemoveAllListeners();
            libraryButton.onClick.AddListener(OpenGameLibrary);
        }
    }

    private void SetupGames()
    {
        Debug.Log("SetupGames called");
        
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null!");
            return;
        }
        
        if (GameManager.Instance.currentGameDefinitions != null)
        {
            List<GameDefinition> currentGameDefinitions = GameManager.Instance.currentGameDefinitions;
            List<GameData> currentGames = new List<GameData>();
            Debug.Log($"Found {currentGameDefinitions.Count} game definitions");
            
            for (int i = 0; i < currentGameDefinitions.Count; i++)
            {
                int id = parseIdToInt(currentGameDefinitions[i].Id);
                string title = currentGameDefinitions[i].DisplayName;
                Sprite coverArt = currentGameDefinitions[i].CoverArt ? currentGameDefinitions[i].CoverArt : null;
                float rating = currentGameDefinitions[i].Quality;
                float originalPrice = (float)(currentGameDefinitions[i].BasePriceCents);
                float discount = currentGameDefinitions[i].Discount;
                string type = currentGameDefinitions[i].Type;
                currentGames.Add(new GameData(id, title, coverArt, rating, originalPrice, discount, type));
                Debug.Log($"Created GameData: {title} (ID: {id})");
            }
            
            Debug.Log($"Created {currentGames.Count} GameData objects");
            Debug.Log($"Have {gameCards.Count} GameCard objects");
            
            for (int i = 0; i < gameCards.Count && i < currentGames.Count; i++)
            {
                if (gameCards[i] != null)
                {
                    Debug.Log($"Setting GameData for GameCard {i}: {currentGames[i].title}");
                    gameCards[i].SetGameData(currentGames[i]);
                }
                else
                {
                    Debug.LogWarning($"GameCard {i} is null!");
                }
            }
        }
        else
        {
            Debug.LogError("GameManager.Instance.currentGameDefinitions is null!");
            return;
        }
    }

    private int parseIdToInt(string id)
    {
        return int.Parse(id.Substring(3, 2));
    }
    
    public void UpdateCartCount()
    {
        if (cartCountText != null && ShoppingCart.Instance != null)
        {
            int cartCount = ShoppingCart.Instance.GetCartCount();
            cartCountText.text = cartCount.ToString();
        }
    }
    
    private void OpenShoppingCart()
    {
        // 跳转到购物车场景
        Debug.Log("跳转到购物车界面");
        
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadCartScene();
        }
        else
        {
            Debug.LogError("GameSceneManager not found!");
        }
    }
    
    private void OpenGameLibrary()
    {
        // 跳转到游戏库场景
        Debug.Log("跳转到游戏库界面");
        
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadLibraryScene();
        }
        else
        {
            Debug.LogError("GameSceneManager not found!");
        }
    }
    
    // 这个方法可以被外部调用来更新游戏数据
    public void UpdateGameData(int cardIndex, GameData newGameData)
    {
        if (cardIndex >= 0 && cardIndex < gameCards.Count && gameCards[cardIndex] != null)
        {
            gameCards[cardIndex].SetGameData(newGameData);
        }
    }
    
    // 这个方法可以被合作同学的代码调用来设置所有游戏数据
    public void SetAllGameData(List<GameData> gamesData)
    {
        for (int i = 0; i < gameCards.Count && i < gamesData.Count; i++)
        {
            if (gameCards[i] != null)
            {
                gameCards[i].SetGameData(gamesData[i]);
            }
        }
    }
    
    private void OnEnable()
    {
        StoreSessionTimer.OnSwapTick += HandleSwapTick;
        HandleSwapTick();
        // 当界面激活时更新购物车计数
        UpdateCartCount();
        
        // 刷新余额显示
        if (CurrencyDisplay.Instance != null)
        {
            CurrencyDisplay.Instance.RefreshBalanceDisplay();
        }
    }

    void OnDisable()
    {
        StoreSessionTimer.OnSwapTick -= HandleSwapTick;
    }

    private void HandleSwapTick()
    {
        Debug.Log("Swap tick received — refreshing displayed games");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PickRandomGames(3);
            SetupGames();
        }
    }
    
    private void RefreshGameCards()
    {
        // 刷新所有GameCard的购买状态
        foreach (var gameCard in gameCards)
        {
            if (gameCard != null)
            {
                gameCard.RefreshPurchaseStatus();
            }
        }

        // 更新购物车计数
        UpdateCartCount();
    }
}


