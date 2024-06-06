using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

// 점프맵을 담당하는 컴포넌트 구성
// 게임종료 팝업창 관련 코드 추가
// 최초 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-06-06

public class JumpMapSystem : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject endPanel;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI remainingTimeText;
    public Button startButton;
    public Button retryButton;
    public Button nextStageButton;
    public Image timerImage;
    public GameObject bombPrefab;

    private bool isPlayerMovementRestricted = true;
    private PlayerInputController playerInputController;
    private float timer = 0f;
    public float duration = 100f; // 타이머의 총 시간, 인스펙터 창에서 설정 가능

    public static JumpMapSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeScene();
        ActivatePopupPanel();
        playerInputController = FindObjectOfType<PlayerInputController>();
        RestrictPlayerMovement(true);
        endPanel.SetActive(false); // 엔드 패널 초기 비활성화
    }

    private void Update()
    {
        if (!isPlayerMovementRestricted)
        {
            timer += Time.deltaTime;
            timerImage.fillAmount = timer / duration;

            // 타이머가 모두 소진되었을 때 처리
            if (timer >= duration)
            {
                timer = duration; // Ensure timer does not exceed duration
                TriggerFlag();
            }
        }
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

        RestrictPlayerMovement(false);

        StartGame();
    }

    public void StartGame()
    {
        // 게임 시작 로직 추가
    }

    public void RestrictPlayerMovement(bool restrict)
    {
        isPlayerMovementRestricted = restrict;
        if (playerInputController != null)
        {
            playerInputController.enabled = !restrict;
        }
    }

    public void TriggerFlag()
    {
        RestrictPlayerMovement(true);
        timerImage.gameObject.SetActive(false);

        float remainingTime = duration - timer;
        string message;

        if (remainingTime <= 0)
        {
            message = "Fail...";
        }
        else if (remainingTime <= 10f)
        {
            message = "Not bad";
        }
        else if (remainingTime <= 20f)
        {
            message = "Good";
        }
        else
        {
            message = "Excellent";
        }

        remainingTimeText.text = $"당신의 기록은 : {timer.ToString("F2")}초입니다. {message}";
        endPanel.SetActive(true);

        retryButton.onClick.AddListener(RetryGame);
        nextStageButton.onClick.AddListener(NextStage);
    }

    private void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextStage()
    {
        SceneManager.LoadScene("JumpMapBonus2");
        Debug.Log("다음 스테이지로 넘어감");
    }

    public void ResetScene()
    {
        Debug.Log("처음으로");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}