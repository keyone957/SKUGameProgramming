using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� ĳ���� ���콺 Ŭ���� ���� ����
// ���� �ۼ���: �ϰ渲
// ���� ������: 2024-06-04
public class CoinBonusMapPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 startPosition; // 캐릭터의 시작 위치
    public float fallThreshold = -10f; // 캐릭터가 떨어질 위치의 Y 값

    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // 처음 위치를 저장
    }

    void Update()
    {
        Move();
        Jump();
        CheckFall();
    }

    void Move()
    {
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero; 
    }
}