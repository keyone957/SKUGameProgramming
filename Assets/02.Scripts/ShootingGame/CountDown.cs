using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{
public Text countdownText;
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

        // 게임을 다시 시작합니다.
        Time.timeScale = 1;
    }
}