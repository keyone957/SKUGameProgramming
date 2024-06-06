using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 30초 타이머 기능 구현
// 코드 리팩토링 and 얻는 코인 수 변경
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-06
public class ShootingTimer : MonoBehaviour
{
    public GameObject Finish;
    public TMP_Text monsterText;
    public TMP_Text TimerText;
    float time = 30f;
    private bool timerStarted;
    public TMP_Text Coin;
    int num;

    private void Awake()
    {
        timerStarted = false;
    }

    void Start()
    {
        StartTimer();
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

                int destroyedChildCount = ChildGenerator.Instance.GetMonsterAttackCount();
                Debug.Log("삭제된 몬스터 수: " + destroyedChildCount);
                monsterText.text = destroyedChildCount.ToString();
                num = destroyedChildCount * 10;
                Coin.text = num.ToString();

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