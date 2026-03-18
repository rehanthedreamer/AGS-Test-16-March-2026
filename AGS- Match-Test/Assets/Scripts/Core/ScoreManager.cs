using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; }
    public int Turn { get; private set; }
    public int Match { get; private set; }

    // Delegate event
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnMatchChanged;
    public static event Action<int> OnTurnChanged;

    int comboCount;
    void Awake()
    {
        Instance = this;
    }

    // ADD SCORE
      public void OnMatch()
    {
        comboCount++;

        int baseScore = 10;

        // Only apply combo after 2 streak
        int multiplier = comboCount > 2 ? comboCount : 1;

        int finalScore = baseScore * multiplier;

        Score += finalScore;
        Debug.Log($"Match! Combo: {comboCount} Score Added: {finalScore}");
        OnScoreChanged?.Invoke(Score);
       
       
        PersistentDataManager.Instance.SetScore(Score);
    }

    public void AddMatch(int value)
    {
        Match += value;
        OnMatchChanged?.Invoke(Match);
    }

     public void AddTurn(int value)
    {
        Turn += value;
        OnTurnChanged?.Invoke(Turn);
    }

    // CALL ON MISMATCH
    public void OnMismatch()
    {
        comboCount = 0;
        OnTurnChanged?.Invoke(1);
    }

    public void ResetMatchNTurn()
    {
        Turn = 0;
        Match = 0;
        OnMatchChanged?.Invoke(Match);
        OnTurnChanged?.Invoke(Turn);
    }

    // LOAD SAVED SCORE
    public void LoadScore()
    {
        Score = PersistentDataManager.Instance.GetScore();
        OnScoreChanged?.Invoke(Score);
    }
}
