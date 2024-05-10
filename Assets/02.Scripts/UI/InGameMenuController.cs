using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//인게임 메뉴 관리하는 컴포넌트
//TODO: 로비씬에서 PlayerPref로 현재 가지고 있는 돈을 저장을 해놓고 게임 도중 다시하기, 타이버튼,게임종료 버튼을 누르면 게임 시작전에 돈으로 되돌아가기.
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-10
public class InGameMenuController : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button titleBtn;
    [SerializeField] private Button endGameBtn;
    [SerializeField] private GameObject settingMenu;

    private void Start()
    {
        restartBtn.onClick.AddListener(OnClickRestartBtn);
        settingBtn.onClick.AddListener(OnClickSettingBtn);
        titleBtn.onClick.AddListener(OnClickTitleBtn);
        endGameBtn.onClick.AddListener(OnClickEndGameBtn);
    }

    private void OnClickRestartBtn()
    {
        SceneManager.LoadScene("Lobby");   
    }

    private void OnClickSettingBtn()
    {
        settingMenu.SetActive(true);
    }

    private void OnClickTitleBtn()
    {
        SceneManager.LoadScene("Title");
        ResetInstance();
    }
    private void OnClickEndGameBtn()
    {
        Application.Quit();
    }

    private void ResetInstance()
    {
        Destroy(AllSceneCanvas.instance.gameObject);
        Destroy(SceneSystem.instance.gameObject);
        Destroy(PlayerManager.instance.gameObject);
    }
}
