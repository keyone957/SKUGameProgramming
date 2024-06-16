using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//���ӿ��� �г� ������Ʈ
// ���� �ۼ��� : ȫ����
// ������ : 
// ���� ������ : 2024-06-07
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
