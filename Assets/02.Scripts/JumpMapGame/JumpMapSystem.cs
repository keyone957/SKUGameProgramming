using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

// 점프맵을 담당하는 컴포넌트 구성
// 게임종료 팝업창 관련 코드 추가, 남은 시간에 따라 점수 부여, 점수별로 다른 색상적용
// 최초 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-06-08

public class JumpMapSystem : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject endPanel;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI resultText;
    public Button startButton;
    public Button nextStageButton;
    public Image timerImage;
    public GameObject bombPrefab;

    private bool isPlayerMovementRestricted = true;
    private PlayerInputController playerInputController;
    private float timer = 0f;
    public float duration;
    public static Vector3 InitialPlayerPosition { get; private set; }

    // 싱글톤 인스턴스
    private static JumpMapSystem instance;
    public static JumpMapSystem Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // 인스턴스가 존재하지 않으면 현재 인스턴스로 설정
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 이미 존재하고 현재 인스턴스가 아니라면 이 인스턴스를 파괴
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // 다음 씬으로 이동해도 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);

        InitialPlayerPosition = transform.position;
    }

    private void Start()
    {
        ActivatePopupPanel();
        playerInputController = FindObjectOfType<PlayerInputController>();
        RestrictPlayerMovement(true);
        endPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isPlayerMovementRestricted)
        {
            timer += Time.deltaTime;
            timerImage.fillAmount = timer / duration;

            if (timer >= duration)
            {
                timer = duration;
                TriggerFlag();
            }
        }

        if (timer >= duration)
        {
            ActivateEndPanel();
        }
    }

    public void StartGame()
    {

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
            remainingTimeText.color = Color.red;
            resultText.color = Color.red;
            remainingTimeText.text = "Fail";
        }
        else if (remainingTime <= 10f)
        {
            message = "Not bad";
            remainingTimeText.color = Color.yellow;
            resultText.color = Color.blue;
        }
        else if (remainingTime <= 20f)
        {
            message = "Good";
            remainingTimeText.color = Color.green;
            resultText.color = Color.blue;
        }
        else
        {
            message = "Excellent";
            remainingTimeText.color = Color.blue;
            resultText.color = Color.blue;
        }

        remainingTimeText.text = message;
        resultText.text = (timer >= duration) ? "GameOver" : "Clear!!!";
        remainingTimeText.text = $" Rank : {message} \n\n 당신의 기록은 : {timer.ToString("F2")}초입니다. ";
        ActivateEndPanel();
    }

    public void ActivateEndPanel()
    {
        endPanel.SetActive(true);
        resultText.text = (timer >= duration) ? "GameOver" : "Clear!!!";
        nextStageButton.onClick.AddListener(NextStage);
    }

    private void NextStage()
    {
        Debug.Log("다음 스테이지로 넘어감");
    }
}
