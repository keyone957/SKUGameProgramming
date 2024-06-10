using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// �����ʿ��� �浹 �� ƨ�ܳ���, ����ó�� ������ y�����δ� ���� �ʵ��� ����
// �浹�� ����Ʈ
// ���� �ۼ��� : ������
// ������ : ȫ����
// ���� ������ : 2024-06-10

public class Bomb : MonoBehaviour
{
    public float forceAmount = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 forceDirection = (other.transform.position - transform.position).normalized;
                forceDirection.y = 0;  // y�� ������ 0���� �����Ͽ� �����θ� ���� ����
                playerRigidbody.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
                SoundManager._instance.PlaySound(Define._bombSound);
            }
        }
    }
}

