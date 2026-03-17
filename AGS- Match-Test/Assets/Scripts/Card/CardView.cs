using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public int cardID;

    public SpriteRenderer iconRenderer;

    CardItem data;

    public void SetData(CardItem item)
    {
        data = item;
        cardID = item.id;

        iconRenderer.sprite = item.icon;
    }
}