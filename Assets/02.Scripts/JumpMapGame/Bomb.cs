using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // ���߷� ���� ����
    public float explosionForce = 1000.0f;

    // Ʈ���� ���� �̺�Ʈ
    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���ŵ� ������Ʈ�� �÷��̾����� Ȯ��
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger!");

            // �÷��̾ ���� �������� �о���ϴ�.
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 direction = other.transform.position - transform.position;
                playerRigidbody.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
            }

            // ��ź �ı� (�ɼ�)
            Destroy(gameObject);
        }
    }
}

