using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//���ѽð� ���� ������Ʈ
//���� �ϼ��� �ְ� �����ؾ���
// �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-05-11

public class TimerText : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    float timeRemaining = 60.0f; // �ʱ� �ð� ����

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // �ð��� 0���� ū ��쿡�� Ÿ�̸� ������Ʈ
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // �ð� ����
            UpdateTimerText(); // Ÿ�̸� UI ������Ʈ
        }
        else
        {
            // �ð��� �� �Ǿ��� �� ������ ����
            // ���⿡ ���ϴ� ������ �߰��Ͻø� �˴ϴ�.
            Debug.Log("Time's up!");
            // TimeOut ������ �̵�
            SceneManager.LoadScene("TimeOut");
        }
    }

    void UpdateTimerText()
    {
        // Ÿ�̸� �ؽ�Ʈ ������Ʈ
        textMesh.text = "Timer : " + Mathf.RoundToInt(timeRemaining).ToString() + "seconds";

        // �ؽ�Ʈ ���� ����
        textMesh.color = Color.red;

        // �ؽ�Ʈ ũ�� ����
        textMesh.fontSize = 30; // ���ϴ� ũ��� ����
    }
}
