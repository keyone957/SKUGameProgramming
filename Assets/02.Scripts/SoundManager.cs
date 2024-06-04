using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//환경설정 위한 volume
//playerprefab값 초기화
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-04
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmSource = null;
    [SerializeField] private AudioSource _soundSource = null;
    public static SoundManager _instance { get; private set; }

    private Dictionary<string, AudioClip> _loadedClip = new Dictionary<string, AudioClip>();
    private float _bgmVolume;
    private float _soundVolume;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //효과음과, 배경음악은 어느 씬에서나 사용하기 때문에 DonDestroyOnLoad를 이용하여 삭제가 되지 않게
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("bgmVolume"))
        {
            PlayerPrefs.SetString("bgmVolume", "1.0"); 
        }
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetString("soundVolume", "1.0");
        }

        _bgmSource.volume = float.Parse(PlayerPrefs.GetString("bgmVolume"));
        _soundSource.volume = float.Parse(PlayerPrefs.GetString("soundVolume"));
    }

    private AudioClip LoadAudioClip(string fullPath)
    {
        AudioClip clip = null;
        if (_loadedClip.TryGetValue(fullPath, out clip))
            return clip;

        clip = Resources.Load<AudioClip>(fullPath);
        if (clip == null)
        {
            Debug.LogError($"[SoundManager.LoadAudioClip.InvalidPath]{fullPath}");
            return null;
        }

        _loadedClip.Add(fullPath, clip);
        return clip;
    }

    #region BGM
    

    private static string GetBGMFullPath(string path) => Define._bgmRoot + "/" + path;
    public void LoadBGM(string path) => LoadAudioClip(GetBGMFullPath(path));

    public void PlayBGM(string path)
    {
        AudioClip clip = LoadAudioClip(GetBGMFullPath(path));
        if (clip == null)
            return;

        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void StopBGM() => _bgmSource.Stop();

    #endregion

    #region SoundEffect

    private static string GetSoundFullPath(string path) => Define._soundRoot + "/" + path;

    public void LoadSound(string path) => LoadAudioClip(GetSoundFullPath(path));

    public void PlaySound(string path)
    {
        AudioClip clip = LoadAudioClip(GetSoundFullPath(path));
        if (clip == null)
            return;
        _soundSource.PlayOneShot(clip);
    }

    public void ClearLoadedAudioClip() => _loadedClip.Clear();

    #endregion

    public void SetSoundVolume(float volume)
    {
        _soundVolume = volume;
        _soundSource.volume = _soundVolume;
    }
    public void SetBgmVolume(float volume)
    {   //배경음 audiosource의 volume 조절
        _bgmVolume = volume;
        _bgmSource.volume = _bgmVolume;
    }
}