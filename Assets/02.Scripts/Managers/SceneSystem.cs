using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//전체 게임 관리하는 씬 시스템 구성
//테스트코드 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-11
public class SceneSystem : MonoBehaviour
{
    public enum NextStageType
    {
        Bonus,
        Normal,
        Boss,
        BeforeBoss
    }

    public static SceneSystem instance { get; private set; }
    public int currentStage;
    public bool isClearStage;//현재 스테이지 클리어 했는지 bool값
    [SerializeField] public int maxStageCnt; //총 스테이지 개수
    [SerializeField] public int bonusStageOrder;//보너스 스테이지 몇번째에 실행할지
    [SerializeField] public int bossStageOrder;
    [SerializeField] public FadeOverlay _fadeOverlay;
    [SerializeField] public int monsterCnt;
    private List<int> usedNormalStages = new List<int>();
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
            int randomBonusStage = Random.Range(1, 5);
            _fadeOverlay.DoFadeOut(1.0f, "BonusStage" + randomBonusStage);
        }
        else if (stageType == NextStageType.Normal)
        {
            // int randomDungeonStage = Random.Range(1, 3);//나중에 맵 개수에 따라 랜덤한 스테이지 나오게
            // _fadeOverlay.DoFadeOut(1.0f, "DungeonStage" + randomDungeonStage);
            // currentStage++;
            
            int uniqueNumber = GetUniqueRandomNumber(0, 4, usedNormalStages);
            if (uniqueNumber != -1)
            {
                Debug.Log(uniqueNumber);
                usedNormalStages.Add(uniqueNumber);
                _fadeOverlay.DoFadeOut(1.0f, "DungeonStage" + uniqueNumber);
                currentStage++;
            }
            else
            {
                Debug.Log("No unique numbers left!");
            }
            
        }
        else if (stageType == NextStageType.Boss)
        {
            _fadeOverlay.DoFadeOut(1.0f,"BossStage");
            currentStage++;
        }
        else if (stageType == NextStageType.BeforeBoss)
        {
            _fadeOverlay.DoFadeOut(1.0f,"DungeonStage5");
            currentStage++;
        }

        isClearStage = false;
    }
    //테스트 씬 이동 함수
    public void TestNextStage(string TestSceneName)
    {
        _fadeOverlay.DoFadeOut(1.0f,TestSceneName);
        if (TestSceneName.Contains("BonusStage"))
        {
            Debug.Log("아멀라");
        }
        else
        {
            currentStage++;
        }
        
        isClearStage = false;
    }
    
    private int GetUniqueRandomNumber(int min, int max, List<int> usedNumbers)
    {
        List<int> possibleNumbers = new List<int>();
        for (int i = min; i <= max; i++)
        {
            if (!usedNumbers.Contains(i))
            {
                possibleNumbers.Add(i);
            }
        }

        if (possibleNumbers.Count == 0)
        {
            return -1;
        }

        int randomIndex = Random.Range(0, possibleNumbers.Count);
        return possibleNumbers[randomIndex];
    }

    public void RestList()
    {
        usedNormalStages.Clear();
        
    }
}