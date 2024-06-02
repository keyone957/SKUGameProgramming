using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// �˾�â ���� �� ���� 
// �˾�â �����߿��� ���� �� Ÿ�̸� �ߴ�
// ���� �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-06-02

public class GameManager : MonoBehaviour
{
    public GameObject popupPanel;  // Panel ������Ʈ
    public TextMeshProUGUI countdownText;  // ī��Ʈ�ٿ� �ؽ�Ʈ
    public Button startButton;  // Start ��ư
    public TextMeshProUGUI gameDescriptionText;  // ���� ���� �ؽ�Ʈ

    private bool gameStarted = false;  // ������ ���۵Ǿ����� ���θ� ��Ÿ���� �÷���

    public void Start()
    {
        // �׻� �ʱ� UI ���� ����
        countdownText.gameObject.SetActive(false);  // ī��Ʈ�ٿ� �ؽ�Ʈ ��Ȱ��ȭ
        popupPanel.SetActive(true);  // �˾�â�� Ȱ��ȭ
        startButton.onClick.AddListener(OnStartButtonClicked);  // Start ��ư Ŭ�� �̺�Ʈ ���
        Debug.Log("start");
    }

    public void OnStartButtonClicked()
    {
        if (!gameStarted)  // ������ ���۵��� �ʾ��� ���� ����
        {
            // JumpMapSystem���� ������ ����
            JumpMapSystem.Instance.StartGame();
        }
    }
}