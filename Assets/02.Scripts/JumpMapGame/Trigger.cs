using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ʈ���� ��ũ��Ʈ
// �̱��� ����
// ���� �ۼ��� : ������
// ������ : ȫ����
// ���� ������ : 2024-06-09

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        // �浹�� ������Ʈ�� �̸��� Player�� ��� ���� ����
        if (other.gameObject.name == "Player")
        {
            GameOver();
        }
    }

    // ���� ���� ����
    private void GameOver()
    {
        Debug.Log("Game Over!");

        // JumpMapSystem�� TriggerFlag �޼��� ȣ���Ͽ� ���� �г� Ȱ��ȭ
        JumpMapSystem.instance.TriggerFlag();
    }
}
