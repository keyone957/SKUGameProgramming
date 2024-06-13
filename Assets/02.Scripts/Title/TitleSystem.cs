using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//타이틀 씬에 있는 것들 관리.
//타이틀씬 들어올때 저장된 브금, 효과음 볼륨 세팅
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-31
public class TitleSystem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SceneStartSequence());
        Time.timeScale = 1.0f;
    }
    
    private IEnumerator SceneStartSequence()
    {
        //게임 시작하면 타이틀씬에서 브금 실행
        //TODO: 타이틀씬에서 어떤 정보를 받아와야 하는지 고민
        //예를 들어 유저 스텟, 돈 정보 등.
        SoundManager._instance.LoadBGM(Define._titleBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._titleBGM);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("모두 삭제");
            PlayerPrefs.DeleteAll();
        }
    }
}
