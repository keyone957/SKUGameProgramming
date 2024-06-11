using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// 게임 시작전 3,2,1, GO 게임신 정지 함수
//코드 리팩토링
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-11
public class CoinBonuCounter : MonoBehaviour
{
    public GameObject Panel;
    public TMP_Text Count;
    public float countdownTime = 3.0f;
    public GameObject failPanel;
    public GameObject successPanel;

    private bool gameStarted = false;

    private void Start()
    {
        AllSceneCanvas.instance.isOpenMenu = true;
        failPanel.SetActive(false);
        successPanel.SetActive(false);
        Panel.SetActive(true);
        Count.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
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
        yield return new WaitForSecondsRealtime(1.0f);

        Count.gameObject.SetActive(false);

        AllSceneCanvas.instance.isOpenMenu = false;
    }
}
