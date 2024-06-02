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

    public static JumpMapSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeScene();
        ActivatePopupPanel();
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
        isPlayerMovementRestricted = false;

        // ���� ���� �޼��� ȣ��
        StartGame();
    }

    // ���� ���� �޼���
    public void StartGame()
    {

    }

    // �÷��̾��� �������� �����ϴ� �޼���
    public void RestrictPlayerMovement(bool restrict)
    {
        isPlayerMovementRestricted = restrict;
    }

    // Update �޼��忡�� �÷��̾��� �������� �����ϴ� ���� �߰�
    private void Update()
    {
        if (isPlayerMovementRestricted)
        {
            // �÷��̾��� �������� �����ϴ� ���� �߰�
            PlayerInputController playerInputController = FindObjectOfType<PlayerInputController>();
            if (playerInputController != null)
            {
                playerInputController.enabled = false;
            }
        }
        else
        {
            // �÷��̾��� �������� ���ѵ��� ���� ���, �������� ����մϴ�.
            PlayerInputController playerInputController = FindObjectOfType<PlayerInputController>();
            if (playerInputController != null)
            {
                playerInputController.enabled = true;
            }
        }
    }
}