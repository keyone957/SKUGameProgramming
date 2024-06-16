using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//게임오버 패널 컴포넌트
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-06-07
public class GameOverPanel : MonoBehaviour
{
   [SerializeField] private Button titleBtn;
   [SerializeField] private Button lobbyBtn;

   private void Start()
   {
      titleBtn.onClick.AddListener(OnClickTitleBtn);
      lobbyBtn.onClick.AddListener(OnClickLobbyBtn);
   }

   private void OnClickTitleBtn()
   {
      SaveLoadManager.instance.FailedMoney();
      SoundManager._instance.PlaySound(Define._gameStartBtn);
      SceneManager.LoadScene("Title");
      Destroy(AllSceneCanvas.instance.gameObject);
      Destroy(SceneSystem.instance.gameObject);
      Destroy(PlayerManager.instance.gameObject);
   }

   private void OnClickLobbyBtn()
   {
      SaveLoadManager.instance.FailedMoney();
      SoundManager._instance.PlaySound(Define._gameStartBtn);
      SceneSystem.instance.RestList();
      SceneManager.LoadScene("Lobby");
      AllSceneCanvas.instance.SetMonsterCnt(0);
      AllSceneCanvas.instance.gameOverPanel.SetActive(false);
   }
}
