using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//몬스터 AI FSM 사용하여 구현중
//TODO: 피격 및 공격 구현해야함
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-24
public class Monster : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    [SerializeField] public int hp;
    [SerializeField] public int damage;
    [SerializeField] public float traceDist;
    [SerializeField] public float attackDist;
    [SerializeField] public float moveSpeed;
    public State state = State.IDLE;
    public bool isDie = false;

    private Transform monsterTrans;
    private Transform playerTrans;
    private Animator anim;
    private Rigidbody2D rb;

    // private readonly int hashIdle=Animator.StringToHash("IsMove");
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDead = Animator.StringToHash("Dead");


    void Start()
    {
        monsterTrans = GetComponent<Transform>();
        playerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CheckMonsterStart());
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterStart()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            if (state == State.DIE) yield break;

            float distance = Vector2.Distance(playerTrans.position, monsterTrans.position);
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    anim.SetBool(hashMove, false);
                    rb.velocity = Vector2.zero;
                    FreezePositionX(false);
                    break;
                case State.TRACE:
                    anim.SetBool(hashMove, true);
                    anim.SetBool(hashAttack, false);
                    FreezePositionX(false);
                    TracePlayer();
                    break;
                case State.ATTACK:
                    anim.SetBool(hashMove, false);
                    anim.SetBool(hashAttack, true);
                    FreezePositionX(true);
                    break;
                case State.DIE:
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void TracePlayer()
    {
        Vector2 dir = new Vector2(playerTrans.position.x - monsterTrans.position.x, 0).normalized;
        rb.velocity = dir * moveSpeed;
        if (playerTrans.position.x < monsterTrans.position.x)
        {
            // 플레이어가 왼쪽에 있을 때
            monsterTrans.localRotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            // 플레이어가 오른쪽에 있을 때
            monsterTrans.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FreezePositionX(bool freeze)
    {
        rb.constraints = freeze
            ? RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation
            : RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnDrawGizmos()
    {
        // 디버그를 위해 추적 및 공격 범위를 그려줌
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, traceDist);
    }
}