using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{
    [SerializeField] private Slider bossHp;
    [SerializeField] public int hp;
    private int curHp;

    private void Start()
    {
        curHp = hp;
    }

    private void Update()
    {
        if (curHp <= 0)
        {
            BossStageSystem.instance.ClearBossStage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            curHp -= 1;
            bossHp.value = (float)curHp / hp;
            SoundManager._instance.PlaySound(Define._damagedBoss);
            Destroy(other.gameObject);
        }
    }
}