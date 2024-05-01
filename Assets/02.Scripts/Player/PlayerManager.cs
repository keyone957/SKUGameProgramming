using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   
    public static PlayerManager instance { get; private set; }
    private Transform spawnPoint;
    public int playerHp;
    public int playerMoney;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void InitializePlayer()
    {
        transform.position = spawnPoint.transform.position;
    }
}
