using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추락시 재시작하는 컴포넌트
// 떨어지면 재시작
// 최초 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-06-05

public class DeathBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JumpMapSystem.Instance.ResetScene();
        }
    }
}