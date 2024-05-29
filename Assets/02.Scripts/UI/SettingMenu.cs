using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
  [SerializeField] private Slider soundSlider;
  [SerializeField] private Slider bgmSlider;
  [SerializeField] private Button exitBtn;
  [SerializeField] private Button fullScreen;
  [SerializeField] private Button windowScreen;

  private void Start()
  {
    exitBtn.onClick.AddListener(() => gameObject.SetActive(false));
  }

  public void SetBgmVolume()
  {
    PlayerPrefs.SetString("bgmVolume", bgmSlider.value.ToString());
    SoundManager._instance.SetBgmVolume(bgmSlider.value);
  }

  public void SetSoundVolume()
  {
    PlayerPrefs.SetString("soundVolume",soundSlider.value.ToString());
    SoundManager._instance.SetSoundVolume(soundSlider.value);
  }

}
