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
    Button settingBtn;
    [SerializeField]
    Slider difficultyLevel;

    [SerializeField] 
    CardGridSpawner cardGridSpawner;
    
    [SerializeField] 
    RectTransform gameHUD;
    // Start is called before the first frame update
    private void OnEnable() {
       playBtn.onClick.AddListener(OnClickPlay);
       settingBtn.onClick.AddListener(OnClickPlay);
       difficultyLevel.onValueChanged.AddListener(OnDifficultyChanged);
       
    }

    private void OnDisable() {
        playBtn.onClick.RemoveListener(OnClickPlay);
        settingBtn.onClick.RemoveListener(OnClickPlay);
        difficultyLevel.onValueChanged.RemoveListener(OnDifficultyChanged);
    }

    void Start()
    {
        difficultyLevel.value = PersistentDataManager.Instance.GetDifficulty();
    }

    void OnClickPlay()
    {
        AudioManager.Instance.PlaySFX("_btnPress");
        cardGridSpawner.ApplyDifficultyToGrid();
        transform.DOScale(Vector3.zero, .2f);
        gameHUD.DOScale(Vector3.one, .1f);
    }
    void OnClickSetting()
    {
        AudioManager.Instance.PlaySFX("_btnPress");
    }

    public void OnDifficultyChanged(float value)
    {
        AudioManager.Instance.PlaySFX(value.ToString());
        PersistentDataManager.Instance.SetDifficulty((int)value);
    }


   
}
