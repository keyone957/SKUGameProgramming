using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimationHandler : MonoBehaviour
{public float jumpForce = 10f; // 점프할 때 가해질 힘의 크기
    public int maxJumps = 2; // 최대 점프 횟수

    private Rigidbody2D rb;
    private int jumpsRemaining;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // 수직 방향의 속도 초기화
        // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 점프 힘 추가
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
        jumpsRemaining--; // 점프 가능 횟수 감소

        // 만약 더블 점프를 허용하지 않을 경우 주석 처리된 코드를 사용하세요.
        // jumpsRemaining = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps; // 땅에 닿으면 점프 횟수 초기화
        }
    }
}
