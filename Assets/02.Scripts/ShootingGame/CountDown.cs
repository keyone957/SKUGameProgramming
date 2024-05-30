using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// 게임 시작전 3,2,1, GO 게임신 정지 함수
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-05-30

public class CountDown : MonoBehaviour
{
public TMP_Text countdownText;
    public float countdownTime = 3.0f; // 카운트다운 시간

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        // 게임을 멈춥니다.
        Time.timeScale = 0;
        float realTimeCountdown = countdownTime;

        while (realTimeCountdown > 0)
        {
            countdownText.text = realTimeCountdown.ToString("0"); // 남은 시간을 텍스트로 표시
            yield return new WaitForSecondsRealtime(1.0f); 
            realTimeCountdown--;
        }

        countdownText.text = "Go!"; 
        yield return new WaitForSecondsRealtime(1.0f); 

        countdownText.gameObject.SetActive(false); 

        Time.timeScale = 1;
    }
}