using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 팝업창 관리 및 초기 3초카운트 컴포넌트
// 시작 시 팝업창 생성 및 설명. 시작버튼 누르면 3초뒤 게임씬 로드
// 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-06-02

public class GameStartManager : MonoBehaviour
{
    public GameObject popupPanel;  // Panel 오브젝트
    public TextMeshProUGUI countdownText;  // 카운트다운 텍스트
    public Button startButton;  // Start 버튼
    public TextMeshProUGUI gameDescriptionText;  // 게임 설명 텍스트

    private bool gameStarted = false;  // 게임이 시작되었는지 여부를 나타내는 플래그

    void Start()
    {
        // 항상 초기 UI 설정 실행
        countdownText.gameObject.SetActive(false);  // 카운트다운 텍스트 비활성화
        popupPanel.SetActive(true);  // 팝업창을 활성화
        startButton.onClick.AddListener(OnStartButtonClicked);  // Start 버튼 클릭 이벤트 등록
        Debug.Log("start");
    }

    void OnStartButtonClicked()
    {
        if (!gameStarted)  // 게임이 시작되지 않았을 때만 실행
        {
            StartCoroutine(StartCountdown());  // 카운트다운 시작
        }
    }

    IEnumerator StartCountdown()
    {
        popupPanel.SetActive(false);  // 팝업창을 비활성화
        countdownText.gameObject.SetActive(true);  // 카운트다운 텍스트 활성화

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();  // 카운트다운 숫자 표시
            yield return new WaitForSeconds(1);  // 1초 대기
        }

        countdownText.text = "Start!!";  // 카운트다운 완료 메시지
        yield return new WaitForSeconds(1);  // 1초 대기

        countdownText.gameObject.SetActive(false);  // 카운트다운 텍스트 비활성화

        // 게임 시작 로직 추가
        StartGame();
    }

    void StartGame()
    {
        gameStarted = true;  // 게임이 시작되었음을 표시
        SceneManager.LoadScene("JumpMapBonus");
    }
}