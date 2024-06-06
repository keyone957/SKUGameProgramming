using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

// �������� ����ϴ� ������Ʈ ����
// �������� �˾�â ���� �ڵ� �߰�
// ���� �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-06

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
    public float duration = 100f; // Ÿ�̸��� �� �ð�, �ν����� â���� ���� ����

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
        endPanel.SetActive(false); // ���� �г� �ʱ� ��Ȱ��ȭ
    }

    private void Update()
    {
        if (!isPlayerMovementRestricted)
        {
            timer += Time.deltaTime;
            timerImage.fillAmount = timer / duration;

            // Ÿ�̸Ӱ� ��� �����Ǿ��� �� ó��
            if (timer >= duration)
            {
                timer = duration; // Ensure timer does not exceed duration
                TriggerFlag();
            }
        }
    }

    private void InitializeScene()
    {
        // �� �ʱ�ȭ ���� �߰�
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
        // ���� ���� ���� �߰�
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

        remainingTimeText.text = $"����� ����� : {timer.ToString("F2")}���Դϴ�. {message}";
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
        Debug.Log("���� ���������� �Ѿ");
    }

    public void ResetScene()
    {
        Debug.Log("ó������");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}