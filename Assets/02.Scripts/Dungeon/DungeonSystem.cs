using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

//던전 씬에 사용될 시스템 컴포넌트추가
//몬스터 갯수에 따라 ui띄우기
//테스트코드 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-11
public class DungeonSystem : MonoBehaviour
{
    public static DungeonSystem instance { get; private set; }
    [SerializeField] private Transform[] monsterSpawnPoint;
    [SerializeField] private GameObject[] monsterPrefab;
    [SerializeField] public int monsterCnt;
    private bool soundPlayed;
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
        SpawnMonster();
        InitializeDungeonScene();
        PlayerManager.instance.AssignSpriteRenderers();
        PlayerManager.instance.AnotherScenePlayer();
        if (SceneManager.GetActiveScene().name == "DungeonStage5")
        {
            StartCoroutine(SceneeStartBeforeBossStage());
        }
        else
        {
            StartCoroutine(SceneStartSequence());
        }
    }
    private void Update()
    {
        if (monsterCnt <= 0 && !soundPlayed)
        {
            SceneSystem.instance.isClearStage = true;
            PlayClearStageSound();
        }
    }
    
    private void PlayClearStageSound()
    {
        SoundManager._instance.PlaySound(Define._clearDungeonStage);
        soundPlayed = true;
    }

    private void InitializeDungeonScene()
    {
        PlayerManager.instance.playerPower = 2;
        monsterCnt = monsterSpawnPoint.Length;
        soundPlayed = false;
        AllSceneCanvas.instance.monsterCnt.SetActive(true);
        AllSceneCanvas.instance.playerHpUI.SetActive(true);
        AllSceneCanvas.instance.SetMonsterCnt(monsterCnt);
        SceneSystem.instance.isClearStage = false;
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        AllSceneCanvas.instance.SetStageText("Stage "+SceneSystem.instance.currentStage);
    }

    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._dungeonBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._dungeonBGM);
    }
    private IEnumerator SceneeStartBeforeBossStage()
    {
        SoundManager._instance.LoadBGM(Define._beforeBossBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._beforeBossBGM);
    }

    private void SpawnMonster()
    {
        for (int i = 0; i < monsterSpawnPoint.Length; i++)
        {
            int randomMonsterPrefab = Random.Range(0, monsterPrefab.Length);
            Instantiate(monsterPrefab[randomMonsterPrefab], monsterSpawnPoint[i].position,monsterSpawnPoint[i].rotation);
        }
    }
    
}
