using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�޶� ������ ���� ���� ������Ʈ
//backgroud ���� ��ǥ�� �÷��̾� �̵� �������� ����,
// �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-05-11

public class MoveBoundary : MonoBehaviour
{
    private BoxCollider2D boundaryCollider;

    void Start()
    {
        // ������Ʈ�� �ִ� BoxCollider2D ������Ʈ�� �����ɴϴ�.
        boundaryCollider = GetComponent<BoxCollider2D>();
    }

    // �÷��̾��� ��ġ�� �����մϴ�.
    public Vector2 RestrictPlayerMovement(Vector2 newPosition)
    {
        // �÷��̾��� ���ο� ��ġ�� �ٿ������ ����� �ʵ��� �����մϴ�.
        newPosition.x = Mathf.Clamp(newPosition.x, boundaryCollider.bounds.min.x, boundaryCollider.bounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, boundaryCollider.bounds.min.y, boundaryCollider.bounds.max.y);

        return newPosition;
    }
}
