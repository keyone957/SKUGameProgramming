using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 보스 스테이지 UI
// 최초 작성자: 홍원기
// 수정자: 
// 최종 수정일: 2024-06-11
public class BossCanvas : MonoBehaviour
{
    [SerializeField] public TMP_Text targetText;
    [SerializeField] public Button nextBtn;
    [SerializeField] public Button startBtn;
    [SerializeField] public GameObject chatPanel;
    [SerializeField] public GameObject rulePanel;
    private string text;
    [SerializeField] private float delay;

    void Start()
    {
        text = targetText.text.ToString();
        targetText.text = " ";
        nextBtn.onClick.AddListener(OnClickNextBtn);
        startBtn.onClick.AddListener(OnClickStartBtn);
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
        // Time.timeScale = 0.0f;
        AllSceneCanvas.instance.isOpenMenu = true;
        chatPanel.SetActive(false);
        rulePanel.SetActive(true);
    }

    private void OnClickStartBtn()
    {
        // Time.timeScale = 1.0f;
        AllSceneCanvas.instance.isOpenMenu = false;
        rulePanel.SetActive(false);
        BossStageSystem.instance.playerInput.enabled = true;
        BossStageSystem.instance.startBossStage = true;
    }
}