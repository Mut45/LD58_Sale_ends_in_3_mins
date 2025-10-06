using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameCatalog catalog;
    public List<GameDefinition> currentGameDefinitions = new();
    [SerializeField] private CurrencyManager currencyManager;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        currencyManager.Init(25000);
    }
    void Start()
    {
        if (catalog == null)
        {
            Debug.LogError("No GameCatalog assigned to GameManager!");
            return;
        }
        PickRandomGames(3);
        PrintPickedGames(); // optional debug
    }

    // Update is called once per frame
    public void PickRandomGames(int count)
    {
        currentGameDefinitions.Clear();
        var catalogSource = catalog.Games;
        if (catalogSource == null || catalogSource.Count == 0)
        {
            Debug.LogWarning("Catalog is empty.");
            return;
        }
        count = Mathf.Min(count, catalogSource.Count);
        List<GameDefinition> temp = new List<GameDefinition>(catalogSource);
        for (int i = 0; i < count; i++)
        {
            int randIndex = Random.Range(i, temp.Count);
            (temp[i], temp[randIndex]) = (temp[randIndex], temp[i]);
            currentGameDefinitions.Add(temp[i]);
        }
    }
    private void PrintPickedGames()
    {
        Debug.Log("Picked Games:");
        foreach (var g in currentGameDefinitions)
            Debug.Log($" - {g.DisplayName} (${g.BasePriceCents / 100f:0.00})");
    }

}
