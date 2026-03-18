using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Sounds")]
    public List<SoundData> musicList;
    public List<SoundData> sfxList;

    Dictionary<string, SoundData> musicDict;
    Dictionary<string, SoundData> sfxDict;

    const string MUSIC_VOL = "MUSIC_VOL";
    const string SFX_VOL = "SFX_VOL";
    const string MUTE_KEY = "MUTE";

    bool isMuted;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        musicDict = new Dictionary<string, SoundData>();
        sfxDict = new Dictionary<string, SoundData>();

        foreach (var s in musicList)
            musicDict[s.id] = s;

        foreach (var s in sfxList)
            sfxDict[s.id] = s;

        LoadSettings();
    }

     public void LoadSettings()
    {
        float musicVol = PlayerPrefs.GetFloat(MUSIC_VOL, 1);
        float sfxVol = PlayerPrefs.GetFloat(SFX_VOL, 1);

        isMuted = PersistentDataManager.Instance.GetMuteState();

        musicSource.volume = musicVol;
        sfxSource.volume = sfxVol;

        musicSource.mute = isMuted;
      
    }

    // MUSIC

    public void PlayMusic(string id)
    {
        if (!musicDict.ContainsKey(id))
            return;

        SoundData sound = musicDict[id];

        musicSource.clip = sound.clip;
        musicSource.volume = sound.volume;
        musicSource.loop = sound.loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // SFX

    public void PlaySFX(string id)
    {
        if (!sfxDict.ContainsKey(id))
            return;

        SoundData sound = sfxDict[id];

        sfxSource.PlayOneShot(sound.clip, sound.volume);
    }

    // VOLUME

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOL, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFX_VOL, volume);
    }

    // MUTE

    public void ToggleMute()
    {
        isMuted = !isMuted;

        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;

        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}