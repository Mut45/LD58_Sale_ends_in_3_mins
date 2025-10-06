using UnityEngine;

[System.Serializable]
public class GameData
{
    public int id;
    public string title;
    public Sprite gameIcon;
    public float rating;
    public float originalPrice;
    public float discount; // 折扣百分比 (0-100)
    public string type; // 游戏类型
    
    public float FinalPrice => originalPrice * discount;
    
    public GameData(int id, string title, Sprite gameIcon, float rating, float originalPrice, float discount, string type)
    {
        this.id = id;
        this.title = title;
        this.gameIcon = gameIcon;
        this.rating = rating;
        this.originalPrice = originalPrice;
        this.discount = discount;
        this.type = type;
    }
}



