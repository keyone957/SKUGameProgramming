using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ŭ����� �ý���
// ���� �ۼ��� : ȫ����
// ������ : 
// ���� ������ : 2024-06-12
public class ClearSceneSystem : MonoBehaviour
{
    private void Start()
    {
        InitialClearScene();
        StartCoroutine(SceneStartSequence());
    }

    private void InitialClearScene()
    {
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
}