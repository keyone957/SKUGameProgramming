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
    public GameObject popupPanel;
    public TextMeshProUGUI countdownText;
    public Button startButton;
    public Image timerImage;

    private bool isPlayerMovementRestricted = true;
    private PlayerInputController playerInputController;

    public static JumpMapSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeScene();
        ActivatePopupPanel();
        // Find the PlayerInputController once at the start
        playerInputController = FindObjectOfType<PlayerInputController>();
        // Restrict player movement at the start
        RestrictPlayerMovement(true);
    }

    private void InitializeScene()
    {
        // 씬 초기화 로직 추가
    }

    private void ActivatePopupPanel()
    {
        popupPanel.SetActive(true);
        Time.timeScale = 0f;
        startButton.onClick.AddListener(StartButtonClicked);
    }

    private void StartButtonClicked()
    {
        popupPanel.SetActive(false);
        Time.timeScale = 1f;
        countdownText.gameObject.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Start!!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        timerImage.gameObject.SetActive(true);

        // 플레이어의 움직임 제한 해제
        RestrictPlayerMovement(false);

        // 게임 시작 메서드 호출
        StartGame();
    }

    // 게임 시작 메서드
    public void StartGame()
    {
        // 게임 시작 로직 추가
    }

    // 플레이어의 움직임을 제한하는 메서드
    public void RestrictPlayerMovement(bool restrict)
    {
        isPlayerMovementRestricted = restrict;
        if (playerInputController != null)
        {
            playerInputController.enabled = !restrict;
        }
    }

    // Update 메서드에서 플레이어의 움직임을 제한하는 로직 추가
    private void Update()
    {

    }
}