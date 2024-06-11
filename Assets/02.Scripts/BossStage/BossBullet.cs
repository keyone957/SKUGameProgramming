using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 보스 총알 collider
// 최초 작성자: 홍원기
// 수정자: 
// 최종 수정일: 2024-06-11
public class BossBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager._instance.PlaySound(Define._damagedSlime);
            PlayerManager.instance.playerHp -= 1;
            AllSceneCanvas.instance.PlayerHPChange(PlayerManager.instance.playerHp);
            if (PlayerManager.instance.playerHp <= 0)
            {
                PlayerManager.instance.isDiePlayer = true;
                SoundManager._instance.PlaySound(Define._playerDie);
                SceneSystem.instance._fadeOverlay.DoFadeOutDie(1.0f);
                BossStageSystem.instance.spawner.SetActive(false);
                BossStageSystem.instance.startBossStage = false;
                BossBullet[] bossBullets = FindObjectsOfType<BossBullet>();
                foreach (BossBullet bossBullet in bossBullets)
                {
                    Destroy(bossBullet.gameObject);
                }
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}