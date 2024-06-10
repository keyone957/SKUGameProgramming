using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 점프맵에서 충돌 시 튕겨나감, 발판처럼 쓰여서 y축으로는 가지 않도록 수정
// 충돌시 이펙트
// 최초 작성자 : 장현우
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-10

public class Bomb : MonoBehaviour
{
    public float forceAmount = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 forceDirection = (other.transform.position - transform.position).normalized;
                forceDirection.y = 0;  // y축 성분을 0으로 설정하여 옆으로만 힘을 가함
                playerRigidbody.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
                SoundManager._instance.PlaySound(Define._bombSound);
            }
        }
    }
}

