using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// �˾�â ���� �� �ʱ� 3��ī��Ʈ ������Ʈ
// ���� �� �˾�â ���� �� ����. ���۹�ư ������ 3�ʵ� ���Ӿ� �ε�
// �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-02

public class GameStartManager : MonoBehaviour
{
    public GameObject popupPanel;  // Panel ������Ʈ
    public TextMeshProUGUI countdownText;  // ī��Ʈ�ٿ� �ؽ�Ʈ
    public Button startButton;  // Start ��ư
    public TextMeshProUGUI gameDescriptionText;  // ���� ���� �ؽ�Ʈ

    private bool gameStarted = false;  // ������ ���۵Ǿ����� ���θ� ��Ÿ���� �÷���

    void Start()
    {
        // �׻� �ʱ� UI ���� ����
        countdownText.gameObject.SetActive(false);  // ī��Ʈ�ٿ� �ؽ�Ʈ ��Ȱ��ȭ
        popupPanel.SetActive(true);  // �˾�â�� Ȱ��ȭ
        startButton.onClick.AddListener(OnStartButtonClicked);  // Start ��ư Ŭ�� �̺�Ʈ ���
        Debug.Log("start");
    }

    void OnStartButtonClicked()
    {
        if (!gameStarted)  // ������ ���۵��� �ʾ��� ���� ����
        {
            StartCoroutine(StartCountdown());  // ī��Ʈ�ٿ� ����
        }
    }

    IEnumerator StartCountdown()
    {
        popupPanel.SetActive(false);  // �˾�â�� ��Ȱ��ȭ
        countdownText.gameObject.SetActive(true);  // ī��Ʈ�ٿ� �ؽ�Ʈ Ȱ��ȭ

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();  // ī��Ʈ�ٿ� ���� ǥ��
            yield return new WaitForSeconds(1);  // 1�� ���
        }

        countdownText.text = "Start!!";  // ī��Ʈ�ٿ� �Ϸ� �޽���
        yield return new WaitForSeconds(1);  // 1�� ���

        countdownText.gameObject.SetActive(false);  // ī��Ʈ�ٿ� �ؽ�Ʈ ��Ȱ��ȭ

        // ���� ���� ���� �߰�
        StartGame();
    }

    void StartGame()
    {
        gameStarted = true;  // ������ ���۵Ǿ����� ǥ��
        SceneManager.LoadScene("JumpMapBonus");
    }
}