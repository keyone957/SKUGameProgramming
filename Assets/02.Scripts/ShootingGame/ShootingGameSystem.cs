using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 슈팅맵 들어왔을때 초기화 
//테스트코드 추가
// 최초 작성자: 홍원기
// 수정자: 
// 최종 수정일: 2024-06-11
public class ShootingGameSystem : MonoBehaviour
{
    [SerializeField] private Button finishBtn;
    [SerializeField] private SpriteRenderer slimeSpr;
    [SerializeField] private GameObject finishObj;
    void Start()
    {
        InitializeShootingMap();
        StartCoroutine(SceneStartSequence());
        finishBtn.onClick.AddListener(OnClickFinishBtn);
    }

    private void InitializeShootingMap()
    {
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        AllSceneCanvas.instance.isOpenMenu = true;
        AllSceneCanvas.instance.monsterCnt.SetActive(false);
        AllSceneCanvas.instance.playerHpUI.SetActive(false);
        AllSceneCanvas.instance.SetStageText("BonusStage!");
        slimeSpr.color = PlayerManager.instance.playerColor;
    }
    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._shootingBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._shootingBGM);
    }

    private void OnClickFinishBtn()
    {
        finishObj.SetActive(false);
        SceneSystem.instance.GoNextStage(SceneSystem.NextStageType.Normal);
        //테스트코드
        // SceneSystem.instance.TestNextStage("DungeonStage5");
        // PlayerManager.instance.playerMoney += ChildGenerator.Instance.GetMonsterAttackCount() * 10;
        SaveLoadManager.instance.GetMoney(ChildGenerator.Instance.GetMonsterAttackCount()*10);
        AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
    }
}
