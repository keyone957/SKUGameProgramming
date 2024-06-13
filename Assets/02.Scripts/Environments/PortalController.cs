using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//포탈에 닿았을때 수행해야할 동작들
//테스트코드 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-11
public class PortalController : MonoBehaviour
{
    [SerializeField] private GameObject keyBoardUI;
    [SerializeField] private List<Sprite> portalSpr = new List<Sprite>();
    private bool isEnter;
    [SerializeField] public SceneSystem.NextStageType nextStageType;
    [SerializeField] public string TestSceneName;
    private void Start()
    {
        isEnter = false;
        BeforeBonusStage();
    }

    private void Update()
    {
        if (isEnter&&Input.GetKeyDown(KeyCode.W))
        {
            // SceneSystem.instance.TestNextStage(TestSceneName);
            if (SceneManager.GetActiveScene().name == "BossStage")
            {
                SceneSystem.instance.TestNextStage("ClearScene");
            }
            else
            {
                SceneSystem.instance.GoNextStage(nextStageType);
            }
            SoundManager._instance.PlaySound(Define._portalSound);
        }
    }

    public void BeforeBonusStage()
    {
        if (SceneSystem.instance.currentStage == SceneSystem.instance.bonusStageOrder-1)
        {
            nextStageType = SceneSystem.NextStageType.Bonus;
        }

        if (SceneSystem.instance.currentStage == SceneSystem.instance.bossStageOrder - 1)
        {
            nextStageType = SceneSystem.NextStageType.Boss;
        }

        if (SceneSystem.instance.currentStage == SceneSystem.instance.bossStageOrder - 2)
        {
            nextStageType = SceneSystem.NextStageType.BeforeBoss;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& SceneSystem.instance.isClearStage)
        {
            keyBoardUI.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = portalSpr[1];
            SoundManager._instance.PlaySound(Define._openDoor);
            isEnter = true;
        }
        else if(other.gameObject.CompareTag("Player")&& !SceneSystem.instance.isClearStage)
        {
            SoundManager._instance.PlaySound(Define._accessDenied);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (SceneSystem.instance.isClearStage)
        {
            keyBoardUI.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = portalSpr[0];
            SoundManager._instance.PlaySound(Define._closeDoor);
            isEnter = false;
        }
    }
}