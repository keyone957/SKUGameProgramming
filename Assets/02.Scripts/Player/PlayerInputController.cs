using System.Collections.Generic;
using UnityEngine;

//플레이어(슬라임) Input관련 컴포넌트
//애니메이터 해시테이블사용해 불러오기.
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-24
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private List<AudioClip> playerSound = new List<AudioClip>();
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] public float jumpForce;
    [SerializeField] public float moveSpeed;
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private GameObject smallSlime;
    [SerializeField] public int maxJumpCnt;
    [SerializeField] public bool canDivide; //점프맵에서 혹여나 분열을 안쓸수도 있으니 필요한 bool값
    private bool isDivide;
    private int jumpCnt;
    private float divideCooldown = 2f;
    private float nextDivideTime = 0f;

    //Animator 파라미터의 해시값 추출
    private readonly int hashIdle = Animator.StringToHash("IsIdle");
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");

    private void Update()
    {
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
            swordCollider.enabled = true;
        }
    }

    private void EndAttack()
    {
        anim.SetBool(hashMove, false);
        anim.SetBool(hashAttack, false);
        anim.SetBool(hashIdle, true);
        attackEffect.SetActive(false);
        swordCollider.enabled = false;
    }

    private void StartAttack()
    {
        PlayEffect(playerSound[1]);
        attackEffect.SetActive(true);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < maxJumpCnt)
        {
            PlayEffect(playerSound[0]);
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

    private void PlayEffect(AudioClip effectSound)
    {
        playerAudioSource.PlayOneShot(effectSound);
    }

    private void DivideSlime()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.localScale = new Vector3(6, 6, 6);
            smallSlime.SetActive(true);
            maxJumpCnt = 3;
            jumpForce = 17f;
            moveSpeed = 13f;
            isDivide = true;
        }
    }

    private void ResetSlime()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.localScale = new Vector3(8, 8, 8);
            smallSlime.SetActive(false);
            maxJumpCnt = 2;
            jumpForce = 20f;
            moveSpeed = 10f;
            isDivide = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 0;
        }
    }

    private void testDamaged()
    {
        
    }

    void OffDamaged()
    { 
        GetComponent<SpriteRenderer>().color=new Color(1, 1, 1, 1);
    }
}