using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameCatalog", menuName = "Game Catalog/Catalog")]
public class GameCatalog : ScriptableObject
{
    [SerializeField] private List<GameDefinition> games = new List<GameDefinition>();
    private Dictionary<string, GameDefinition> index;

    public IReadOnlyList<GameDefinition> Games => games;

    public void BuildIndex()
    {
        index = new Dictionary<string, GameDefinition>();
        foreach (var g in games)
        {
            if (g == null || string.IsNullOrEmpty(g.Id)) continue;
            if (!index.ContainsKey(g.Id)) index.Add(g.Id, g);
            else Debug.LogWarning($"Duplicate GameDefinition id: {g.Id}");
        }
    }

    public GameDefinition GetById(string id)
    {
        if (index == null) BuildIndex();
        return (id != null && index.TryGetValue(id, out var g)) ? g : null;
    }
}