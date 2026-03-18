using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public static Action OnGameOver;
    public static Action OnCloseGameOverScreen;

    [SerializeField]
    Button nextLevelBtn;

    [SerializeField]
    RectTransform gameHUD;


    void OnEnable()
    {
        nextLevelBtn.onClick.AddListener(OnClickNextLevel);
        OnGameOver += ShowGameOverScreen;
        OnCloseGameOverScreen += CloseGameOverScreen;
    }

    void OnDisable()
    {
        nextLevelBtn.onClick.RemoveListener(OnClickNextLevel);
         OnGameOver -= ShowGameOverScreen;
         OnCloseGameOverScreen -= CloseGameOverScreen;
    }
    // Start is called before the first frame update
   void OnClickNextLevel()
    {
         AudioManager.Instance.PlaySFX("_btnPress");
        CardGridSpawner.Instance.ApplyDifficultyToGrid();
        transform.DOScale(Vector3.zero, .2f);
        gameHUD.DOScale(Vector3.one, .1f);
        ScoreManager.Instance.ResetMatchNTurn();
        OnCloseGameOverScreen?.Invoke();
        AudioManager.Instance.PlayMusic("_bgMusic");
    }

    void ShowGameOverScreen()
    {
        transform.DOScale(Vector3.one, .2f);
        AudioManager.Instance.PlaySFX("_gameOver");
        AudioManager.Instance.StopMusic();
         CardGridSpawner.Instance.ClearGrid();
    }

    void CloseGameOverScreen()
    {
        transform.DOScale(Vector3.zero,.2f);
    }
  
}
