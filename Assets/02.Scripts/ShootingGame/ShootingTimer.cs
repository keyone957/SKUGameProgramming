using UnityEngine;
using TMPro;

// 30초 타이머 기능 구현
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-03
public class ShootingTimer : MonoBehaviour
{
    public TMP_Text TimerText;
    float time = 30f;
    bool timerStarted = false;

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
