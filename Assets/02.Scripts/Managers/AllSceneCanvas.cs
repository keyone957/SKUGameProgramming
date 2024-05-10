using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//모든 씬에서 사용할 UI관리하는 컴포넌트
//체력,돈,인게임 메뉴
//최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-10
public class AllSceneCanvas : MonoBehaviour
{
    public static AllSceneCanvas instance { get; private set; }
    [SerializeField] private GameObject playerHpUI;
    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private Button inGameMenuBtn;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private Button exitMenuBtn;
    public bool isOpenMenu;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        isOpenMenu = false;
        inGameMenuBtn.onClick.AddListener(OnClickInGameMenu);
        exitMenuBtn.onClick.AddListener(OnClickInGameMenu);
    }
    
    public void PlayerHPChange(int playerHP)
    {
        int playerCurHP = PlayerManager.instance.playerHp;
        
        for (int j = 1; j < 6; j++)
        {
            playerHpUI.transform.GetChild(j).gameObject.SetActive(false);
        }

        for (int i = 1; i <= playerCurHP; i++)
        {
            playerHpUI.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void SetMoney(int money)
    {
        playerMoneyText.text = money.ToString();
    }

    public void OnClickInGameMenu()
    {
        if (isOpenMenu)
        {
            inGameMenu.SetActive(false);
            isOpenMenu = false; 
        }
        else
        {
            inGameMenu.SetActive(true);
            isOpenMenu = true;
        }
    }
    
    private void OnKeyboardInGameMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpenMenu)
            {
                inGameMenu.SetActive(true);
                isOpenMenu = true;
            }
            else
            {
                inGameMenu.SetActive(false);
                isOpenMenu = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerManager.instance.playerHp--;
            PlayerHPChange(PlayerManager.instance.playerHp);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerManager.instance.playerMoney += 10;
            SetMoney(PlayerManager.instance.playerMoney);
        }

        OnKeyboardInGameMenu();
    }
}