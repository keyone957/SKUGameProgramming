using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߶��� ������ϴ� ������Ʈ
// �������� �����
// ���� �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-05

public class DeathBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JumpMapSystem.Instance.ResetScene();
        }
    }
}