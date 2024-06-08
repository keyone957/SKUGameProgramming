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
    public GameObject finishPanel;

    void Start()
    {
        DeactivateAllCoins();
        if (finishPanel != null)
        {
            finishPanel.SetActive(false); 
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
                return;
            }
        }
        if (finishPanel != null)
        {
            finishPanel.SetActive(true); 
        }
    }
}