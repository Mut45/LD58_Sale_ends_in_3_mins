using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LibraryStatsManager : MonoBehaviour
{
    [Header("UI Text References")]
    [SerializeField] private Text totalGamesText;
    [SerializeField] private Text totalOriginalPriceText;
    [SerializeField] private Text quality5GamesText;
    [SerializeField] private Text quality0GamesText;

    private void Start()
    {
        // 监听游戏库变化事件
        GameLibrary.OnLibraryChanged += UpdateStats;
        
        // 初始更新统计
        UpdateStats();
    }

    private void OnDestroy()
    {
        // 取消监听事件
        GameLibrary.OnLibraryChanged -= UpdateStats;
    }

    private void UpdateStats()
    {
        var library = GameLibrary.Instance;
        if (library == null)
        {
            Debug.LogWarning("GameLibrary instance not found");
            return;
        }

        var ownedGames = library.GetOwnedGames();
        if (ownedGames == null)
        {
            Debug.LogWarning("Owned games list is null");
            return;
        }

        // 统计各项数据
        int totalGames = ownedGames.Count;
        float totalOriginalPrice = 0f;
        int quality5Count = 0;
        int quality0Count = 0;

        foreach (var game in ownedGames)
        {
            // 累加原价（以元为单位），除以100
            totalOriginalPrice += game.originalPrice / 100f;

            // 统计quality为5和0的游戏数量
            if (game.rating >= 4.5f) // rating接近5表示quality为5
            {
                quality5Count++;
            }
            else if (game.rating <= 0.5f) // rating接近0表示quality为0
            {
                quality0Count++;
            }
        }

        // 更新UI显示
        if (totalGamesText != null)
            totalGamesText.text = totalGames.ToString();

        if (totalOriginalPriceText != null)
            totalOriginalPriceText.text = totalOriginalPrice.ToString("F2");

        if (quality5GamesText != null)
            quality5GamesText.text = quality5Count.ToString();

        if (quality0GamesText != null)
            quality0GamesText.text = quality0Count.ToString();

        Debug.Log($"Library Stats Updated - Total: {totalGames}, Price: {totalOriginalPrice:F2}, Q5: {quality5Count}, Q0: {quality0Count}");
    }

    // 公共方法：手动刷新统计
    public void RefreshStats()
    {
        UpdateStats();
    }

    // 公共方法：设置UI引用
    public void SetUITexts(Text totalGames, Text totalPrice, Text quality5, Text quality0)
    {
        totalGamesText = totalGames;
        totalOriginalPriceText = totalPrice;
        quality5GamesText = quality5;
        quality0GamesText = quality0;
        
        // 立即更新显示
        UpdateStats();
    }
}

