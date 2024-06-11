using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 코인 닿게 될시
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-06
public class CoinTrigger : MonoBehaviour
{
    private bool collided = false;
    //소리나는부분 주석 처리
    //    public AudioClip coinSound; 
    //    private AudioSource audioSource;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            coinManager.RemoveCoin(gameObject);
            coinManager.CheckAllCoinsCollected();
        }
    }
    /*
        private void PlayCoinSound()
        {
            if (coinSound != null)
            {
                audioSource.clip = coinSound;
                audioSource.Play();
            }
        }
    }*/
}