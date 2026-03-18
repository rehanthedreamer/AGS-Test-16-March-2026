using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField]
    Button nextLevelBtn;


    void OnEnable()
    {
        nextLevelBtn.onClick.AddListener(OnClickNextLevel);
    }

    void OnDisable()
    {
        nextLevelBtn.onClick.RemoveListener(OnClickNextLevel);
    }
    // Start is called before the first frame update
   void OnClickNextLevel()
    {
        
    }
  
}
