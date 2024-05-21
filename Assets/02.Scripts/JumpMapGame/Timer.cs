using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//제한시간 관련 컴포넌트
//보다 완성도 있게 수정해야함
// 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-05-11

public class TimerText : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    float timeRemaining = 60.0f; // 초기 시간 설정

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // 시간이 0보다 큰 경우에만 타이머 업데이트
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 시간 감소
            UpdateTimerText(); // 타이머 UI 업데이트
        }
        else
        {
            // 시간이 다 되었을 때 실행할 동작
            Debug.Log("Time's up!");
            // TimeOut 씬으로 이동
            SceneManager.LoadScene("TimeOut");
        }
    }

    void UpdateTimerText()
    {
        // 타이머 텍스트 업데이트
        textMesh.text =  Mathf.RoundToInt(timeRemaining).ToString() + "seconds";

        // 텍스트 색상 설정
        textMesh.color = Color.white;

        // 텍스트 크기 설정
        //textMesh.fontSize = 20; // 원하는 크기로 설정
    }
}
