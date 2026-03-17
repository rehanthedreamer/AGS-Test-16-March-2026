using System.Collections.Generic;
using UnityEngine;

public class CardGridSpawner : MonoBehaviour
{
    public int rows = 4;
    public int columns = 4;

    [Header("Container Size")]
    public float containerWidth = 8f;
    public float containerHeight = 8f;

    [Header("Spacing")]
    public float spacing = 0.3f;

    public CardDatabase database;
    public CardCategory selectedCategory;
    List<CardItem> selectedCards;

    List<GameObject> activeCards = new List<GameObject>();
    List<int> cardIDs = new List<int>();

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        selectedCards = database.GetByCategory(selectedCategory);
        int pairCount = (rows * columns) / 2;
        List<CardItem> finalList = new List<CardItem>();

        for (int i = 0; i < pairCount; i++)
        {
            var item = selectedCards[i % selectedCards.Count];
            finalList.Add(item);
            finalList.Add(item);
        }

        Shuffle(finalList);

        float cellWidth = (containerWidth - (columns - 1) * spacing) / columns;
        float cellHeight = (containerHeight - (rows - 1) * spacing) / rows;

        float cardSize = Mathf.Min(cellWidth, cellHeight);
        // Calculate actual grid size
        float gridWidth = columns * cardSize + (columns - 1) * spacing;
        float gridHeight = rows * cardSize + (rows - 1) * spacing;

        // Center offset
        float startX = -gridWidth / 2 + cardSize / 2;
        float startY = gridHeight / 2 - cardSize / 2-.6f;

        int index = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector3 pos = new Vector3(
                    startX + c * (cardSize + spacing),
                    startY - r * (cardSize + spacing),
                    0
                );

                GameObject card = CardPool.Instance.GetCard();
                card.transform.SetParent(transform);
                card.transform.localPosition = pos;

                card.transform.localScale = Vector3.one * cardSize;

                CardView view = card.GetComponent<CardView>();
                view.SetData(finalList[index]);
                activeCards.Add(card);

                index++;
            }
        }
    }

    void ClearGrid()
    {
        foreach (var card in activeCards)
        {
            CardPool.Instance.ReturnCard(card);
        }

        activeCards.Clear();
    }

    void Shuffle(List<CardItem> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public void SetGrid(int r, int c)
    {
        rows = r;
        columns = c;

        GenerateGrid();
    }
}