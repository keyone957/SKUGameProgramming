using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//팝업 메뉴 뜰때 역할에 맞는 팝업창 띄우기 및 그에 알맞은 버튼 이벤트 추가
//TODO: 로비씬에서 PlayerPref로 현재 가지고 있는 돈을 저장을 해놓고 게임 도중 다시하기, 타이버튼,게임종료 버튼을 누르면 게임 시작전에 돈으로 되돌아가기.
//다시하기 눌렀을때 몬스터 카운트 0으로
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-04
public class PopUpController : MonoBehaviour
{
   [SerializeField] private Button yesBtn;
   [SerializeField] private Button noBtn;
   [SerializeField] private TMP_Text warningText;
   public string popUpMode;
   

   private void Start()
   {
      yesBtn.onClick.AddListener(OnClickYesBtn);
      noBtn.onClick.AddListener(OnClickNoBtn);
   }
   public void SetPopUpMode(string mode,string text)
   {
      popUpMode = mode;
      warningText.text = text;
      gameObject.SetActive(true);
   }

   private void OnClickYesBtn()
   {
      AllSceneCanvas.instance.isOpenMenu = false;
      gameObject.SetActive(false);
      if (popUpMode == "restart")
      {
         InGameMenuController.instance.gameObject.SetActive(false);
         //SceneSystem.instance.TestNextStage("Lobby");
         SaveLoadManager.instance.FailedMoney();
         SceneSystem.instance.RestList();
         SceneManager.LoadScene("Lobby");
         AllSceneCanvas.instance.SetMonsterCnt(0);
      }
      else if (popUpMode == "title")
      {
         SaveLoadManager.instance.FailedMoney();
         SceneManager.LoadScene("Title");
         // SceneSystem.instance.TestNextStage("Title");
         ResetInstance();
      }
   }

   private void OnClickNoBtn()
   {
      gameObject.SetActive(false);
   }
   private void ResetInstance()
   {
      Destroy(AllSceneCanvas.instance.gameObject);
      Destroy(SceneSystem.instance.gameObject);
      Destroy(PlayerManager.instance.gameObject);
   }
}
