using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//모든 씬에서 사용할 UI관리하는 컴포넌트
//체력, 돈. 등.
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-05-08
public class AllSceneCanvas : MonoBehaviour
{
    public static AllSceneCanvas instance { get; private set; }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}