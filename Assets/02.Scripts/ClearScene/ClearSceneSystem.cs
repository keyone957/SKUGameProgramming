using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//클리어씬 시스템
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-06-12
public class ClearSceneSystem : MonoBehaviour
{
    [SerializeField] private Button finishBtn;
    private void Start()
    {
        InitialClearScene();
        finishBtn.onClick.AddListener(OnClickFinishBtn);
        StartCoroutine(SceneStartSequence());
    }

    private void InitialClearScene()
    {
        PlayerManager.instance.AssignSpriteRenderers();
        PlayerManager.instance.AnotherScenePlayer();
        AllSceneCanvas.instance.monsterCnt.SetActive(true);
        AllSceneCanvas.instance.playerHpUI.SetActive(true);
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        AllSceneCanvas.instance.SetStageText("Clear!!!");
    }

    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._clearStageBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._clearStageBGM);
    }

    private void OnClickFinishBtn()
    {
        // SceneSystem.instance.TestNextStage("Title");
        SaveLoadManager.instance.GetMoney(4000);
        SceneManager.LoadScene("Title");
        Destroy(AllSceneCanvas.instance.gameObject);
        Destroy(SceneSystem.instance.gameObject);
        Destroy(PlayerManager.instance.gameObject);
    }
}