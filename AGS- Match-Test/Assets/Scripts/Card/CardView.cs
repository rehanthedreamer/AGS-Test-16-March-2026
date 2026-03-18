using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Pool;

public class CardView : MonoBehaviour
{
    [Header("References")]
    public GameObject front;
    public GameObject back;
    public SpriteRenderer frontIcon;

    [Header("Settings")]
    public float flipDuration = 0.2f;

    // Data
    public int cardID;

    // States
    public bool isFlipped = false;
    public bool isMatched = false;

    public static bool IsInputLocked = false;
    CardPool pool;

    bool isAnimating = false;


    public void Init(CardPool poolRef)
    {
        pool = poolRef;
    }
    // SETUP (called from Spawner)
    public void Setup(CardItem cardItem)
    {
        
        cardID = cardItem.id;
        frontIcon.sprite = cardItem.icon;

        isFlipped = false;
        isMatched = false;
        isAnimating = false;
    }

    // CLICK HANDLER
    void OnMouseDown()
    {
        if (IsInputLocked) return;
        if (isFlipped || isMatched || isAnimating) return;

        Flip();
        // Notify match system
        MatchSystem.Instance.RegisterFlip(this);
    }

    // FLIP ANIMATION
    public void Flip()
    {
        if (isAnimating) return;
        StartCoroutine(FlipRoutine());
    }

    IEnumerator FlipRoutine()
    {
        isAnimating = true;

        float t = 0;
        float startY = transform.eulerAngles.y;
        float targetY = startY + 180f;
        AudioManager.Instance.PlaySFX("_cardFlip");
        while (t < 1)
        {
            t += Time.deltaTime / flipDuration;

            float yRotation = Mathf.Lerp(startY, targetY, t);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            // Switch visuals at half flip
            if (t >= 0.5f)
            {
                front.SetActive(!isFlipped);
                back.SetActive(isFlipped);
            }

            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, targetY, 0);

        isFlipped = !isFlipped;
        isAnimating = false;
    }

    // FORCE STATES (NO ANIMATION)
    public void SetInstantFront()
    {
        isFlipped = true;
        front.SetActive(true);
        back.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 180f, 0);
    }

    public void SetInstantBack()
    {
        isFlipped = false;
        front.SetActive(false);
        back.SetActive(true);
        transform.rotation = Quaternion.Euler(0, 0f, 0);
    }

    // MATCHED STATE
    public void SetMatched()
    {
        isMatched = true;
        GetComponent<Collider2D>().enabled = false;
        MatchEffect();
    }

    void MatchEffect()
    {
        Vector3 originalScale = transform.localScale;
        transform.DOScale(originalScale*1.1f, .3f).OnComplete(()=> 
            RemoveCard()
        ); 
    }

    void RemoveCard()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(.2f).
        Append(transform.DOScale(Vector3.zero, .2f)).OnComplete(()=>
        {
            ReturnToPool();
            CardGridSpawner.Instance.OnCardRemovedFromGrid();
        }
        );
    }

    public void ReturnToPool()
    {
        pool.ReturnToPool(this);
    }
}