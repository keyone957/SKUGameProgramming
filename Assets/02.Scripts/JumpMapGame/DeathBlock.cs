using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߶��� �˾�â�� Ȱ��ȭ�ϴ� ������Ʈ
// �������� �˾�â Ȱ��ȭ
// ���� �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-07

public class DeathBlock : MonoBehaviour
{
    private Vector3 respawnPosition = new Vector3(-33f, 37f, 0f);  // �÷��̾ �������� ��ġ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾� ��ġ �ʱ�ȭ
            ResetPlayerPosition(collision.gameObject);
        }
    }

    private void ResetPlayerPosition(GameObject player)
    {
        // �÷��̾ ������ ��ġ�� �̵�
        player.transform.position = respawnPosition;
    }
}