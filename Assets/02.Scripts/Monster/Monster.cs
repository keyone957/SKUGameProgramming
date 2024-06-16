using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//몬스터 공격할때 에니메이션 이벤트 추가
//몬스터 피격, 죽음 상태 추가
//몬스터 타입에 따라 효과음다르게
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-30
public class Monster : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public enum MonsterType
    {
        Wolf,
        Skeleton
    }

    [SerializeField] public int hp;
    private int curHp;
    [SerializeField] public int damage;
    [SerializeField] public float traceDist;
    [SerializeField] public float attackDist;
    [SerializeField] public float moveSpeed;
    [SerializeField] private Collider2D monsterAttack;
    [SerializeField] private GameObject hpCanvas;
    [SerializeField] public Slider monsterHpBarSlider;
    [SerializeField] public MonsterType monsterType;
    public State state = State.IDLE;
    public bool isDie = false;

    private Transform monsterTrans;
    [SerializeField]private Transform playerTrans;
    private Animator anim;
    private Rigidbody2D rb;
   

    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDead = Animator.StringToHash("Dead");


    void Start()
    {
        monsterTrans = GetComponent<Transform>();
        PlayerInputController playerController = FindObjectOfType<PlayerInputController>();
        if (playerController != null)
        {
            playerTrans = playerController.transform;
        }
        else
        {
            Debug.LogError("PlayerInputController not found!");
        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        curHp = hp;
        StartCoroutine(CheckMonsterStart());
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterStart()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.05f);
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
                    isDie = true;
                    Die();
                    yield return new WaitForSeconds(1.25f);
                    Destroy(this.gameObject);
                    break;
            }

            yield return new WaitForSeconds(0.05f);
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
            hpCanvas.transform.localRotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            // 플레이어가 오른쪽에 있을 때
            monsterTrans.localRotation = Quaternion.Euler(0, 0, 0);
            hpCanvas.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //몬스터가 공격할때 밀려나지 않게
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            OnDamaged();
            Debug.Log("해골 아파용");
        }
    }

    private void OnDamaged()
    {
        if (monsterType == MonsterType.Skeleton)
        {
            SoundManager._instance.PlaySound(Define._damagedSkeleton);
        }
        else if (monsterType == MonsterType.Wolf)
        {
            SoundManager._instance.PlaySound(Define._damagedWolf);
        }


        curHp -= PlayerManager.instance.playerPower;
        monsterHpBarSlider.value = (float)curHp / hp;
        if (curHp <= 0)
        {
            state = State.DIE;
        }
    }

    private void StartAttack()
    {
        monsterAttack.enabled = true;
    }

    private void Die()
    {
        if (monsterType == MonsterType.Skeleton)
        {
            SoundManager._instance.PlaySound(Define._deathSkeleton);
        }
        else if (monsterType == MonsterType.Wolf)
        {
            SoundManager._instance.PlaySound(Define._deathWolf);
        }
        // PlayerManager.instance.playerMoney += 10;
        SaveLoadManager.instance.GetMoney(10);
        AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
        DungeonSystem.instance.monsterCnt--;
        AllSceneCanvas.instance.SetMonsterCnt(DungeonSystem.instance.monsterCnt);
        anim.SetTrigger(hashDead);
        monsterAttack.enabled = false;
        rb.simulated = false;
    }

    private void EndAttack()
    {
        monsterAttack.enabled = false;
    }
}