using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LibraryPageUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button backToStoreButton;     // 返回商城按钮
    [SerializeField] private Text libraryTitleText;        // 游戏库标题
    [SerializeField] private Text gameCountText;          // 游戏数量显示
    
    [Header("游戏展示区域")]
    [SerializeField] private Transform gamesContainer;     // 游戏容器
    [SerializeField] private GameObject libraryItemPrefab; // 游戏库项目预制体
    [SerializeField] private ScrollRect scrollRect;        // 滚动视图
    
    [Header("布局设置")]
    [SerializeField] private int itemsPerRow = 5;         // 每行显示的游戏数量
    [SerializeField] private int itemsPerPage = 10;       // 每页显示的游戏数量
    [SerializeField] private float itemSpacing = 10f;     // 项目间隔
    [SerializeField] private Vector2 itemSize = new Vector2(150, 200); // 项目大小
    
    [Header("场景名称")]
    [SerializeField] private string storeSceneName = "Shop_Page";
    
    private List<LibraryItemUI> libraryItemUIs = new List<LibraryItemUI>();
    private GameLibrary gameLibrary;
    
    private void Start()
    {
        gameLibrary = GameLibrary.Instance;
        if (gameLibrary == null)
        {
            Debug.LogError("GameLibrary instance not found!");
            return;
        }
        
        InitializeUI();
        UpdateLibraryDisplay();
        
        // 监听游戏库变化
        GameLibrary.OnLibraryChanged += UpdateLibraryDisplay;
    }
    
    private void OnDestroy()
    {
        GameLibrary.OnLibraryChanged -= UpdateLibraryDisplay;
    }
    
    private void InitializeUI()
    {
        // 设置按钮事件
        if (backToStoreButton != null)
        {
            backToStoreButton.onClick.RemoveAllListeners();
            backToStoreButton.onClick.AddListener(OnBackToStoreClicked);
        }
        
        // 设置滚动视图
        if (scrollRect != null)
        {
            scrollRect.vertical = true;
            scrollRect.horizontal = false;
        }
        
        // 设置游戏容器的GridLayoutGroup
        SetupGridLayout();
    }
    
    private void SetupGridLayout()
    {
        if (gamesContainer == null) return;
        
        // 添加GridLayoutGroup组件
        GridLayoutGroup gridLayout = gamesContainer.GetComponent<GridLayoutGroup>();
        if (gridLayout == null)
        {
            gridLayout = gamesContainer.gameObject.AddComponent<GridLayoutGroup>();
        }
        
        // 配置GridLayoutGroup
        gridLayout.cellSize = itemSize;
        gridLayout.spacing = new Vector2(itemSpacing, itemSpacing);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = itemsPerRow;
        gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayout.childAlignment = TextAnchor.UpperLeft;
    }
    
    private void UpdateLibraryDisplay()
    {
        if (gameLibrary == null) return;
        
        var ownedGames = gameLibrary.GetOwnedGames();
        Debug.Log($"游戏库中有 {ownedGames.Count} 个游戏");
        
        // 更新游戏数量显示
        if (gameCountText != null)
        {
            gameCountText.text = $"已拥有 {ownedGames.Count} 款游戏";
        }
        
        // 确保有足够的UI项目
        while (libraryItemUIs.Count < ownedGames.Count)
        {
            CreateNewLibraryItemUI();
        }
        
        // 更新现有UI项目
        for (int i = 0; i < ownedGames.Count; i++)
        {
            if (libraryItemUIs[i] != null)
            {
                libraryItemUIs[i].SetGameData(ownedGames[i]);
                libraryItemUIs[i].gameObject.SetActive(true);
            }
        }
        
        // 隐藏多余的UI项目
        for (int i = ownedGames.Count; i < libraryItemUIs.Count; i++)
        {
            if (libraryItemUIs[i] != null)
            {
                libraryItemUIs[i].gameObject.SetActive(false);
            }
        }
        
        // 更新滚动视图
        UpdateScrollView(ownedGames.Count);
    }
    
    private void CreateNewLibraryItemUI()
    {
        if (libraryItemPrefab == null || gamesContainer == null)
        {
            Debug.LogError("Library item prefab or container not assigned!");
            return;
        }
        
        GameObject newItem = Instantiate(libraryItemPrefab, gamesContainer);
        LibraryItemUI itemUI = newItem.GetComponent<LibraryItemUI>();
        
        if (itemUI != null)
        {
            libraryItemUIs.Add(itemUI);
        }
        else
        {
            Debug.LogError("LibraryItemUI component not found on prefab!");
        }
    }
    
    private void UpdateScrollView(int gameCount)
    {
        if (scrollRect == null) return;
        
        // 计算是否需要滚动
        int totalRows = Mathf.CeilToInt((float)gameCount / itemsPerRow);
        float contentHeight = totalRows * (itemSize.y + itemSpacing) + itemSpacing;
        
        // 设置Content的大小
        RectTransform contentRect = scrollRect.content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);
        }
        
        // 如果游戏数量超过每页限制，启用滚动
        bool needsScroll = gameCount > itemsPerPage;
        scrollRect.verticalScrollbar.gameObject.SetActive(needsScroll);
        
        Debug.Log($"游戏库滚动设置: 总游戏数={gameCount}, 总行数={totalRows}, 内容高度={contentHeight}, 需要滚动={needsScroll}");
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
    
    // 公共方法，用于外部调用更新显示
    public void RefreshLibraryDisplay()
    {
        UpdateLibraryDisplay();
    }
    
    // 调试方法
    [ContextMenu("刷新游戏库显示")]
    public void ForceRefreshLibrary()
    {
        Debug.Log("强制刷新游戏库显示");
        UpdateLibraryDisplay();
    }
    
    [ContextMenu("打印游戏库信息")]
    public void PrintLibraryInfo()
    {
        if (gameLibrary == null)
        {
            Debug.Log("GameLibrary instance not found");
            return;
        }
        
        var ownedGames = gameLibrary.GetOwnedGames();
        Debug.Log("=== 游戏库信息 ===");
        Debug.Log($"已拥有游戏数量: {ownedGames.Count}");
        
        for (int i = 0; i < ownedGames.Count; i++)
        {
            var game = ownedGames[i];
            Debug.Log($"{i + 1}. {game.title} (ID: {game.id})");
        }
        Debug.Log("================");
    }
}


