using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라 움직임 제한 관련 컴포넌트
//backgroud 밖의 좌표로 플레이어 이동 제한위해 생성,
// 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-05-11

public class MoveBoundary : MonoBehaviour
{
    private BoxCollider2D boundaryCollider;

    void Start()
    {
        // 오브젝트에 있는 BoxCollider2D 컴포넌트를 가져옵니다.
        boundaryCollider = GetComponent<BoxCollider2D>();
    }

    // 플레이어의 위치를 제한합니다.
    public Vector2 RestrictPlayerMovement(Vector2 newPosition)
    {
        // 플레이어의 새로운 위치가 바운더리를 벗어나지 않도록 제한합니다.
        newPosition.x = Mathf.Clamp(newPosition.x, boundaryCollider.bounds.min.x, boundaryCollider.bounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, boundaryCollider.bounds.min.y, boundaryCollider.bounds.max.y);

        return newPosition;
    }
}
