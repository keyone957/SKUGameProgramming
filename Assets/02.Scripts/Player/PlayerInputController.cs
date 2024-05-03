using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

//플레이어(슬라임) Input관련 컴포넌트
//FSM 삭제. ==> 코드 복잡해질 것 같음.
//현재 이동, 공격 구현
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-03
public class PlayerInputController : MonoBehaviour
{
 
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private List<AudioClip> playerSound = new List<AudioClip>();
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] public float jumpForce;
    [SerializeField] private Collider2D swordCollider;
    private int jumpCnt;
    private Vector2 moveVelocity;
    public float moveSpeed;
    private bool isAttackEnd;

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            HandleMoveState();
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            HandleIdleState();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            HandleAttackState();
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < 2)
        {
            HandleJumpState();
        }
    }
    private void HandleIdleState()
    {
        anim.SetBool("IsMove", false);
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsIdle", true);
    }

    public void HandleMoveState()
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsMove", true);
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        moveVelocity = moveInput.normalized * moveSpeed;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (moveHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        }

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void HandleAttackState()
    {
        anim.SetBool("IsAttack", true);
        swordCollider.enabled = true;
    }

    private void EndAttack()
    {
        anim.SetBool("IsMove", false);
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsIdle", true);
        swordCollider.enabled = false;
    }

    private void StartAttack()
    {
        PlayEffect(playerSound[1]);
    }

    public void HandleJumpState()
    {
        PlayEffect(playerSound[0]);
        GameObject jumpVfx = Instantiate(jumpEffect);
        jumpVfx.transform.parent = gameObject.transform;
        jumpVfx.transform.localPosition = new Vector2(0, -0.1f);
        jumpVfx.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpCnt++;
        Destroy(jumpVfx,0.4f);
    }

    public void PlayEffect(AudioClip effectSound)
    {
        playerAudioSource.PlayOneShot(effectSound);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 0;
        }
    }
}