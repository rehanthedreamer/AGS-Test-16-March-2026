using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class CardGridSpawner : MonoBehaviour
{
    public static CardGridSpawner Instance;
    public int rows = 4;
    public int columns = 4;
    [Header("Container Size")]
     float containerWidth = 8f;
     float containerHeight = 8f;

    [Header("Spacing")]
    public float spacing = 0.2f;

    [Header("Card Database")]
    public CardDatabase database;
    public CardCategory selectedCategory;
    List<CardItem> selectedCards;
    List<CardView> activeCards = new List<CardView>();

    public int totalCards;
    int remainingCards;
    void Awake()
    {
        Instance = this;
    }
   public void ApplyDifficultyToGrid()
    {
        switch (PersistentDataManager.Instance.GetDifficulty())
        {
            case 1:
                SetGrid(2,2);
                break;

            case 2:
                SetGrid(2,3);
                break;

            case 3:
                SetGrid(4,4);
                break;
            case 4:
                 SetGrid(5,6);
                break;
                case 5:
                 SetGrid(6,6);
                break;
        }
    }

    public void GenerateGrid()
    {
        // remaining card count
        totalCards = rows * columns;
        remainingCards = totalCards;
        ClearGrid();
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
                CardView card = CardPool.Instance.Get();
                card.transform.SetParent(transform);
                card.transform.localPosition = pos;

                card.transform.localScale = Vector3.one * cardSize;

                CardView view = card.GetComponent<CardView>();
                view.Setup(finalList[index]);
                activeCards.Add(card);
                index++;
            }
        }
        StartCoroutine(PreviewAndHideCards());
    }
    

  public  void ClearGrid()
    {
        foreach (var card in activeCards)
        {
            CardPool.Instance.ReturnToPool(card);
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


    IEnumerator PreviewAndHideCards()
    {
        CardView.IsInputLocked = true;

        foreach (var card in activeCards)
        {
            CardView view = card.GetComponent<CardView>();

            if (!view.isFlipped)
                view.Flip();
        }

        yield return new WaitForSeconds(2f);

        foreach (var card in activeCards)
        {
            CardView view = card.GetComponent<CardView>();

            if (view.isFlipped && !view.isMatched)
                view.Flip();
        }

        yield return new WaitForSeconds(0.3f); // wait for animation    
        CardView.IsInputLocked = false;
    }

    public void OnCardRemovedFromGrid()
    {
        remainingCards--;
        if(remainingCards <= 0)
        {
            Debug.Log("Game Over");
            GameOverScreen.OnGameOver?.Invoke();
        }
    }
    
}