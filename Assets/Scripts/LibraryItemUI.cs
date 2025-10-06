using UnityEngine;
using UnityEngine.UI;

public class LibraryItemUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image gameIconImage;          // 游戏图标
    [SerializeField] private Text gameTitleText;           // 游戏标题
    
    [Header("Visual Settings")]
    [SerializeField] private Color normalTitleColor = Color.white;
    [SerializeField] private Color hoverTitleColor = Color.yellow;
    
    private GameData currentGameData;
    
    private void Start()
    {
        InitializeUI();
    }
    
    private void InitializeUI()
    {
        // 可以在这里添加悬停效果或其他初始化逻辑
    }
    
    public void SetGameData(GameData gameData)
    {
        currentGameData = gameData;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (currentGameData == null) return;
        
        // 更新游戏图标
        if (gameIconImage != null)
        {
            gameIconImage.sprite = currentGameData.gameIcon;
            
            // 如果没有图标，显示默认图标或隐藏
            if (gameIconImage.sprite == null)
            {
                gameIconImage.color = Color.gray;
            }
            else
            {
                gameIconImage.color = Color.white;
            }
        }
        
        // 更新游戏标题
        if (gameTitleText != null)
        {
            gameTitleText.text = currentGameData.title;
            gameTitleText.color = normalTitleColor;
        }
    }
    
    // 获取当前游戏数据
    public GameData GetGameData()
    {
        return currentGameData;
    }
    
    // 悬停效果（可选）
    public void OnPointerEnter()
    {
        if (gameTitleText != null)
        {
            gameTitleText.color = hoverTitleColor;
        }
    }
    
    public void OnPointerExit()
    {
        if (gameTitleText != null)
        {
            gameTitleText.color = normalTitleColor;
        }
    }
}


