using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// 점프맵을 담당하는 컴포넌트 구성
// 점프맵 인스턴스
// 최초 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-06-02

public class JumpMapSystem : MonoBehaviour
{
    public GameObject popupPanel;      // 팝업창 오브젝트
    public TextMeshProUGUI countdownText;         // 카운트다운 텍스트
    public Button startButton;         // Start 버튼
    public Image timerImage;           // Timer 이미지

    public static JumpMapSystem Instance;  // 싱글톤 인스턴스

    private void Awake()
    {
        // JumpMapSystem 인스턴스를 설정
        Instance = this;
    }

    private void Start()
    {
        InitializeScene();
        ActivatePopupPanel();
    }

    // 씬의 초기화
    private void InitializeScene()
    {
        // 여기에 씬 초기화 로직 추가
    }

    // 팝업 패널 활성화
    private void ActivatePopupPanel()
    {
        popupPanel.SetActive(true);
        Time.timeScale = 0f;  // 팝업창이 열릴 때 시간을 멈춤
        startButton.onClick.AddListener(StartButtonClicked);
    }

    // Start 버튼 클릭 시 호출되는 메서드
    private void StartButtonClicked()
    {
        popupPanel.SetActive(false);  // 팝업창 비활성화
        Time.timeScale = 1f;           // 시간 다시 시작
        countdownText.gameObject.SetActive(true);  // 카운트다운 텍스트 활성화
        StartCoroutine(StartCountdown());
    }

    // 카운트다운을 시작하는 코루틴
    private IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();  // 카운트다운 숫자 표시
            yield return new WaitForSeconds(1f);  // 1초 대기
        }

        countdownText.text = "Start!!";  // 카운트다운 완료 메시지
        yield return new WaitForSeconds(1f);  // 1초 대기

        countdownText.gameObject.SetActive(false);  // 카운트다운 텍스트 비활성화

        // 타이머 이미지 활성화
        timerImage.gameObject.SetActive(true);
    }

    // 게임 시작 메서드
    public void StartGame()
    {
        // 게임 시작 관련 로직 추가
    }
}
