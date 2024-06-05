using UnityEngine;
using TMPro;

// 30초 타이머 기능 구현
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-03
public class ShootingTimer : MonoBehaviour
{
    public GameObject Finish;
    public TMP_Text monsterText;

    private ChildGenerator childGenerator;
    public TMP_Text TimerText;
    float time = 30f;
    bool timerStarted = false;
    public TMP_Text Coin;
    int num;

    void Start()
    {
        StartTimer();
        childGenerator = ChildGenerator.Instance;

    }

    void StartTimer()
    {
        timerStarted = true;
        time = 30f;
    }

    void Update()
    {
        if (timerStarted)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                TimerText.text = Mathf.Ceil(time).ToString();
            }
            else
            {
                time = 0;
                TimerText.text = "0";
                timerStarted = false;
                if (ChildGenerator.Instance != null)
                {
                    ChildGenerator.Instance.DestroyAllChildren();
                }
                if (childGenerator != null)
                {
                    int destroyedChildCount = childGenerator.GetMonsterAttackCount();
                    Debug.Log("삭제된 몬스터 수: " + destroyedChildCount);
                    monsterText.text = destroyedChildCount.ToString();
                    num = destroyedChildCount * 50;
                    Coin.text = num.ToString();
                }
                Finish.SetActive(true);
            }
        }
    }

    public void SetTimerToZero()
    {
        time = 0;
        TimerText.text = "0";
        timerStarted = false;
    }
}
