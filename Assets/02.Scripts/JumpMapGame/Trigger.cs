using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 트리거 스크립트
// 싱글톤 수정
// 최초 작성자 : 장현우
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-09

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        // 충돌한 오브젝트의 이름이 Player일 경우 게임 종료
        if (other.gameObject.name == "Player")
        {
            GameOver();
        }
    }

    // 게임 종료 로직
    private void GameOver()
    {
        Debug.Log("Game Over!");

        // JumpMapSystem의 TriggerFlag 메서드 호출하여 엔드 패널 활성화
        JumpMapSystem.instance.TriggerFlag();
    }
}
