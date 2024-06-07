using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//모든씬에서 플레이어 정보를 가져와야하므로 DontDestroyOnLoad로 생성 하여 플레이어 정보 가져옴
//플레이어 죽었을때 씬에 있는 몬스터 다 삭제해버리기
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-07
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    [SerializeField] private SpriteRenderer slimeImg;
    [SerializeField] private SpriteRenderer divideSlimeImg;
    [SerializeField] private SpriteRenderer playerSwordImg;
    private Transform spawnPoint;
    public int playerHp;
    public int playerMoney;
    public int playerPower;
    public Color playerColor;
    public Sprite playerSwordSpr;
    public bool isDiePlayer;
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
        AssignSpriteRenderers();
        isDiePlayer = false;
    }

    public void DestroyAllMonsters()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();

        foreach (Monster monster in monsters)
        {
            Destroy(monster.gameObject);
        }
    }

    public void InitializePlayer()
    {
        playerColor = slimeImg.color;
        playerSwordSpr = playerSwordImg.sprite;
    }

    public void AnotherScenePlayer()
    {
        slimeImg.color = playerColor;
        divideSlimeImg.color = playerColor;
        playerSwordImg.sprite = playerSwordSpr;
    }

    public void SetPlayerColor(Color slimeColor)
    {
        playerColor = slimeColor;
        slimeImg.color = playerColor;
        divideSlimeImg.color = playerColor;
    }

    public void SetPlayerSword(Sprite swordImg)
    {
        playerSwordSpr = swordImg;
        playerSwordImg.sprite = playerSwordSpr;
    }

    //매우 안좋은 방법이긴하지만 시간상....
    public void AssignSpriteRenderers()
    {
        if (slimeImg == null)
        {
            GameObject slimeObj = GameObject.Find("Player");
            if (slimeObj != null)
            {
                slimeImg = slimeObj.GetComponent<SpriteRenderer>();
            }
        }

        if (divideSlimeImg == null)
        {
            GameObject divideSlimeObj = FindInactiveObjectByName("DivideSlime");
            if (divideSlimeObj != null)
            {
                divideSlimeImg = divideSlimeObj.GetComponent<SpriteRenderer>();
            }
        }

        if (playerSwordImg == null)
        {
            GameObject playerSwordObj = GameObject.Find("Sword1");
            {
                playerSwordImg = playerSwordObj.GetComponent<SpriteRenderer>();
            }
        }
    }
    //비활성화 되어있는 오브젝트 찾기
    private GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform obj in objs)
        {
            if (obj.hideFlags == HideFlags.None && obj.name == name)
            {
                return obj.gameObject;
            }
        }
        return null;
    }
}
