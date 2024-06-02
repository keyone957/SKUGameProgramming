using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 팝업창 구성 및 정보 
// 팝업창 실행중에는 게임 및 타이머 중단
// 최초 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-06-02

public class GameManager : MonoBehaviour
{
    public GameObject popupPanel;  // Panel 오브젝트
    public TextMeshProUGUI countdownText;  // 카운트다운 텍스트
    public Button startButton;  // Start 버튼
    public TextMeshProUGUI gameDescriptionText;  // 게임 설명 텍스트

    private bool gameStarted = false;  // 게임이 시작되었는지 여부를 나타내는 플래그

    public void Start()
    {
        // 항상 초기 UI 설정 실행
        countdownText.gameObject.SetActive(false);  // 카운트다운 텍스트 비활성화
        popupPanel.SetActive(true);  // 팝업창을 활성화
        startButton.onClick.AddListener(OnStartButtonClicked);  // Start 버튼 클릭 이벤트 등록
        Debug.Log("start");
    }

    public void OnStartButtonClicked()
    {
        if (!gameStarted)  // 게임이 시작되지 않았을 때만 실행
        {
            // JumpMapSystem으로 시작을 위임
            JumpMapSystem.Instance.StartGame();
        }
    }
}