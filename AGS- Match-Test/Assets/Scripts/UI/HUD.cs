using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText, countText;

    [SerializeField]
    Button homeBtn;

   [SerializeField] RectTransform menuScreen;

    void OnEnable()
    {
        homeBtn.onClick.AddListener(OnClickHomeBtn);
    }

    void OnDisable()
    {
        homeBtn.onClick.RemoveListener(OnClickHomeBtn);
    }

    // Start is called before the first frame update
    void OnClickHomeBtn()
    {
        menuScreen.DOScale(Vector3.one, .2f);
        AudioManager.Instance.PlaySFX("_btnPress");
    }
}
