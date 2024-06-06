using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ʈ���� ��ũ��Ʈ
// �����ϸ� ���� �� �ε�, �켱 �����
// ���� �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-05

public class Trigger2 : MonoBehaviour
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
        // ���� ���� ���� ������ ���⿡ �߰�
        // ��: ���� �޴��� ���ư���, Ư�� �� �ε��ϱ� ��
        SceneManager.LoadScene("");
    }
}