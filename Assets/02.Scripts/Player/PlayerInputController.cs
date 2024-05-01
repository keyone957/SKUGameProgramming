using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//플레이어(슬라임) Input관련 컴포넌트
//플레이어 Input에 따라 State부여하여 상태 부여 (FSM사용)
//현재 이동, 공격 구현
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-01
public class PlayerInputController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Move,
        Attack,
        Die,
        Jump,
        Damaged,
        
    }
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private List<AudioClip> playerSound=new List<AudioClip>();
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private GameObject jumpEffect;
    private Vector2 moveVelocity;
    public float moveSpeed = 5.0f;
    private State _state;
    
    void Start()
    {
        _state = State.Idle;
    }

    void Update()
    {
        switch (_state)
        {
            case State.Idle:
                HandleIdleState();
                break;
            case State.Move:
                HandleMoveState();
                break;
            case State.Damaged:
                break;
            case State.Attack:
                HandleAttackState();
                break;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void HandleIdleState()
    {
        anim.SetBool("IsMove", false);
        anim.SetBool("IsAttack",false);
        anim.SetBool("IsIdle", true);
     
        // Check for input to transition to Move state
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            _state = State.Move;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            // GameObject j = Instantiate(jumpEffect);
            // j.transform.parent = gameObject.transform;
           // j.transform.localPosition = new Vector3(0, -0.1f, 0f);
            // j.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            _state = State.Attack;
            // Destroy(j,0.4f);
        }
    }
    public void HandleMoveState()
    {
        anim.SetBool("IsIdle", false);
        // anim.SetBool("IsAttack",false);
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("IsAttack",true);
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _state = State.Idle;
        }
    }

    private void HandleAttackState()
    {
        anim.SetBool("IsAttack", true);
    }

    public void EndAttackAnim()
    {
        _state = State.Idle;
    }
    public void PlayEffect()
    {
        playerAudioSource.PlayOneShot(playerSound[1]);
    }
}
