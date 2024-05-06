using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//로비 씬으로 이동하게 변경
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-06
public class TitleMenu : MonoBehaviour
{
    [SerializeField] private Button _btnStart=null;
    [SerializeField] private FadeOverlay _fadeOverlay=null;

    //버튼에 이벤트 연결
    private void Awake()
    {
        _btnStart.onClick.AddListener(OnClickStartBtn);
    }

    private void OnClickStartBtn()
    {
        _fadeOverlay.DoFadeOut(1.5f,"Lobby");
    }
}
