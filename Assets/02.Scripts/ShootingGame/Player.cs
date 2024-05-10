using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� ĳ���� ���콺 Ŭ���� ���� ����
// ���� �ۼ���: �ϰ渲
// ���� ������: 2024-05-10
public class Player : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private List<AudioClip> playerSound = new List<AudioClip>();
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private GameObject attackEffect;
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

    //�Ҹ����� �ڲ� ���� ���� �ּ� ó��
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
