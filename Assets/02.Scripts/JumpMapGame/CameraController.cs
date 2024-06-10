using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�޶� ������ ���� ������Ʈ
//backgroud ���� ��ǥ�� �÷��̾� �̵� ���� �ؾ���
// �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-05-11

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject moveBoundary;
    public float cameraOffsetX = 2.0f; // ī�޶�� �÷��̾� ���� �Ÿ�


    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 targetPosition = new Vector3(playerPos.x + cameraOffsetX, playerPos.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}