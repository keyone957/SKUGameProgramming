using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� ĳ���� ���콺 Ŭ���� ���� ����
// ���� �ۼ���: �ϰ渲
// ���� ������: 2024-06-04
public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private List<AudioClip> playerSound = new List<AudioClip>();
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private GameObject attackEffect;
    private ChildGenerator childGenerator;
    [SerializeField] private HpUi hpUi;

    private void Start()
    {
        childGenerator = ChildGenerator.Instance;
        hpUi = GetComponent<HpUi>();
        Idle();
    }

    private void Update()
    {
        Attack();
    }

    private void Idle()
    {
        anim.SetBool("IsIdle", true);
    }

    private void Attack()
    {
        if (Time.timeScale == 0)
        {
            return; // 게임이 멈춰 있을 때는 입력을 무시합니다.
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스 왼쪽 버튼 입력");
            anim.SetBool("IsAttack", true);
            swordCollider.enabled = true;
            if (childGenerator != null)
            {
                if (childGenerator.GetFirstTag() == "Skeleton")
                {
                    childGenerator.RemoveFirstChild();
                }
                else if (childGenerator.GetFirstTag() == "IceGolem")
                {
                    hpUi.SetHp(-1);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("마우스 오른쪽 버튼 입력");
            anim.SetBool("IsAttack", true);
            swordCollider.enabled = true;
            if (childGenerator != null)
            {
                if (childGenerator.GetFirstTag() == "Skeleton")
                {
                    hpUi.SetHp(-1);
                }
                else if (childGenerator.GetFirstTag() == "IceGolem")
                {
                    childGenerator.RemoveFirstChild();
                }
            }
        }
    }

    public void EndAttack()
    {
        Debug.Log("공격 종료");
        anim.SetBool("IsAttack", false);
        swordCollider.enabled = false;
        attackEffect.SetActive(false);
        Idle();
    }

    private void StartAttack()
    {
        //PlayEffect(playerSound[0]);
        attackEffect.SetActive(true);
    }

    // 효과음을 재생하는 함수
    // public void PlayEffect(AudioClip effectSound)
    // {
    //     playerAudioSource.PlayOneShot(effectSound);
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
        }
    }
}

