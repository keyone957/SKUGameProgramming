using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//슬라임이 분열되었을때 분열된 슬라임 관리하는 컴포넌트
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-05-06
public class DividePlayerInputController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    public float moveSpeed;
    private int jumpCnt;
    private void Update()
    {
       Idle();
       Move();
       Attack();
       if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < 2)
       {
           Jump();
       }
       
    }

    private void Idle()
    {
        anim.SetBool("IsMove", false);
        anim.SetBool("IsIdle",true);
    }

    private void Move()
    {
     
        if (Input.GetKey(KeyCode.A))
        { 
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsMove", true);
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Max(rb.velocity.x, -moveSpeed), rb.velocity.y);//속도제한
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {         
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsMove", true);
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Min(rb.velocity.x, moveSpeed), rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) // 이동 키를 뗀 경우
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsMove", false);
            rb.velocity = new Vector3(rb.velocity.normalized.x, rb.velocity.y);
        }

    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("IsAttack", true);
        }
    }
    private void EndAttack()
    {
        anim.SetBool("IsMove", false);
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsIdle", true);
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        jumpCnt++;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 0;
        }
    }
}
