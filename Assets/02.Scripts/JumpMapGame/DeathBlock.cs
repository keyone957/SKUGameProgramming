using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추락시 팝업창을 활성화하는 컴포넌트
// 죽었을때 효과음
// 최초 작성자 : 장현우
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-10

public class DeathBlock : MonoBehaviour
{
    private Vector3 respawnPosition = new Vector3(-33f, 37f, 0f);  // 플레이어가 리스폰될 위치

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 위치 초기화
            ResetPlayerPosition(collision.gameObject);
            SoundManager._instance.PlaySound(Define._restartJumpMap);
        }
    }

    private void ResetPlayerPosition(GameObject player)
    {
        // 플레이어를 리스폰 위치로 이동
        player.transform.position = respawnPosition;
    }
}