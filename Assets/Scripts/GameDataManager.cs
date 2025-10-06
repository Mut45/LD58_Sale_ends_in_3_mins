using UnityEngine;
using System.Collections.Generic;

// 这个类用于管理游戏数据，可以被合作同学的代码使用
public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
    
    [SerializeField] private List<GameData> allGamesData = new List<GameData>();
    
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
    
    // 添加游戏数据
    public void AddGameData(GameData gameData)
    {
        allGamesData.Add(gameData);
    }
    
    // 根据ID获取游戏数据
    public GameData GetGameDataById(int id)
    {
        return allGamesData.Find(game => game.id == id);
    }
    
    // 获取所有游戏数据
    public List<GameData> GetAllGamesData()
    {
        return new List<GameData>(allGamesData);
    }
    
    // 更新游戏数据
    public void UpdateGameData(int id, GameData updatedGameData)
    {
        int index = allGamesData.FindIndex(game => game.id == id);
        if (index != -1)
        {
            allGamesData[index] = updatedGameData;
        }
    }
    
    // 清空所有游戏数据
    public void ClearAllGameData()
    {
        allGamesData.Clear();
    }
}



