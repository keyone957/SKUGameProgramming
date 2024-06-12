using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//서서히 zoom하는 카메라
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-06-12
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private GameObject clearPanel;
    public float zoomDuration;
    public float startSize = 12f;
    public float endSize = 2f;

    private void Start()
    {
        if (virtualCamera != null)
        {
            StartCoroutine(ZoomCoroutine());
        }
        SoundManager._instance.StopBGM();
    }

    private IEnumerator ZoomCoroutine()
    {
        float elapsedTime = 0f;
        float initialSize = virtualCamera.m_Lens.OrthographicSize;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomDuration;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, t);
            yield return null;
        }
        
        virtualCamera.m_Lens.OrthographicSize = endSize;
        yield return new WaitForSeconds(2.8f);
        
        clearPanel.SetActive(true);
    }
}
