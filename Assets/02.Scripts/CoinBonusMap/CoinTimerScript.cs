using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// 원 닿게 될시 타이머 시작
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-06

public class CoinTimerScript : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float countdownTime = 10f;
    public GameObject failPanel;
    public List<GameObject> coins = new List<GameObject>();
    private CoinManager coinManager;
    private AudioSource backgroundMusicSource;

    void Start()
    {
        backgroundMusicSource = GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>();
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
        coinManager = CoinManager.Instance;
    }

    private IEnumerator CountdownRoutine()
    {
        float timer = countdownTime;
        while (timer > 0f)
        {
            countdownText.text = Mathf.RoundToInt(timer).ToString();
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }
        countdownText.text = "0";
        StopCountdown();
    }

    public void StopCountdown()
    {
        Time.timeScale = 0f;

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }

        if (coinManager.AreAllCoinsCollected())
        {
            coinManager.ShowSuccessPanel();
        }
        else
        {
            if (failPanel != null)
            {
                failPanel.SetActive(true);
            }
        }
    }
}