using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//인게임 메뉴 관리하는 컴포넌트
//팝업 메뉴 뜰때 역할에 맞는 팝업창 띄우기
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-11
public class InGameMenuController : MonoBehaviour
{
    public static InGameMenuController instance { get; private set; }
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button titleBtn;
    [SerializeField] private Button endGameBtn;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private PopUpController popUP;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        restartBtn.onClick.AddListener(OnClickRestartBtn);
        settingBtn.onClick.AddListener(OnClickSettingBtn);
        titleBtn.onClick.AddListener(OnClickTitleBtn);
        endGameBtn.onClick.AddListener(OnClickEndGameBtn);
    }

    private void OnClickRestartBtn()
    {
        popUP.SetPopUpMode("restart","경고\n현재까지의 진행사항은 저장이 안됩니다.\n 그래도 다시하시겠습니까?");
    }

    private void OnClickSettingBtn()
    {
        settingMenu.SetActive(true);
    }

    private void OnClickTitleBtn()
    {
        popUP.SetPopUpMode("title","경고\n현재까지의 진행사항은 저장이 안됩니다.\n 그래도 타이틀로 돌아가시겠습니까?");
    }
    private void OnClickEndGameBtn()
    {
        Application.Quit();
    }
}