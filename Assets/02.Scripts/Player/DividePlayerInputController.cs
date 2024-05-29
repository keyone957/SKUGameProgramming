using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//플레이어를 분열 슬라임이 따라다니게
//애니메이터 해시 파라미터사용
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-29
public class DividePlayerInputController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float moveSpeed;
    private int jumpCnt;
    public Transform target; 
    public Vector2 followOffset; //따라다니는 오브젝트의 떨어질 거리
    
    private readonly int hashIdle = Animator.StringToHash("IsIdle");
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private void Update()
    {
     Idle();
     Move();
     Attack();
    }

    private void Idle()
    {
        anim.SetBool(hashMove, false);
        anim.SetBool(hashIdle,true);
    }

    //움직일 때 ,분열 슬라인오브젝트가 같이 따라다니게
    private void Move()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            Vector3 targetForward = target.right;
           
            Vector3 offset = targetForward * followOffset.x + target.up * followOffset.y;
            Vector3 followPosition = targetPosition - offset;
           
            transform.position = followPosition;
            if (Input.GetKey(KeyCode.A))
            { 
                anim.SetBool(hashIdle, false);
                anim.SetBool(hashMove, true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Attack();
            }
            else if (Input.GetKey(KeyCode.D)) 
            {         
                anim.SetBool(hashIdle, false);
                anim.SetBool(hashMove, true);
                transform.rotation = Quaternion.Euler(0, -180f, 0);
                Attack();
            }
        }

    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool(hashAttack, true);
        }
    }
    private void EndAttack()
    {
        anim.SetBool(hashMove, false);
        anim.SetBool(hashAttack, false);
        anim.SetBool(hashIdle, true);
    }
}
