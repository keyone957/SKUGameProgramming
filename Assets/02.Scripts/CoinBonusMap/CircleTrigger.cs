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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;
            Destroy(gameObject);

            // Timer 스크립트 컴포넌트를 찾아서 Countdown를 시작합니다.
            CoinTimerScript CoinTimerScript = FindObjectOfType<CoinTimerScript>();
            if (CoinTimerScript != null)
            {
                CoinTimerScript.StartCountdown();
            }
        }
    }
}