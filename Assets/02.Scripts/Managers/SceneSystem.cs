using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//전체 게임 관리하는 씬 시스템 구성
//맵 타입에 따라서 다음이동할 씬을 랜덤으로 이동하는 함수 추가
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-09
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
    public int maxStageCnt; //총 스테이지 개수
    [SerializeField] public FadeOverlay _fadeOverlay;

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
            int randomBonusStage = Random.RandomRange(1, 3);
            _fadeOverlay.DoFadeOut(1.0f, "BonusStage" + randomBonusStage);
        }
        else if (stageType == NextStageType.Normal)
        {
            int randomNoramlStage = Random.RandomRange(1, 6);
            _fadeOverlay.DoFadeOut(1.0f, "NormalStage" + randomNoramlStage);
        }

        currentStage++;
        isClearStage = false;
    }

    public void EndGame()
    {
    }
}