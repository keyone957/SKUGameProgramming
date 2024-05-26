using UnityEngine;
using TMPro;
//Ÿ�̸� ��� ����
public class Timer : MonoBehaviour
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
                TimerText.text= Mathf.Ceil(time).ToString();
            }
            //Ÿ�̸� ������ ��� 
            else
            {
                timerStarted = false;
            }
        }
    }
}
