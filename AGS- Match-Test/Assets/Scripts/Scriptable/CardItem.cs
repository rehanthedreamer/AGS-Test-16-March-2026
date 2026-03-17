using UnityEngine;

public enum CardCategory
{
    Animals,
    Fruits,
    Eatable,
    Custom
}

[System.Serializable]
public class CardItem
{
    public int id;
    public CardCategory category;
    public Sprite icon;
}