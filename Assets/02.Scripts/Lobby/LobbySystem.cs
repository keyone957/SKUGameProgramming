using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//로비씬 들어갈때 해야할 동작들 
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-05-04
public class LobbySystem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SceneStartSequence());
    }
    
    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._lobbyBgm);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._lobbyBgm);
    }

}
