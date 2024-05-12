using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Button exitBtn;

    private void Start()
    {
        exitBtn.onClick.AddListener(OnClickExitBtn);
    }
    private void OnClickExitBtn()
    {
        gameObject.SetActive(false);
    }
}
