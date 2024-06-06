using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 코드 리팩토링 및 이펙트 사운드 추가
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-06
public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private HpUi hpUi;

    private void Start()
    {
        hpUi = GetComponent<HpUi>();
        Idle();
    }

    private void Update()
    {
        Idle();
        Attack();
    }

    private void Idle()
    {
        anim.SetBool("IsIdle", true);
    }

    private void Attack()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("IsAttack", true);
            if (ChildGenerator.Instance.GetFirstTag() == "Skeleton")
            {
                SoundManager._instance.PlaySound(Define._shootingSkeleton);
                ChildGenerator.Instance.RemoveFirstChild();
            }
            else if (ChildGenerator.Instance.GetFirstTag() == "IceGolem")
            {
                SoundManager._instance.PlaySound(Define._shootingDamaged);
                hpUi.SetHp(-1);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("IsAttack", true);
            if (ChildGenerator.Instance.GetFirstTag() == "Skeleton")
            {
                SoundManager._instance.PlaySound(Define._shootingDamaged);
                hpUi.SetHp(-1);
            }
            else if (ChildGenerator.Instance.GetFirstTag() == "IceGolem")
            {
                SoundManager._instance.PlaySound(Define._shootingGolem);
                ChildGenerator.Instance.RemoveFirstChild();
            }
        }
    }

    public void EndAttack()
    {
        anim.SetBool("IsAttack", false);
        Idle();
    }
}

