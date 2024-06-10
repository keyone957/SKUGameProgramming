using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 코인 전체 관리 및 코인 다 먹었을 때 로직
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-10
public class CoinManager : MonoBehaviour
{
    public List<GameObject> coins = new List<GameObject>();
    public GameObject successPanel;
    private int collectedCoinsCount = 0;

    private CoinTimerScript coinTimerScript;
    private static CoinManager _instance;

    public static CoinManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CoinManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("CoinManager");
                    _instance = go.AddComponent<CoinManager>();
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        DeactivateAllCoins();
        if (successPanel != null)
        {
            successPanel.SetActive(false);
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

    public void RemoveCoin(GameObject coin)
    {
        if (coins.Contains(coin))
        {
            coin.SetActive(false); 
            coins.Remove(coin); 
            collectedCoinsCount++;
            CheckAllCoinsCollected();
        }
    }

    public bool AreAllCoinsCollected()
    {
        return collectedCoinsCount == coins.Count;
    }

    public void ShowSuccessPanel()
    {
        if (successPanel != null)
        {
            successPanel.SetActive(true);
        }
    }

    public void CheckAllCoinsCollected()
    {
        if (AreAllCoinsCollected())
        {
            if (coinTimerScript == null)
            {
                coinTimerScript = FindObjectOfType<CoinTimerScript>();
            }

            if (coinTimerScript != null)
            {
                coinTimerScript.StopCountdown();
            }

            ShowSuccessPanel();
        }
    }
}
