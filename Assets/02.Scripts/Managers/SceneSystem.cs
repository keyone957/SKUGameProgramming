using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//전체 게임 관리하는 씬 시스템 구성
//이동할 씬이름 변경
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-04
public class SceneSystem : MonoBehaviour
{
    public enum NextStageType
    {
        Bonus,
        Normal,
        Boss
    }

    public static SceneSystem instance { get; private set; }
    public int currentStage;
    public bool isClearStage;//현재 스테이지 클리어 했는지 bool값
    [SerializeField] public int maxStageCnt; //총 스테이지 개수
    [SerializeField] public int bonusStageOrder;//보너스 스테이지 몇번째에 실행할지
    [SerializeField] public FadeOverlay _fadeOverlay;
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

    //다음으로 넘어갈 스테이지 타입에 따라서 랜덤으로 씬넘어가기
    //이거를 포탈이나 버튼 이벤트에 넣어서 실행 하게 하면 좋을듯
    public void GoNextStage(NextStageType stageType)
    {
        if (stageType == NextStageType.Bonus)
        {
            int randomBonusStage = Random.Range(1, 3);
            _fadeOverlay.DoFadeOut(1.0f, "BonusStage" + randomBonusStage);
        }
        else if (stageType == NextStageType.Normal)
        {
            int randomDungeonStage = Random.Range(1, 3);//나중에 맵 개수에 따라 랜덤한 스테이지 나오게
            _fadeOverlay.DoFadeOut(1.0f, "DungeonStage" + randomDungeonStage);
        }

        currentStage++;
        isClearStage = false;
    }
    //테스트 씬 이동 함수
    public void TestNextStage(string TestSceneName)
    {
        _fadeOverlay.DoFadeOut(1.0f,TestSceneName);
    }
    public void EndGame()
    {
    }
}