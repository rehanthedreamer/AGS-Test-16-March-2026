using UnityEngine;
using System.Collections.Generic;

public class CardPool : MonoBehaviour
{
    public static CardPool Instance;
    public CardView cardPrefab;
    public int initialSize = 20;

    List<CardView> pool = new List<CardView>();

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
        pool.Add(card);
        return card;
    }

    public CardView Get()
    {
       for (int i = 0; i < pool.Count; i++)
       {
        if(!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }
        return CreateNew();
    }

    public void ReturnToPool(CardView card)
    {
        card.gameObject.SetActive(false);
        // Reset state
        card.transform.localScale = Vector3.one;
        card.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        card.transform.parent = transform;
        card.GetComponent<Collider2D>().enabled = true;  
    }
}