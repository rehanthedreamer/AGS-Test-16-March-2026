using System.Collections.Generic;
using UnityEngine;

public class CardGridSpawner : MonoBehaviour
{
    public int rows = 4;
    public int columns = 4;

    [Header("Container Size")]
    public float containerWidth = 10f;
    public float containerHeight = 6f;

    [Header("Spacing")]
    public float spacing = 0.3f;

    List<GameObject> activeCards = new List<GameObject>();
    List<int> cardIDs = new List<int>();

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        ClearGrid();

        int totalCards = rows * columns;
        int pairCount = totalCards / 2;

        cardIDs.Clear();

        for (int i = 0; i < pairCount; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        Shuffle(cardIDs);

        float cellWidth = (containerWidth - (columns - 1) * spacing) / columns;
        float cellHeight = (containerHeight - (rows - 1) * spacing) / rows;

        float cardSize = Mathf.Min(cellWidth, cellHeight);

        float startX = -containerWidth / 2 + cardSize / 2;
        float startY = containerHeight / 2 - cardSize / 2;

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
                view.cardID = cardIDs[index];
                view.ResetCard();

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

    void Shuffle(List<int> list)
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