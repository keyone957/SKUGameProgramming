using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// 게임 시작전 3,2,1, GO 게임신 정지 함수
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-03

public class CountDown : MonoBehaviour
{
 public GameObject Panel;
    public TMP_Text Count;
    public float countdownTime = 3.0f;

    private void Start()
    {
        // 게임이 시작될 때 일시정지 상태로 설정
        Time.timeScale = 0;

        // 패널을 활성화하고 카운트다운 텍스트를 비활성화
        Panel.SetActive(true);
        Count.gameObject.SetActive(false);

        // Start 버튼 클릭 이벤트에 StartGame 함수 연결
    }
    private void Update()
    {
        // 스페이스바 입력을 감지하여 StartGame 함수를 호출
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        // 패널을 비활성화하고 카운트다운 시작
        Panel.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        // 카운트다운 텍스트를 활성화
        Count.gameObject.SetActive(true);

        float realTimeCountdown = countdownTime;

        // 카운트다운을 진행
        while (realTimeCountdown > 0)
        {
            Count.text = realTimeCountdown.ToString("0");
            yield return new WaitForSecondsRealtime(1.0f);
            realTimeCountdown--;
        }

        // 카운트다운이 끝났을 때 "Go!" 텍스트 표시
        Count.text = "Go!";
        yield return new WaitForSecondsRealtime(1.0f);

        // 카운트다운 텍스트를 비활성화
        Count.gameObject.SetActive(false);

        // 게임을 다시 시작
        Time.timeScale = 1;
    }
}
