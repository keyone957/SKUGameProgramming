using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySystem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SceneStartSequence());
    }
    
    private IEnumerator SceneStartSequence()
    {
        //게임 시작하면 타이틀씬에서 브금 실행
        //TODO: 타이틀씬에서 어떤 정보를 받아와야 하는지 고민
        //예를 들어 유저 스텟, 돈 정보 등.
        SoundManager._instance.LoadBGM(Define._lobbyBgm);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._lobbyBgm);
    }

}
