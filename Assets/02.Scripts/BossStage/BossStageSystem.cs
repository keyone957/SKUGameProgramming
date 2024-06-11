using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
// 보스맵 시스템
// 최초 작성자: 홍원기
// 수정자: 
// 최종 수정일: 2024-06-11
public class BossStageSystem : MonoBehaviour
{
    public static BossStageSystem instance { get; private set; }
    [SerializeField] private Vector2 spawnAreaSize;
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private float spawnInterval;
    [SerializeField] public PlayerInputController playerInput;
    [SerializeField] public GameObject spawner;
    [SerializeField] private GameObject clearChatPanel;
    public bool startBossStage;
    private bool isSpawning = false;

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
        InitialBossStage();
        StartCoroutine(SceneStartSequence());
    }

    private void InitialBossStage()
    {
        SceneSystem.instance.isClearStage = false;
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        AllSceneCanvas.instance.playerHpUI.SetActive(true);
        AllSceneCanvas.instance.monsterCnt.SetActive(false);
        PlayerManager.instance.AssignSpriteRenderers();
        PlayerManager.instance.AnotherScenePlayer();
        AllSceneCanvas.instance.SetStageText("BossStage!!!");
        playerInput.enabled = false;
    }

    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._bossStageBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._bossStageBGM);
    }

    private void Update()
    {
        if (startBossStage && !isSpawning)
        {
            spawner.SetActive(true);
            StartCoroutine(SpawnObjectsCoroutine());
        }
    }

    public void ClearBossStage()
    {
        spawner.SetActive(false);
        startBossStage = false;
        SceneSystem.instance.isClearStage = true;
        BossBullet[] bossBullets = FindObjectsOfType<BossBullet>();
        foreach (BossBullet bossBullet in bossBullets)
        {
            Destroy(bossBullet.gameObject);
        }
        clearChatPanel.SetActive(true);
    }

    IEnumerator SpawnObjectsCoroutine()
    {
        isSpawning = true;
        yield return new WaitForSeconds(5.0f);
        while (startBossStage)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    void SpawnObject()
    {
        Vector2 spawnPosition = GetRandomPositionInArea();
        Instantiate(bulletObj, spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomPositionInArea()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        return new Vector2(randomX, randomY) + (Vector2)transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}