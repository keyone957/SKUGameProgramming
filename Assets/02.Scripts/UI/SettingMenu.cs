using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//세팅창에서 화면모드, 브금, 효과음 볼륨 조절
// 최초 작성자 : 홍원기
// 수정자 :
// 최종 수정일 : 2024-05-31
public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button fullScreen;
    [SerializeField] private Button windowScreen;

    private void Start()
    {
        exitBtn.onClick.AddListener(() =>
        {
            SoundManager._instance.PlaySound(Define._clickMenuSound);
            gameObject.SetActive(false);
        });
        fullScreen.onClick.AddListener(SetFullScreen);
        windowScreen.onClick.AddListener(SetWindowScreen);
        soundSlider.value = float.Parse(PlayerPrefs.GetString("soundVolume"));
        bgmSlider.value = float.Parse(PlayerPrefs.GetString("bgmVolume"));
        ScreenModeSpr(PlayerPrefs.GetString("screenMode"));
    }

    public void SetBgmVolume()
    {
        PlayerPrefs.SetString("bgmVolume", bgmSlider.value.ToString());
        SoundManager._instance.SetBgmVolume(bgmSlider.value);
    }

    public void SetSoundVolume()
    {
        PlayerPrefs.SetString("soundVolume", soundSlider.value.ToString());
        SoundManager._instance.SetSoundVolume(soundSlider.value);
    }

    public void SetWindowScreen()
    {
        ScreenModeSpr("Window");
        PlayerPrefs.SetString("screenMode","Window");
        Screen.fullScreen = false;
        Screen.SetResolution(1280, 720, false);
    }

    public void SetFullScreen()
    {
        ScreenModeSpr("FullScreen");
        PlayerPrefs.SetString("screenMode","FullScreen");
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);
    }

    public void ScreenModeSpr(string mode)
    {
        if (mode == "FullScreen")
        {
            fullScreen.GetComponent<Image>().color = HexToColor("#65BA68");
            windowScreen.GetComponent<Image>().color =  HexToColor("#FFFFFF");
        }
        else if(mode =="Window")
        {
            fullScreen.GetComponent<Image>().color =  HexToColor("#FFFFFF");
            windowScreen.GetComponent<Image>().color =HexToColor("#65BA68");
        }
    }
    
    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hex color string");
            return Color.white;
        }
    }
}
