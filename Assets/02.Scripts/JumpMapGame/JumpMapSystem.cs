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
    public GameObject popupPanel;      // �˾�â ������Ʈ
    public TextMeshProUGUI countdownText;         // ī��Ʈ�ٿ� �ؽ�Ʈ
    public Button startButton;         // Start ��ư
    public Image timerImage;           // Timer �̹���

    public static JumpMapSystem Instance;  // �̱��� �ν��Ͻ�

    private void Awake()
    {
        // JumpMapSystem �ν��Ͻ��� ����
        Instance = this;
    }

    private void Start()
    {
        InitializeScene();
        ActivatePopupPanel();
    }

    // ���� �ʱ�ȭ
    private void InitializeScene()
    {
        // ���⿡ �� �ʱ�ȭ ���� �߰�
    }

    // �˾� �г� Ȱ��ȭ
    private void ActivatePopupPanel()
    {
        popupPanel.SetActive(true);
        Time.timeScale = 0f;  // �˾�â�� ���� �� �ð��� ����
        startButton.onClick.AddListener(StartButtonClicked);
    }

    // Start ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    private void StartButtonClicked()
    {
        popupPanel.SetActive(false);  // �˾�â ��Ȱ��ȭ
        Time.timeScale = 1f;           // �ð� �ٽ� ����
        countdownText.gameObject.SetActive(true);  // ī��Ʈ�ٿ� �ؽ�Ʈ Ȱ��ȭ
        StartCoroutine(StartCountdown());
    }

    // ī��Ʈ�ٿ��� �����ϴ� �ڷ�ƾ
    private IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();  // ī��Ʈ�ٿ� ���� ǥ��
            yield return new WaitForSeconds(1f);  // 1�� ���
        }

        countdownText.text = "Start!!";  // ī��Ʈ�ٿ� �Ϸ� �޽���
        yield return new WaitForSeconds(1f);  // 1�� ���

        countdownText.gameObject.SetActive(false);  // ī��Ʈ�ٿ� �ؽ�Ʈ ��Ȱ��ȭ

        // Ÿ�̸� �̹��� Ȱ��ȭ
        timerImage.gameObject.SetActive(true);
    }

    // ���� ���� �޼���
    public void StartGame()
    {
        // ���� ���� ���� ���� �߰�
    }
}
