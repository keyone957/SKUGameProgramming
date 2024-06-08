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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            if (coinManager != null)
            {
                Debug.Log("CoinManager found, removing coin.");
                coinManager.RemoveCoin(gameObject);  // 리스트에서 코인을 제거
                Destroy(gameObject);  // 코인 오브젝트 파괴
                coinManager.CheckAllCoinsCollected();
            }
            else
            {
                Debug.Log("CoinManager not found.");
                Destroy(gameObject); 
            }

        }
    }
}