using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//상점 ui관련
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-06-02
public class ShopController : MonoBehaviour
{
    [SerializeField] private Button swordTab;
    [SerializeField] private Button slimeColorTab;
    [SerializeField] private GameObject sowrdPanel;
    [SerializeField] private GameObject slimeColorPanel;
    [SerializeField] private Button exitBtn;

    private void Start()
    {
        swordTab.onClick.AddListener(OnClickSwordTab);
        slimeColorTab.onClick.AddListener(OnClickColorTab);
        exitBtn.onClick.AddListener(()=>
        {
            SoundManager._instance.PlaySound(Define._clickMenuSound);
            gameObject.SetActive(false);
        });
    }

    private void OnClickSwordTab()
    {
        sowrdPanel.SetActive(true);
        slimeColorPanel.SetActive(false);
        swordTab.GetComponent<Image>().color = new Color32(255, 157, 157,255);
        slimeColorTab.GetComponent<Image>().color = new Color32(255, 255, 255,255);
    }

    private void OnClickColorTab()
    {
        sowrdPanel.SetActive(false);
        slimeColorPanel.SetActive(true);
        swordTab.GetComponent<Image>().color = new Color32(255, 255, 255,255);
        slimeColorTab.GetComponent<Image>().color =new Color32(255, 157, 157,255);
    }
}