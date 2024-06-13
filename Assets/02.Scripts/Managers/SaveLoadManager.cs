using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�÷��̾� �� ���� ���
//���� �ۼ��� : ȫ����
// ������ : ȫ����
// ���� ������ : 2024-06-13
public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance { get; private set; }

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

    public void InitialPlayerMoney()
    {
        if (!PlayerPrefs.HasKey("playerMoney"))
        {
            PlayerPrefs.SetInt("playerMoney",0);
        }

        if (!PlayerPrefs.HasKey("playerFailMoney"))
        {
            PlayerPrefs.SetInt("playerFailMoney",0);
        }

        PlayerManager.instance.playerMoney = PlayerPrefs.GetInt("playerMoney");
        PlayerPrefs.SetInt("playerFailedMoney",PlayerPrefs.GetInt("playerMoney"));
    }

    public void GetMoney(int money)
    {
        PlayerManager.instance.playerMoney += money;
        PlayerPrefs.SetInt("playerMoney",PlayerManager.instance.playerMoney);
    }

    public void UseMoney(int money)
    {
        PlayerManager.instance.playerMoney -= money;
        PlayerPrefs.SetInt("playerMoney",PlayerManager.instance.playerMoney);
    }

    public void FailedMoney()
    {
        PlayerPrefs.SetInt("playerMoney",PlayerPrefs.GetInt("playerFailedMoney"));
        PlayerManager.instance.playerMoney = PlayerPrefs.GetInt("playerFailedMoney");
    }
}
