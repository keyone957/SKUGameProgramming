using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // 폭발력 조절 변수
    public float explosionForce = 1000.0f;

    // 트리거 감지 이벤트
    private void OnTriggerEnter(Collider other)
    {
        // 트리거된 오브젝트가 플레이어인지 확인
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger!");

            // 플레이어를 폭발 방향으로 밀어냅니다.
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 direction = other.transform.position - transform.position;
                playerRigidbody.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
            }

            // 폭탄 파괴 (옵션)
            Destroy(gameObject);
        }
    }
}

