using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 게임 시작전 3,2,1, GO 게임신 정지 함수
//3 2 1 후 효과음
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-10

public class CountDown : MonoBehaviour
{
    public GameObject Panel;
    public TMP_Text Count;
    public float countdownTime = 3.0f;
    public GameObject Finish;
    [SerializeField] private bool gameStarted;

    private void Start()
    {
        gameStarted = false;
        Finish.SetActive(false);
        Panel.SetActive(true);
        Count.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Panel.SetActive(false);
        StartCoroutine(StartCountdown());
        gameStarted = true;
    }

    private IEnumerator StartCountdown()
    {
        Count.gameObject.SetActive(true);

        float realTimeCountdown = countdownTime;

        while (realTimeCountdown > 0)
        {
            Count.text = realTimeCountdown.ToString("0");
            yield return new WaitForSecondsRealtime(1.0f);
            realTimeCountdown--;
        }

        Count.text = "Go!";
        SoundManager._instance.PlaySound(Define._jumpMapStart);
        yield return new WaitForSecondsRealtime(1.0f);
        
        Count.gameObject.SetActive(false);

        AllSceneCanvas.instance.isOpenMenu = false;

    }
}
