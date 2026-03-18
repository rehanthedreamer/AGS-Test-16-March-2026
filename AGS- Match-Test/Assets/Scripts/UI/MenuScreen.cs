using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class MenuScreen : MonoBehaviour
{

    [SerializeField]
    Button playBtn;
    [SerializeField]
    Button musicBtn;
    [SerializeField]
    GameObject muteObj;
    [SerializeField]
    Slider difficultyLevel;

    [SerializeField] 
    CardGridSpawner cardGridSpawner;
    
    [SerializeField] 
    RectTransform gameHUD;
    // Start is called before the first frame update
    private void OnEnable() {
       playBtn.onClick.AddListener(OnClickPlay);
       musicBtn.onClick.AddListener(OnClickMusic);
       difficultyLevel.onValueChanged.AddListener(OnDifficultyChanged);
       
    }

    private void OnDisable() {
        playBtn.onClick.RemoveListener(OnClickPlay);
        musicBtn.onClick.RemoveListener(OnClickMusic);
        difficultyLevel.onValueChanged.RemoveListener(OnDifficultyChanged);
    }

    void Start()
    {
        difficultyLevel.value = PersistentDataManager.Instance.GetDifficulty();
        muteObj.SetActive(PersistentDataManager.Instance.GetMuteState());
        AudioManager.Instance.LoadSettings();
        ScoreManager.Instance.LoadScore();
        GameOverScreen.OnCloseGameOverScreen?.Invoke();
    }

    void OnClickPlay()
    {
        AudioManager.Instance.PlaySFX("_btnPress");
        cardGridSpawner.ApplyDifficultyToGrid();
        transform.DOScale(Vector3.zero, .2f);
        gameHUD.DOScale(Vector3.one, .1f);
        ScoreManager.Instance.ResetMatchNTurn();
        GameOverScreen.OnCloseGameOverScreen?.Invoke();
        AudioManager.Instance.PlayMusic("_bgMusic");
    }
    void OnClickMusic()
    {
        AudioManager.Instance.PlaySFX("_btnPress");
        var isMute = PersistentDataManager.Instance.GetMuteState();
        PersistentDataManager.Instance.SetMuteState(isMute? false: true);
        muteObj.SetActive(PersistentDataManager.Instance.GetMuteState());
        AudioManager.Instance.LoadSettings();
        ScoreManager.Instance.LoadScore();
    }

    public void OnDifficultyChanged(float value)
    {
        AudioManager.Instance.PlaySFX(value.ToString());
        PersistentDataManager.Instance.SetDifficulty((int)value);
    }


   
}
