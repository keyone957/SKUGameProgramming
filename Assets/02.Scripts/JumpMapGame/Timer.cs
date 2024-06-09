using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//제한시간 관련 컴포넌트
//타이머 소진 시 초기화 하는 부분 제거
//타임아웃 시 리스폰 컴포넌트 작성완료
//싱글톤 수정
// 작성자 : 장현우
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-09


public class Timer : MonoBehaviour
{
    // UI 요소들을 연결할 SerializedField 변수들
    [SerializeField] private Image UiFill; // 카운트 다운 시간을 시각적으로 나타내는 이미지
    [SerializeField] private TextMeshProUGUI UiText; // 카운트 다운 시간을 표시하는 TextMeshProUGUI 텍스트
    [SerializeField] private Color warningColor = Color.red; // 남은 시간이 10초 미만일 때의 색상

    // 카운트 다운의 총 시간을 저장하는 변수
    public int Duration;
    // 남은 시간을 저장하는 변수
    public int remainingDuration;

    private Color defaultColor; // 기본 색상을 저장하는 변수
    private Color defaultTextColor; // 기본 텍스트 색상을 저장하는 변수
    private bool isBlinking = false; // 깜빡임 효과 여부를 저장하는 변수

    // 스크립트가 시작될 때 호출되는 메서드
    private void Start()
    {
        defaultColor = UiFill.color;
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

            // 남은 시간이 10초 미만이고 깜빡임이 시작되지 않았다면 깜빡임 코루틴 시작
            if (remainingDuration < 10 && !isBlinking)
            {
                StartCoroutine(BlinkWarning());
            }

            // 남은 시간을 1초 감소
            remainingDuration--;
            // 1초 대기
            yield return new WaitForSeconds(1.0f);
        }
        // 카운트 다운 종료 시 호출되는 메서드
        OnEnd();

    }

    // 카운트 다운이 종료될 때 호출되는 메서드
    private void OnEnd()
    {
        // 타이머가 소진되면 엔드 팝업 패널을 활성화
        JumpMapSystem.instance.ActivateEndPanel();
    }



    private IEnumerator BlinkWarning()
    {
        isBlinking = true;

        while (remainingDuration > 0 && remainingDuration < 10)
        {
            UiFill.color = warningColor;
            UiText.color = warningColor;
            yield return new WaitForSeconds(0.5f);
            UiFill.color = defaultColor;
            UiText.color = defaultTextColor;
            yield return new WaitForSeconds(0.5f);
        }

        UiFill.color = defaultColor;
        UiText.color = defaultTextColor;
        isBlinking = false;
    }
}