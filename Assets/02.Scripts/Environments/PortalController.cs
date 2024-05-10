using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//포탈에 닿았을때 수행해야할 동작들
//게임 클리어 조건 추가, 조건이 맞고 W를 누르면 다음 씬으로 이동 가능
//test 씬이동 함수 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-10
public class PortalController : MonoBehaviour
{
    [SerializeField] private GameObject keyBoardUI;
    [SerializeField] private List<Sprite> portalSpr = new List<Sprite>();
    [SerializeField] private List<AudioClip> doorSound = new List<AudioClip>();
    private AudioSource doorAudioSource;
    private bool isEnter;
    [SerializeField] public SceneSystem.NextStageType nextStageType;
    [SerializeField] public string TestSceneName;
    private void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
        isEnter = false;
    }

    private void Update()
    {
        if (isEnter&&Input.GetKeyDown(KeyCode.W))
        {
            // SceneSystem.instance.GoNextStage(nextStageType);
            SceneSystem.instance.TestNextStage(TestSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& SceneSystem.instance.isClearStage)
        {
            keyBoardUI.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = portalSpr[1];
            doorAudioSource.PlayOneShot(doorSound[0]);
            isEnter = true;

        }
        else if(other.gameObject.CompareTag("Player")&& !SceneSystem.instance.isClearStage)
        {
            doorAudioSource.PlayOneShot(doorSound[2]);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (SceneSystem.instance.isClearStage)
        {
            keyBoardUI.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = portalSpr[0];
            doorAudioSource.PlayOneShot(doorSound[1]);
            isEnter = false;
        }
    }
}