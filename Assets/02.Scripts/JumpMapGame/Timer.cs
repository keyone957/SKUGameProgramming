using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//���ѽð� ���� ������Ʈ
//Ÿ�̸� ���� �� �ʱ�ȭ �ϴ� �κ� ����
//Ÿ�Ӿƿ� �� ������ ������Ʈ �ۼ��Ϸ�
//�̱��� ����
// �ۼ��� : ������
// ������ : ȫ����
// ���� ������ : 2024-06-09


public class Timer : MonoBehaviour
{
    // UI ��ҵ��� ������ SerializedField ������
    [SerializeField] private Image UiFill; // ī��Ʈ �ٿ� �ð��� �ð������� ��Ÿ���� �̹���
    [SerializeField] private TextMeshProUGUI UiText; // ī��Ʈ �ٿ� �ð��� ǥ���ϴ� TextMeshProUGUI �ؽ�Ʈ
    [SerializeField] private Color warningColor = Color.red; // ���� �ð��� 10�� �̸��� ���� ����

    // ī��Ʈ �ٿ��� �� �ð��� �����ϴ� ����
    public int Duration;
    // ���� �ð��� �����ϴ� ����
    public int remainingDuration;

    private Color defaultColor; // �⺻ ������ �����ϴ� ����
    private Color defaultTextColor; // �⺻ �ؽ�Ʈ ������ �����ϴ� ����
    private bool isBlinking = false; // ������ ȿ�� ���θ� �����ϴ� ����

    // ��ũ��Ʈ�� ���۵� �� ȣ��Ǵ� �޼���
    private void Start()
    {
        defaultColor = UiFill.color;
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

            // ���� �ð��� 10�� �̸��̰� �������� ���۵��� �ʾҴٸ� ������ �ڷ�ƾ ����
            if (remainingDuration < 10 && !isBlinking)
            {
                StartCoroutine(BlinkWarning());
            }

            // ���� �ð��� 1�� ����
            remainingDuration--;
            // 1�� ���
            yield return new WaitForSeconds(1.0f);
        }
        // ī��Ʈ �ٿ� ���� �� ȣ��Ǵ� �޼���
        OnEnd();

    }

    // ī��Ʈ �ٿ��� ����� �� ȣ��Ǵ� �޼���
    private void OnEnd()
    {
        // Ÿ�̸Ӱ� �����Ǹ� ���� �˾� �г��� Ȱ��ȭ
        JumpMapSystem.instance.ActivateEndPanel();
    }



    private IEnumerator BlinkWarning()
    {
        isBlinking = true;

        while (remainingDuration > 0 && remainingDuration < 10)
        {
            UiFill.color = warningColor;
            UiText.color = warningColor;
            yield return new WaitForSeconds(0.5f);
            UiFill.color = defaultColor;
            UiText.color = defaultTextColor;
            yield return new WaitForSeconds(0.5f);
        }

        UiFill.color = defaultColor;
        UiText.color = defaultTextColor;
        isBlinking = false;
    }
}