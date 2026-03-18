using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;

    List<CardView> flippedCards = new List<CardView>();

    void Awake()
    {
        Instance = this;
    }

   

    public void RegisterFlip(CardView card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count >= 2)
        {
            StartCoroutine(CheckMatch(flippedCards[flippedCards.Count - 2],
                                      flippedCards[flippedCards.Count - 1]));
        }
    }

    IEnumerator CheckMatch(CardView a, CardView b)
    {
        yield return new WaitForSeconds(0.5f);

        if (a.cardID == b.cardID)
        {
            a.SetMatched();
            b.SetMatched();
            flippedCards.Remove(a);
            flippedCards.Remove(b);
            ScoreManager.Instance.OnMatch();
            ScoreManager.Instance.AddMatch(1);
            ScoreManager.Instance.AddTurn(1);
            AudioManager.Instance.PlaySFX("_cardMatched");
        }
        else
        {
            a.Flip();
            b.Flip();
            flippedCards.Remove(a);
            flippedCards.Remove(b);
            ScoreManager.Instance.OnMismatch();
            ScoreManager.Instance.AddTurn(1);
            AudioManager.Instance.PlaySFX("_cardMismatched");
        }
    }
}