using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Game/Card Database")]
public class CardDatabase : ScriptableObject
{
    public List<CardItem> cards = new List<CardItem>();

    Dictionary<int, CardItem> lookup;

    public void Init()
    {
        lookup = new Dictionary<int, CardItem>();

        foreach (var card in cards)
        {
            if (!lookup.ContainsKey(card.id))
                lookup.Add(card.id, card);
        }
    }

    public CardItem GetCardByID(int id)
    {
        if (lookup == null) Init();

        if (lookup.ContainsKey(id))
            return lookup[id];

        return null;
    }

    public List<CardItem> GetByCategory(CardCategory category)
    {
        List<CardItem> result = new List<CardItem>();

        foreach (var card in cards)
        {
            if (card.category == category)
                result.Add(card);
        }

        return result;
    }
}