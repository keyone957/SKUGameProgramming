using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Count : MonoBehaviour
{
    // UI ��ҵ��� ������ SerializedField ������
    [SerializeField] private Image UiFill; // ī��Ʈ �ٿ� �ð��� �ð������� ��Ÿ���� �̹���
    [SerializeField] private TextMeshProUGUI UiText; // ī��Ʈ �ٿ� �ð��� ǥ���ϴ� TextMeshProUGUI �ؽ�Ʈ

    // ī��Ʈ �ٿ��� �� �ð��� �����ϴ� ����
    public int Duration;

    // ���� �ð��� �����ϴ� ����
    public int remainingDuration;

    // ��ũ��Ʈ�� ���۵� �� ȣ��Ǵ� �޼���
    private void Start()
    {
        // ī��Ʈ �ٿ��� �����ϴ� �޼��� ȣ��
        Being(Duration);
    }

    // ī��Ʈ �ٿ��� �����ϴ� �޼���
    private void Being(int Second)
    {
        // ���� �ð��� ����
        remainingDuration = Second;

        StartCoroutine(UpdateTimer());
    }

    // ī��Ʈ �ٿ��� ������Ʈ
    private IEnumerator UpdateTimer()
    {
        // ���� �ð��� 0 �̻��� ���� �ݺ�
        while (remainingDuration >= 0)
        {
            // �ؽ�Ʈ�� ���� �ð��� ��:�� �������� ǥ��
            UiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            // �̹����� fillAmount�� �ð��� ����� ���� �����Ͽ� �ð������� ǥ��
            UiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            // ���� �ð��� 1�� ����
            remainingDuration--;
            // 1�� ���
            yield return new WaitForSeconds(1.0f);
        }
        // ī��Ʈ �ٿ� ���� �� ȣ��Ǵ� �޼���
        OnEnd();
        InitializeGame();
    }

    // ī��Ʈ �ٿ��� ����� �� ȣ��Ǵ� �޼���
    private void OnEnd()
    {
        print("Time Over");
    }

    private void InitializeGame()
    {
        // �ð��ʰ� ����ϸ� �ش� ���Ӿ��� �ʱ�ȭ������ ȸ��

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}