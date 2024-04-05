using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//타이틀 씬안에 있는 메뉴들 관리하는 컴포넌트
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-04-05
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
        _fadeOverlay.DoFadeOut(1.5f,"SampleScene");
    }
}
