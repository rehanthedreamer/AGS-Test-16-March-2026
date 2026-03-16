using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardView : MonoBehaviour
{
    public int cardID;
    public bool isFlipped;
    public bool isMatched;

    public GameObject front;
    public GameObject back;

    public void OnClick()
    {
        if (isFlipped || isMatched) return;

        Flip();
        MatchSystem.Instance.RegisterFlip(this);
    }

    public void Flip()
    {
        StartCoroutine(FlipRoutine());
    }

    IEnumerator FlipRoutine()
    {
        float duration = 0.2f;
        float time = 0;

        while (time < duration)
        {
            transform.Rotate(0, 180 * Time.deltaTime / duration, 0);
            time += Time.deltaTime;
            yield return null;
        }

        isFlipped = !isFlipped;

        front.SetActive(isFlipped);
        back.SetActive(!isFlipped);
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    public void ResetCard()
    {
        isFlipped = false;
        front.SetActive(false);
        back.SetActive(true);
    }
}