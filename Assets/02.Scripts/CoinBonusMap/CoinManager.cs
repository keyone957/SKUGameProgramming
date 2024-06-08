using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 코인 전체 관리 
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-06
public class CoinManager : MonoBehaviour
{
    public List<GameObject> coins = new List<GameObject>();
    public GameObject panel;  // 패널 오브젝트를 드래그 앤 드롭으로 할당하세요.

    void Start()
    {
        DeactivateAllCoins();
        if (panel != null)
        {
            panel.SetActive(false);  // 게임 시작 시 패널 비활성화
        }
    }

    public void ActivateCoin(GameObject coin)
    {
        coin.SetActive(true);
    }

    public void ActivateAllCoins()
    {
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
    }

    public void DeactivateAllCoins()
    {
        foreach (GameObject coin in coins)
        {
            coin.SetActive(false);
        }
    }

    public void CheckAllCoinsCollected()
    {
        foreach (GameObject coin in coins)
        {
            if (coin.activeInHierarchy)
            {
                return;  // 활성화된 코인이 아직 남아있음
            }
        }
        if (panel != null)
        {
            panel.SetActive(true);  // 모든 코인이 사라졌다면 패널 활성화
        }
    }
}