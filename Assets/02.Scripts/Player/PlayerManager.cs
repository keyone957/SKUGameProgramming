using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//모든씬에서 플레이어 정보를 가져와야하므로 DontDestroyOnLoad로 생성 하여 플레이어 정보 가져옴
//상점 관련(슬라임 색, 무기 스프라이트) 프로퍼티 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-02
public class PlayerManager : MonoBehaviour
{
   
    public static PlayerManager instance { get; private set; }
    private Transform spawnPoint;
    public int playerHp;
    public int playerMoney;
    public int playerPower;
    public Color playerColor;
    public Image playerSwordSpr;
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
    public void InitializePlayer()
    {
        transform.position = spawnPoint.transform.position;
    }
}
