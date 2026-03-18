using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    public static PersistentDataManager Instance;

    const string SAVE_KEY = "GAME_SAVE";

    public SaveData Data { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // -----------------------------
    // SAVE
    // -----------------------------

    public void Save()
    {
        string json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    // -----------------------------
    // LOAD
    // -----------------------------

    public void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            Data = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Data = GetDefaultData();
            Save();
        }
    }

    // -----------------------------
    // DEFAULT DATA
    // -----------------------------

    SaveData GetDefaultData()
    {
        return new SaveData
        {
            difficulty = 0,
            score = 0,
            musicVolume = 1f,
            sfxVolume = 1f,
            isMuted = false
        };
    }

    // -----------------------------
    // SETTERS (Clean API)
    // -----------------------------

    public void SetDifficulty(int value)
    {
        Data.difficulty = value;
        Save();
    }

    public int GetDifficulty()
    {
        return Data.difficulty;
    }

    public void SetScore(int value)
    {
        Data.score = value;
        Save();
    }

    public int GetScore()
    {
        return Data.score;
    }

    public void SetAudio(float music, float sfx, bool mute)
    {
        Data.musicVolume = music;
        Data.sfxVolume = sfx;
        Data.isMuted = mute;
        Save();
    }

    public bool GetMuteState()
    {
        return Data.isMuted;
    }

    public void SetMuteState(bool mute)
    {
        Data.isMuted = mute;
        Save();
    }
}