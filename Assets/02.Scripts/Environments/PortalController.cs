using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//포탈에 닿았을때 수행해야할 동작들
//클리어시 현재 스테이지 ++
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-31
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
            // SceneSystem.instance.GoNextStage(nextStageType);
            SceneSystem.instance.currentStage++;
            SceneSystem.instance.TestNextStage(TestSceneName);
        }
    }

    public void BeforeBonusStage()
    {
        if (SceneSystem.instance.currentStage == SceneSystem.instance.bonusStageOrder-1)
        {
            nextStageType = SceneSystem.NextStageType.Bonus;
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