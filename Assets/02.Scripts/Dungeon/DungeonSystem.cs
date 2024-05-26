using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//던전 씬에 사용될 시스템 컴포넌트추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-24
public class DungeonSystem : MonoBehaviour
{
    [SerializeField] private Transform[] monsterSpawnPoint;
    [SerializeField] private GameObject[] monsterPrefab;
    
    void Start()
    {
        // StartCoroutine(SceneStartSequence());
        // InitializeLobbyScene();
    }

    private void InitializeLobbyScene()
    {
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
            Instantiate(monsterPrefab[randomMonsterPrefab], monsterSpawnPoint[i]);
        }
    }

}
