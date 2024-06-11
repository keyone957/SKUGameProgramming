using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 원 닿게 될시 타이머 시작
//코드 리팩토링
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-11

public class CoinTimerScript : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float countdownTime = 10f;
    public GameObject failPanel;
    public GameObject successPanel;

    public void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        float timer = countdownTime;
        while (timer > 0f)
        {
            if (CoinManager.instance.AreAllCoinsCollected())
            {
                successPanel.SetActive(true);
                AllSceneCanvas.instance.isOpenMenu = true;
                yield break;
            }

            countdownText.text = Mathf.RoundToInt(timer).ToString();
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        countdownText.text = "0";
        StopCountdown();
    }

    public void StopCountdown()
    {
        AllSceneCanvas.instance.isOpenMenu = true;

        if (CoinManager.instance.AreAllCoinsCollected())
        {
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
}