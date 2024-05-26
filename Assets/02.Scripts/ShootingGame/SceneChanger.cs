using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//규칙읽고 난 후 버튼 클릭 시 미니게임씬으로 이동

public class SceneChanger : MonoBehaviour
{
    private string sceneName = "GameScene"; 

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
