using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//맨처음 로고에서 이전에 저장된 화면모드 값을 가져와 화면모드 조절
// 최초 작성자 : 홍원기
// 수정자 :
// 최종 수정일 : 2024-05-31
public class InitialGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("screenMode") == "Window")
        {
            Screen.fullScreen = false;
            Screen.SetResolution(1280, 720, false);
        }
        else if (PlayerPrefs.GetString("screenMode") == "FullScreen")
        {
            Screen.fullScreen = true;
            Screen.SetResolution(1920, 1080, true);
        }
    }
}
