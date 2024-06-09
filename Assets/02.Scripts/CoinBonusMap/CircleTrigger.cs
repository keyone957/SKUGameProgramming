using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 원 닿게 될시, 코인 생성 및 사라지는 트리거
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-06
public class CircleTrigger : MonoBehaviour
{
    private bool collided = false;
    public AudioClip newBackgroundMusic;
    private static AudioSource bgmAudioSource;

    void Start()
    {
        if (bgmAudioSource == null)
        {
            GameObject bgmObject = new GameObject("BackgroundMusic");
            bgmAudioSource = bgmObject.AddComponent<AudioSource>();
            bgmAudioSource.loop = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;

            if (newBackgroundMusic != null)
            {
                bgmAudioSource.clip = newBackgroundMusic;
                bgmAudioSource.Play();
            }

            Destroy(gameObject);

            CoinManager coinManager = FindObjectOfType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.ActivateAllCoins();
            }

            CoinTimerScript timerComponent = FindObjectOfType<CoinTimerScript>();
            if (timerComponent != null)
            {
                timerComponent.StartCountdown();
            }
        }
    }
}