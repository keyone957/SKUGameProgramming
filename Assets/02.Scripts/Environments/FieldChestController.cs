using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 그냥 필드에 숨겨져있는 보물상자
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-07
public class FieldChestController : MonoBehaviour
{
    [SerializeField] private GameObject keyBoardUI;
    [SerializeField] private int chestMoney;
    private Animator chestAnim;
    private bool isEnter;

    //Animator 파라미터의 해시값 추출
    private readonly int hashGoldenIdle = Animator.StringToHash("IdleGolden");
    private readonly int hashGoldenOpen = Animator.StringToHash("OpenGolden");

    private void Start()
    {
        chestAnim = GetComponent<Animator>();
        isEnter = false;
    }

    private void Update()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.W))
        {
            SoundManager._instance.PlaySound(Define._getCoin);
            ChestAnim();
        }
        chestAnim.SetBool(hashGoldenIdle, true);
    }

    private void ChestAnim()
    {
        chestAnim.SetTrigger(hashGoldenOpen);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            keyBoardUI.SetActive(true);
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        keyBoardUI.SetActive(false);
        isEnter = false;
    }
    public void OpenChestEndEvent()
    {
        // PlayerManager.instance.playerMoney += chestMoney;
        SaveLoadManager.instance.GetMoney(chestMoney);
        AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
        Destroy(gameObject);
    }
}