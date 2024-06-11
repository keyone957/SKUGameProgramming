using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// �ѱ��ھ� ������ ��ȭâ
// ���� �ۼ���: ȫ����
// ������: 
// ���� ������: 2024-06-11
public class TwinkleChat : MonoBehaviour
{ 
    [SerializeField] public TMP_Text targetText;
    [SerializeField] private float delay;
    [SerializeField] private Button nextBtn;
    private string text;
    [SerializeField] private GameObject bossMonster;
    
    private void Start()
    {
        text = targetText.text.ToString();
        targetText.text = " ";
        nextBtn.onClick.AddListener(OnClickNextBtn);
        StartCoroutine(textPrint());
    }
    IEnumerator textPrint()
    {
        int count = 0;
        while (count != text.Length)
        {
            if (count < text.Length)
            {
                targetText.text += text[count].ToString();
                count++;
                SoundManager._instance.PlaySound(Define._textSound);
            }

            yield return new WaitForSeconds(delay);
        }
        nextBtn.gameObject.SetActive(true);
    }

    private void OnClickNextBtn()
    {
        gameObject.SetActive(false);
        Destroy(bossMonster);
    }
}
