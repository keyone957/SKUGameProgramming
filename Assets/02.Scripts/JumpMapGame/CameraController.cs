using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라 움직임 관련 컴포넌트
//backgroud 밖의 좌표로 플레이어 이동 제한 해야함
// 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-05-11

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject moveBoundary;
    public float cameraOffsetX = 2.0f; // 카메라와 플레이어 간의 거리


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