using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//던전 씬에 사용될 시스템 컴포넌트추가
//몬스터 갯수에 따라 ui띄우기
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-28
public class DungeonSystem : MonoBehaviour
{
    public static DungeonSystem instance { get; private set; }
    [SerializeField] private Transform[] monsterSpawnPoint;
    [SerializeField] private GameObject[] monsterPrefab;
    [SerializeField] public int monsterCnt;
    
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
    void Start()
    {
        // StartCoroutine(SceneStartSequence());
        // InitializeDungeonScene();
        SpawnMonster();
        monsterCnt = monsterSpawnPoint.Length;
        AllSceneCanvas.instance.SetMonsterCnt(monsterCnt);
    }

    private void InitializeDungeonScene()
    {
        AllSceneCanvas.instance.SetMonsterCnt(monsterSpawnPoint.Length);
        SceneSystem.instance.isClearStage = false;
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        
    }

    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._dungeonBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._dungeonBGM);
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
