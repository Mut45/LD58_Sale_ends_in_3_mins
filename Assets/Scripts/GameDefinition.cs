using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameDefinition", menuName = "Game Catalog/Game Definition")]
public class GameDefinition : ScriptableObject
{
    [SerializeField] private string id;  // stable unique id, e.g., "G_SIM_0001"
    [SerializeField] private string displayName;
    [SerializeField] private int basePriceCents; // store cents to avoid float issues
    [SerializeField] private float discount; // the discount the game is on
    [SerializeField] private Sprite coverArt; // the banner icon for the game
    [SerializeField] private string series; // the series that this game is part of
    [SerializeField] private int seriesCount; // the number of games in the series
    [SerializeField] private string type;
    [SerializeField] private float quality;

    public string Id => id;
    public string DisplayName => displayName;
    public int BasePriceCents => basePriceCents;
    public float Discount => discount;
    public Sprite CoverArt => coverArt;
    public string Series => series;
    public int SeriesCount => seriesCount;
    public string Type => type;
    public float Quality => quality;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Auto-fill a safe ID if empty
        if (string.IsNullOrEmpty(id))
            id = name.ToUpperInvariant().Replace(' ', '_');
        // Clamp the quality score if exceeding 5 or under 0
        if (quality > 5) {
            quality = Mathf.Min(quality, 5);
        }
        if (quality < 0) {
            quality = Mathf.Max(quality, 0);
        }
    }
#endif
}