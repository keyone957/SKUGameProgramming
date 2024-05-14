using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 게임 캐릭터 마우스 클릭시 공격 구현
// 최초 작성자: 하경림
// 최종 수정일: 2024-05-11
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
        //childGenerator.FindObjectOfType<ChildGenerator>();
        childGenerator = ChildGenerator.Instance;
        hpUi = GetComponent<HpUi>();
    }

    private void Update()
    {
        Idle();
        Attack();
    }
    private void Idle()
    {
        anim.SetBool("IsMove", false);
        anim.SetBool("IsIdle", true);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                    Debug.Log("땡!");
                    hpUi.SetHp(-1);
                }
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("IsAttack", true);
            swordCollider.enabled = true;
            if (childGenerator != null)
            {
                if (childGenerator.GetFirstTag() == "Skeleton")
                {
                    Debug.Log("땡!");
                    hpUi.SetHp(-1);

                }
                else if (childGenerator.GetFirstTag() == "IceGolem")
                {
                    childGenerator.RemoveFirstChild();
                }
            }
        }
    }



    private void EndAttack()
    {
        anim.SetBool("IsMove", false);
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsIdle", true);
        attackEffect.SetActive(false);
        swordCollider.enabled = false;
    }

    private void StartAttack()
    {
        //PlayEffect(playerSound[0]);
        attackEffect.SetActive(true);
    }

    //소리에서 자꾸 오류 나서 주석 처리
   // public void PlayEffect(AudioClip effectSound)
   // {
   //     playerAudioSource.PlayOneShot(effectSound);
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
        }
    }
}

