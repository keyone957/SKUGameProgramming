using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 내 bgm 및 효과음 플레이 및 관리 하기 위한 SoundManager
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-04-12
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
        //TODO : 환경설정에 따라서 사용자가 설정한 음량에 따라 배경,효과음 실행
        //PlayerPrefs 사용 바람.
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
}