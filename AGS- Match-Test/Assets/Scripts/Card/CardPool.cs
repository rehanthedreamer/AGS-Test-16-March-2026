using UnityEngine;
using System.Collections.Generic;

public class CardPool : MonoBehaviour
{
    public static CardPool Instance;
    public CardView cardPrefab;
    public int initialSize = 20;

    Queue<CardView> pool = new Queue<CardView>();

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNew();
        }
    }

    CardView CreateNew()
    {
        CardView card = Instantiate(cardPrefab, transform);
        card.gameObject.SetActive(false);
        card.Init(this);

        pool.Enqueue(card);
        return card;
    }

    public CardView Get()
    {
        if (pool.Count == 0)
            CreateNew();

        CardView card = pool.Dequeue();
        card.gameObject.SetActive(true);
        card.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        return card;
    }

    public void ReturnToPool(CardView card)
    {
        card.gameObject.SetActive(false);
        // Reset state
        card.transform.localScale = Vector3.one;
        card.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        card.GetComponent<Collider2D>().enabled = true;
        pool.Enqueue(card);
        
    }
}