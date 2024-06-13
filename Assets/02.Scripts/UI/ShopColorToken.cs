using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//상점 슬라임 색 + 무기 토큰
//플레이어 구매여부 코드 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-04
public class ShopColorToken : MonoBehaviour
{
    [SerializeField] private Image tokenSlime;//바꿀 슬라임 컬러
    [SerializeField] private int price;//가격
    [SerializeField] private TMP_Text priceText;// 가격 문자열
    [SerializeField] private Button buyBtn;//구매버튼
    [SerializeField] private TMP_Text buyBtnText;
    [SerializeField] private GameObject shopPopup; //상점 팝업
    [SerializeField] private Button popupBtn;//팝업 버튼
    [SerializeField] private TMP_Text popupText;
    private bool isBuy;//샀는지?
    [SerializeField] private string mode;//샀으면 버튼 모드
    [SerializeField] private string tokenMode;
    
    private void Start()
    {
        buyBtn.onClick.AddListener(OnClickBuyBtn);
        popupBtn.onClick.AddListener(()=>shopPopup.SetActive(false));
        priceText.text = price.ToString();
        if (PlayerPrefs.HasKey(gameObject.name.ToString()))
        {
            mode = PlayerPrefs.GetString(gameObject.name.ToString());
        }
        else
        {
            mode = "buy";
        }

        BtnMode(mode);
    }

    private void OnClickBuyBtn()
    {
        if (mode == "buy")
        {
            BuyPopup();
        }
        else if (mode == "equip"&&tokenMode=="color")
        {
            SoundManager._instance.PlaySound(Define._equip);
            PlayerManager.instance.SetPlayerColor(tokenSlime.color);
        }
        else if (mode == "equip" && tokenMode == "sword")
        {
            SoundManager._instance.PlaySound(Define._equip);
            PlayerManager.instance.SetPlayerSword(tokenSlime.sprite);
        }
    }
    private void BuyPopup()
    {
        shopPopup.SetActive(true);
        if (PlayerManager.instance.playerMoney >= price)
        {
            SoundManager._instance.PlaySound(Define._buySuccess);
            popupText.text = "구매 완료!";
            // PlayerManager.instance.playerMoney -=price;
            SaveLoadManager.instance.UseMoney(price);
            AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
            mode = "equip";
            PlayerPrefs.SetString(gameObject.name.ToString(),mode);
            BtnMode(mode);
        }
        else if (PlayerManager.instance.playerMoney < price)
        {
            SoundManager._instance.PlaySound(Define._buyDenied);
            popupText.text = "코인이 부족합니다.\n\n코인을 모아주세요!";
        }
    }
    private void BtnMode(string btnMode)
    {
        if (btnMode == "buy")
        {
            buyBtnText.text = "구매";
        }
        else if(btnMode=="equip")
        {
            buyBtnText.text = "장착";
        }
    }
}
