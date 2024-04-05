using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//씬이동할때 페이드인 페이드아웃
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-04-05
public class FadeOverlay : MonoBehaviour
{
    [SerializeField]
    private Image _sprite = null;
    private GameObject _go;
    private GameObject _Go { get { if (_go == null) { _go = gameObject; } return _go; } }
    private Coroutine _fadeCoroutine;

  
    //어두워지기
    public void DoFadeOut(float duration,string sceneName)
    {
        StopFade();
        _Go.SetActive(true);

        if (duration != 0.0f)
        {
            _fadeCoroutine = StartCoroutine(Fade(true, duration,sceneName));
        }
        else
        {
            Color color = _sprite.color;
            color.a = 1.0f;
            _sprite.color = color;
        }
    }

   //밝아지기
    public void DoFadeIn(float duration,string sceneName)
    {
        StopFade();

        if (duration != 0.0f)
        {
            _Go.SetActive(true);
            _fadeCoroutine = StartCoroutine(Fade(false, duration,sceneName));
        }
        else
            _Go.SetActive(false);
    }

    private IEnumerator Fade(bool isOut, float duration,string sceneName)
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

        if (!isOut)
        {
            _Go.SetActive(false);
           
        }

        _fadeCoroutine = null;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void StopFade()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
    }

    public bool IsFading() => (_fadeCoroutine != null);
}