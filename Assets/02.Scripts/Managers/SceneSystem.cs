using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//전체 게임 관리하는 씬 시스템 구성(아직 미완.)
//로비씬 들어가면 DontDestroyOnLoad로 삭제 안되게
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-05-06
public class SceneSystem : MonoBehaviour
{
    public static SceneSystem instance { get; private set; }
    public int currentStage;
    public bool isClearStage;
    public int maxStageCnt;
    void Start()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        currentStage = 0;
    }

    public void ClearCurrentStage()
    {
        currentStage++;
    }
    
    public void EndGame()
    {
        
    }
    
}
