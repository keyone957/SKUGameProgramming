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
            Destroy(gameObject);


        }
    }
}