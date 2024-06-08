using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

// �������� ����ϴ� ������Ʈ ����
// �������� �˾�â ���� �ڵ� �߰�, ���� �ð��� ���� ���� �ο�, �������� �ٸ� ��������
// ���� �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-08

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

    // �̱��� �ν��Ͻ�
    private static JumpMapSystem instance;
    public static JumpMapSystem Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // �ν��Ͻ��� �������� ������ ���� �ν��Ͻ��� ����
        if (instance == null)
        {
            instance = this;
        }
        // �ν��Ͻ��� �̹� �����ϰ� ���� �ν��Ͻ��� �ƴ϶�� �� �ν��Ͻ��� �ı�
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // ���� ������ �̵��ص� �ı����� �ʵ��� ����
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
        remainingTimeText.text = $" Rank : {message} \n\n ����� ����� : {timer.ToString("F2")}���Դϴ�. ";
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
        Debug.Log("���� ���������� �Ѿ");
    }
}
