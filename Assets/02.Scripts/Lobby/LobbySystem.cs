using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//로비씬 들어갈때 해야할 동작들 
//플레이어 정보 초기화 함수 추가 TODO: 나중에는 플레이어 무기, 돈, 슬라임 색 PlayerPref로 받아올 예정
//플레이어 초기화
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-31
public class LobbySystem : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(SceneStartSequence());
        InitializeLobbyScene();
    }

    private void InitializeLobbyScene()
    {
        SceneSystem.instance.isClearStage = true;
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        PlayerManager.instance.playerHp = 5;
        PlayerManager.instance.playerMoney = 0;
        PlayerManager.instance.AssignSpriteRenderers();
        PlayerManager.instance.InitializePlayer();
        AllSceneCanvas.instance.PlayerHPChange(PlayerManager.instance.playerHp);
        AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
        AllSceneCanvas.instance.SetStageText("Lobby");
    }

    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._lobbyBgm);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._lobbyBgm);
    }

}
