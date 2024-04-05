using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 맨처음에 게임 시작할 때 로고 or 이미지 나오고 페이드인 페이드 아웃 후 타이틀 화면으로 넘어감(그냥 연출용)
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-04-05
public class ShowImage : MonoBehaviour
{
    [SerializeField] public bool _finished = false;
    [SerializeField] private Image _sprite = null;


    private void Start()
    {
        _finished = false;
        StartCoroutine(Do());
    }
    //페이드인 페이드 아웃 실행 코루틴
    //효과 실행 하고 나면 타이틀 씬으로 이동
    public IEnumerator Do()
    {
        yield return StartCoroutine(Fade(true, 2.0f));
        yield return StartCoroutine(Fade(false, 1.0f));
        _finished = true;
        SceneManager.LoadScene("Title");
    }

    //서서히 알파값 조절하여 페이드 인 페이드 아웃 기능 추가 
    private IEnumerator Fade(bool isOut, float duration)
    {
        float startTime = Time.time;
        float startAlpha = (isOut ? 0.0f : 1.0f);
        float endAlpha = (isOut ? 1.0f : 0.0f);
        Color color = _sprite.color;

        while (Time.time - startTime - duration < 0)
        {
            float elapsed = (Time.time - startTime);
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            _sprite.color = color;
            yield return null;
        }

        color.a = endAlpha;
        _sprite.color = color;
    }

}