using System.Collections.Generic;
using UnityEngine;

public class GameLibrary : MonoBehaviour
{
    public static GameLibrary Instance { get; private set; }
    
    [SerializeField] private List<GameData> ownedGames = new List<GameData>();
    
    // 游戏库变化事件
    public static System.Action OnLibraryChanged;
    
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
    
    public void AddGame(GameData game)
    {
        // 注释掉重复检查，允许重复添加相同游戏
        // if (!ownedGames.Contains(game))
        // {
            ownedGames.Add(game);
            Debug.Log($"游戏已添加到库中: {game.title}");
            
            // 触发游戏库变化事件
            OnLibraryChanged?.Invoke();
        // }
    }
    
    public List<GameData> GetOwnedGames()
    {
        return new List<GameData>(ownedGames);
    }
    
    public bool HasGame(int gameId)
    {
        return ownedGames.Exists(game => game.id == gameId);
    }
}



