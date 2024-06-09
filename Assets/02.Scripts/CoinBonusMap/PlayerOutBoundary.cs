using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 플레이어가 떨어졌을 경우 실패 패널
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-09
public class PlayerOutBoundary : MonoBehaviour
{
    public float minYPosition = -10f;
    public GameObject failPanel;

    private void Update()
    {
        if (transform.position.y < minYPosition)
        {
            TriggerFail();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            TriggerFail();
        }
    }

    private void TriggerFail()
    {
        Time.timeScale = 0f; 
        if (failPanel != null)
        {
            failPanel.SetActive(true);
        }
    }
}