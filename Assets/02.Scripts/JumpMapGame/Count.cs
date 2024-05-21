using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Count : MonoBehaviour
{
    // UI 요소들을 연결할 SerializedField 변수들
    [SerializeField] private Image UiFill; // 카운트 다운 시간을 시각적으로 나타내는 이미지
    [SerializeField] private TextMeshProUGUI UiText; // 카운트 다운 시간을 표시하는 TextMeshProUGUI 텍스트

    // 카운트 다운의 총 시간을 저장하는 변수
    public int Duration;

    // 남은 시간을 저장하는 변수
    public int remainingDuration;

    // 스크립트가 시작될 때 호출되는 메서드
    private void Start()
    {
        // 카운트 다운을 시작하는 메서드 호출
        Being(Duration);
    }

    // 카운트 다운을 시작하는 메서드
    private void Being(int Second)
    {
        // 남은 시간을 설정
        remainingDuration = Second;

        StartCoroutine(UpdateTimer());
    }

    // 카운트 다운을 업데이트
    private IEnumerator UpdateTimer()
    {
        // 남은 시간이 0 이상인 동안 반복
        while (remainingDuration >= 0)
        {
            // 텍스트에 남은 시간을 분:초 형식으로 표시
            UiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            // 이미지의 fillAmount를 시간의 경과에 따라 조절하여 시각적으로 표시
            UiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            // 남은 시간을 1초 감소
            remainingDuration--;
            // 1초 대기
            yield return new WaitForSeconds(1.0f);
        }
        // 카운트 다운 종료 시 호출되는 메서드
        OnEnd();
        InitializeGame();
    }

    // 카운트 다운이 종료될 때 호출되는 메서드
    private void OnEnd()
    {
        print("Time Over");
    }

    private void InitializeGame()
    {
        // 시간초가 경과하면 해당 게임씬의 초기화면으로 회귀

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}