using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 코인 전체 관리 및 코인 다 먹었을 때 로직
//코드 리팩토링, 테스트코드 추가
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-11
public class CoinManager : MonoBehaviour
{ 
    [SerializeField] private int collectedCoinsCount = 0;
    [SerializeField] private SpriteRenderer slimeSpr;
    [SerializeField] private SpriteRenderer playerSwordImg;
    [SerializeField] private Button failBtn;
    [SerializeField] private Button successBtn;
    public List<GameObject> coins = new List<GameObject>();
    public GameObject successPanel;
    private CoinTimerScript coinTimerScript;
    public static CoinManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        DeactivateAllCoins();
        successPanel.SetActive(false);
        failBtn.onClick.AddListener(OnClickFailBtn);
        successBtn.onClick.AddListener(OnClicKSuccessBtn);
        InitialCoinMap();
    }

    private void InitialCoinMap()
    {
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        AllSceneCanvas.instance.monsterCnt.SetActive(false);
        AllSceneCanvas.instance.SetStageText("BonusStage!");
        slimeSpr.color = PlayerManager.instance.playerColor;
        playerSwordImg.sprite = PlayerManager.instance.playerSwordSpr;
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
            collectedCoinsCount++;
            CheckAllCoinsCollected();
        }
    }

    public bool AreAllCoinsCollected()
    {
        return collectedCoinsCount == 6;
    }


    public void CheckAllCoinsCollected()
    {
        if (AreAllCoinsCollected())
        {
            if (coinTimerScript != null)
            {
                coinTimerScript.StopCountdown();
            }
        }
    }

    private void OnClickFailBtn()
    {
        AllSceneCanvas.instance.isOpenMenu = false;
        SceneSystem.instance.GoNextStage(SceneSystem.NextStageType.Normal);
        // SceneSystem.instance.TestNextStage("DungeonStage5");
    }

    private void OnClicKSuccessBtn()
    {
        AllSceneCanvas.instance.isOpenMenu = false;
        SaveLoadManager.instance.GetMoney(1500);
        // PlayerManager.instance.playerMoney += 1500;
        AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
        SceneSystem.instance.GoNextStage(SceneSystem.NextStageType.Normal);
        // SceneSystem.instance.TestNextStage("DungeonStage5");
    }
}