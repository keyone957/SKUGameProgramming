using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//각 메뉴 버튼들 이벤트 연결
//메뉴 버튼들의 효과음 나오게
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-31
public class TitleMenu : MonoBehaviour
{
    [SerializeField] private Button _btnStart=null;
    [SerializeField] private FadeOverlay _fadeOverlay=null;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button quitGameBtn;
    [SerializeField] private GameObject settingMenu;
    //버튼에 이벤트 연결
    private void Awake()
    {
        _btnStart.onClick.AddListener(OnClickStartBtn);
        settingBtn.onClick.AddListener(OnClickSettingBtn);
        quitGameBtn.onClick.AddListener(OnClickQuitGameBtn);
    }

    private void OnClickStartBtn()
    {
        SoundManager._instance.PlaySound(Define._gameStartBtn);
        _fadeOverlay.DoFadeOut(1.5f,"Lobby");
    }

    private void OnClickSettingBtn()
    {
        SoundManager._instance.PlaySound(Define._clickMenuSound);
        settingMenu.SetActive(true);
    }

    private void OnClickQuitGameBtn()
    {
        SoundManager._instance.PlaySound(Define._clickMenuSound);
        Application.Quit();

    }
}
