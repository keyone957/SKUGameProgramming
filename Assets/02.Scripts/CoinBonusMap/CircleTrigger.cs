using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 원 닿게 될시, 코인 생성 및 사라지는 트리거
//코드 리팩토링
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-11
public class CircleTrigger : MonoBehaviour
{
    private bool collided = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collided)
        {
            SoundManager._instance.PlayBGM(Define._coinStageBGM);
            collided = true;
            Destroy(gameObject);
            CoinManager.instance.ActivateAllCoins();
            CoinTimerScript timerComponent = FindObjectOfType<CoinTimerScript>();
            if (timerComponent != null)
            {
                timerComponent.StartCountdown();
            }
            
        }
    }
}