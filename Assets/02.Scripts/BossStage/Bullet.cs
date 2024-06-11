using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̻��� ������Ʈ
//�÷��̾ �ȸԾ����� 3�ʵڿ� �����
// ���� �ۼ���: ȫ����
// ������: 
// ���� ������: 2024-06-11
public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform target; // �̵��� ��ǥ ��ġ
    [SerializeField] public float speed; // �̵� �ӵ�
    private bool isShoot;
    private float destroyDelay = 3.0f;
    private bool hasCollided = false;

    private void Start()
    {
        isShoot = false;
        target = FindObjectOfType<BossMonster>().transform;
        StartCoroutine(DestroyAfterDelay(destroyDelay));
    }

    void Update()
    {
        if (isShoot)
        {
            ShotBullet();
        }
    }

    private void ShotBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Vector2 direction = (target.position - transform.position).normalized;
        transform.right = direction;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!hasCollided)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager._instance.PlaySound(Define._shootBullet);
            isShoot = true;
            hasCollided = true;
        }
    }
}