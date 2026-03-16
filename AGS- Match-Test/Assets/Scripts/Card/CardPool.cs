using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    public static CardPool Instance;

    public GameObject cardPrefab;
    public int initialSize = 40;

    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(cardPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetCard()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(cardPrefab, transform);
        return newObj;
    }

    public void ReturnCard(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}