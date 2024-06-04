using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// �������� ����ϴ� ������Ʈ ����
// ������ �ν��Ͻ�
// ���� �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-02

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

        // �÷��̾��� ������ ���� ����
        RestrictPlayerMovement(false);

        // ���� ���� �޼��� ȣ��
        StartGame();
    }

    // ���� ���� �޼���
    public void StartGame()
    {
        // ���� ���� ���� �߰�
    }

    // �÷��̾��� �������� �����ϴ� �޼���
    public void RestrictPlayerMovement(bool restrict)
    {
        isPlayerMovementRestricted = restrict;
        if (playerInputController != null)
        {
            playerInputController.enabled = !restrict;
        }
    }

    // Update �޼��忡�� �÷��̾��� �������� �����ϴ� ���� �߰�
    private void Update()
    {

    }
}