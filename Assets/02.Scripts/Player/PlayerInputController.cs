using System;
using System.Collections.Generic;
using UnityEngine;

//플레이어(슬라임) Input관련 컴포넌트
//플레이어 사망함수 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-07
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private List<AudioClip> playerSound = new List<AudioClip>();
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private GameObject smallSlime;
    [SerializeField] private int maxJumpCnt;
    [SerializeField] public bool canDivide; //점프맵에서 혹여나 분열을 안쓸수도 있으니 필요한 bool값
    [SerializeField] private SpriteRenderer slimeSpr;
    [SerializeField] private SpriteRenderer divideSlimeSpr;
    private bool isDivide;
    private int jumpCnt;
    private float divideCooldown = 2f; //분열 스킬 쿨타임
    private float nextDivideTime = 0f;
    

    private bool isInvincible = false; //무적상태 bool값

    //Animator 파라미터의 해시값 추출
    private readonly int hashIdle = Animator.StringToHash("IsIdle");
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");

    private void Update()
    {
        if (PlayerManager.instance.isDiePlayer)
        {
            return;
        }

        Idle();
        Move();
        Attack();
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < maxJumpCnt)
        {
            Jump();
        }

        if (canDivide && Input.GetKeyDown(KeyCode.K) && Time.time > nextDivideTime)
        {
            if (!isDivide)
            {
                DivideSlime();
                nextDivideTime = Time.time + divideCooldown;
            }
            else
            {
                ResetSlime();
                nextDivideTime = Time.time + divideCooldown;
            }
        }
    }

    private void Idle()
    {
        anim.SetBool(hashMove, false);
        anim.SetBool(hashIdle, true);
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool(hashIdle, false);
            anim.SetBool(hashMove, true);
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Max(rb.velocity.x, -moveSpeed), rb.velocity.y); //속도제한
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Attack();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool(hashIdle, false);
            anim.SetBool(hashMove, true);
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Min(rb.velocity.x, moveSpeed), rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, -180f, 0);
            Attack();
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) // 이동 키를 뗀 경우
        {
            anim.SetBool(hashIdle, true);
            anim.SetBool(hashMove, false);
            rb.velocity = new Vector3(rb.velocity.normalized.x, rb.velocity.y);
            Attack();
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
        swordCollider.enabled = false;
    }

    private void StartEffect()
    {
        attackEffect.SetActive(true);
    }

    private void EndEffect()
    {
        attackEffect.SetActive(false);
    }

    private void StartAttack()
    {
        swordCollider.enabled = true;
        SoundManager._instance.PlaySound(Define._playerAttack);
        attackEffect.SetActive(true);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < maxJumpCnt)
        {
            SoundManager._instance.PlaySound(Define._playerJump);
            GameObject jumpVfx = Instantiate(jumpEffect);
            jumpVfx.transform.parent = gameObject.transform;
            jumpVfx.transform.localPosition = new Vector2(0, -0.1f);
            jumpVfx.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCnt++;

            Destroy(jumpVfx, 0.4f);
            Attack();
        }
    }

    private void DivideSlime()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Color divideSlimeColor = divideSlimeSpr.color;
            divideSlimeColor.a = 1.0f;
            divideSlimeSpr.color = divideSlimeColor;
            ////////////////////////////////////////////////////////////////////////////////
            SetSlimeStat(new Vector3(6, 6, 6), true, 3, 17f, 13f, 1, true);
        }
    }

    private void ResetSlime()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            /////////////////////////////////////////////////////////////////
            SetSlimeStat(new Vector3(8, 8, 8), false, 2, 20f, 10f, 2, false);
        }
    }

    private void SetSlimeStat(Vector3 slimeScale, bool slimeActive, int jCnt, float jForce, float mSpeed,
        int playerPower, bool divide)
    {
        transform.localScale = slimeScale;
        smallSlime.SetActive(slimeActive);
        maxJumpCnt = jCnt;
        jumpForce = jForce;
        moveSpeed = mSpeed;
        PlayerManager.instance.playerPower = playerPower;
        isDivide = divide;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MonsterAttackCol") && !isInvincible)
        {
            SoundManager._instance.PlaySound(Define._damagedSlime);
            PlayerManager.instance.playerHp -= other.gameObject.transform.parent.GetComponent<Monster>().damage;
            AllSceneCanvas.instance.PlayerHPChange(PlayerManager.instance.playerHp);
            OnDamaged(other.gameObject.transform.position);
            if (PlayerManager.instance.playerHp <= 0)
            {
                PlayerManager.instance.isDiePlayer = true;
                PlayerManager.instance.DestroyAllMonsters();
                SoundManager._instance.PlaySound(Define._playerDie);
                SceneSystem.instance._fadeOverlay.DoFadeOutDie(1.0f);
            }
        }
    }

    //무적상태 활성화
    private void OnDamaged(Vector2 targetPos)
    {
        isInvincible = true;
        Color slimeColor = slimeSpr.color;
        slimeColor.a = 0.4f;
        slimeSpr.color = slimeColor;
        if (isDivide)
        {
            Color divideSlimeColor = divideSlimeSpr.color;
            divideSlimeColor.a = 0.4f;
            divideSlimeSpr.color = divideSlimeColor;
        }

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        Invoke("ResetInvincibility", 2);
    }

    //무적상태 원래대로 돌아오게
    private void ResetInvincibility()
    {
        isInvincible = false;
        Color slimeColor = slimeSpr.color;
        slimeColor.a = 1.0f;
        slimeSpr.color = slimeColor;
        if (isDivide)
        {
            Color divideSlimeColor = divideSlimeSpr.color;
            divideSlimeColor.a = 1.0f;
            divideSlimeSpr.color = divideSlimeColor;
        }
    }

}