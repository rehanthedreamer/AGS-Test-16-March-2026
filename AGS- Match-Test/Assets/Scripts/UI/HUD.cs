using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText, matchText, trunText;

    [SerializeField]
    Button homeBtn;

   [SerializeField] RectTransform menuScreen;

    void OnEnable()
    {
        homeBtn.onClick.AddListener(OnClickHomeBtn);
        ScoreManager.OnScoreChanged += UpdateScoreUI;
        ScoreManager.OnMatchChanged += UpdateMacthUI;
        ScoreManager.OnTurnChanged += UpdateTurnUI;
    }

    void OnDisable()
    {
        homeBtn.onClick.RemoveListener(OnClickHomeBtn);
        ScoreManager.OnScoreChanged -= UpdateScoreUI;
        ScoreManager.OnMatchChanged -= UpdateMacthUI;
        ScoreManager.OnTurnChanged -= UpdateTurnUI;
    }

    // Start is called before the first frame update
    void OnClickHomeBtn()
    {
        menuScreen.DOScale(Vector3.one, .2f);
        AudioManager.Instance.PlaySFX("_btnPress");
        AudioManager.Instance.StopMusic();
        CardGridSpawner.Instance.ClearGrid();
    }

    void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: "+ score;
    }

      void UpdateMacthUI(int match)
    {
         matchText.text = "Score: "+ match;
    }
      void UpdateTurnUI(int turn)
    {
         trunText.text = "Score: "+ turn;
    }
}
