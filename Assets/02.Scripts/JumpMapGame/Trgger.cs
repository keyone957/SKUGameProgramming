using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 트리거 스크립트
// 도달하면 다음 씬 로드, 우선 현재 씬으로 설정
// 최초 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-06-02

public class TriggerGameOver : MonoBehaviour
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
        // 게임 종료 관련 로직을 여기에 추가
        // 예: 메인 메뉴로 돌아가기, 특정 씬 로드하기 등
        SceneManager.LoadScene("JumpMapBonus");
    }
}
