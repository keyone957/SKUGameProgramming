using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

// 점프맵을 담당하는 컴포넌트 구성
// 게임종료 팝업창 관련 코드 추가, 남은 시간에 따라 점수 부여, 점수별로 다른 색상적용
// 테스트 코드 추가
// 최초 작성자 : 장현우
// 수정자 : 홍원기
// 최종 수정일 : 2024-06-11

public class JumpMapSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer slimeSpr;
    [SerializeField] private SpriteRenderer playerSwordImg;
    [SerializeField] private TMP_Text moneyText;
    private int clearMoney;
    public GameObject popupPanel;
    public GameObject endPanel;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI resultText;
    public Button startButton;
    public Button nextStageButton;
    public Image timerImage;

    private bool isPlayerMovementRestricted = true;
    private PlayerInputController playerInputController;
    private float timer = 0f;
    public float duration;
    public static Vector3 InitialPlayerPosition { get; private set; }

    // 싱글톤 인스턴스
    public static JumpMapSystem instance;

    private void Awake()
    {
        // 인스턴스가 존재하지 않으면 현재 인스턴스로 설정
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 이미 존재하고 현재 인스턴스가 아니라면 이 인스턴스를 파괴
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        InitialPlayerPosition = transform.position;
    }

    private void Start()
    {
        ActivatePopupPanel();
        playerInputController = FindObjectOfType<PlayerInputController>();
        RestrictPlayerMovement(true);
        endPanel.SetActive(false);
        nextStageButton.onClick.AddListener(NextStage);
        startButton.onClick.AddListener(StartButtonClicked);
        StartCoroutine(SceneStartSequence());
        InitializeJumpMap();
    }

    private void Update()
    {
        if (!isPlayerMovementRestricted)
        {
            timer += Time.deltaTime;
            timerImage.fillAmount = timer / duration;

            if (timer >= duration)
            {
                timer = duration;
                TriggerFlag();
            }
        }

        if (timer >= duration)
        {
            ActivateEndPanel();
        }
    }

    public void InitializeJumpMap()
    {
        SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
        AllSceneCanvas.instance.monsterCnt.SetActive(false);
        AllSceneCanvas.instance.SetStageText("BonusStage!");
        slimeSpr.color = PlayerManager.instance.playerColor;
        playerSwordImg.sprite = PlayerManager.instance.playerSwordSpr;
    }

    private IEnumerator SceneStartSequence()
    {
        SoundManager._instance.LoadBGM(Define._jumpMapBGM);
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SoundManager._instance.PlayBGM(Define._jumpMapBGM);
    }

    private void ActivatePopupPanel()
    {
        popupPanel.SetActive(true);
        AllSceneCanvas.instance.isOpenMenu = true;
    }

    private void StartButtonClicked()
    {
        popupPanel.SetActive(false);
        AllSceneCanvas.instance.isOpenMenu = false;
        countdownText.gameObject.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Start!!";
        SoundManager._instance.PlaySound(Define._jumpMapStart);
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        timerImage.gameObject.SetActive(true);

        RestrictPlayerMovement(false);
    }

    public void RestrictPlayerMovement(bool restrict)
    {
        isPlayerMovementRestricted = restrict;
        if (playerInputController != null)
        {
            playerInputController.enabled = !restrict;
        }
    }

    public void TriggerFlag()
    {
        RestrictPlayerMovement(true);
        timerImage.gameObject.SetActive(false);

        float remainingTime = duration - timer;
        string message;

        if (remainingTime <= 0)
        {
            message = "Fail...";
            remainingTimeText.color = Color.red;
            resultText.color = Color.red;
            remainingTimeText.text = "Fail";
            clearMoney = 0;
            moneyText.text = clearMoney.ToString();
        }
        else if (remainingTime <= 10f)
        {
            message = "Not bad";
            remainingTimeText.color = Color.yellow;
            resultText.color = Color.blue;
            clearMoney = 200;
            moneyText.text = clearMoney.ToString();
        }
        else if (remainingTime <= 20f)
        {
            message = "Good";
            remainingTimeText.color = Color.green;
            resultText.color = Color.blue;
            clearMoney = 400;
            moneyText.text = clearMoney.ToString();
        }
        else
        {
            message = "Excellent";
            remainingTimeText.color = Color.blue;
            resultText.color = Color.blue;
            clearMoney = 800;
            moneyText.text = clearMoney.ToString();
        }

        remainingTimeText.text = message;
        resultText.text = (timer >= duration) ? "GameOver" : "Clear!!!";
        remainingTimeText.text = $" Rank : {message} \n\n 당신의 기록은 : {timer.ToString("F2")}초입니다. ";
        ActivateEndPanel();
    }

    public void ActivateEndPanel()
    {
        endPanel.SetActive(true);
        resultText.text = (timer >= duration) ? "GameOver" : "Clear!!!";
    }

    private void NextStage()
    {
        
        // PlayerManager.instance.playerMoney += clearMoney;
        SaveLoadManager.instance.GetMoney(clearMoney);
        AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
        SceneSystem.instance.GoNextStage(SceneSystem.NextStageType.Normal);
        //아래는 테스트코드
        // if (SceneManager.GetActiveScene().name == "BonusStage2")
        // {
        //     SceneSystem.instance.TestNextStage("BonusStage3");
        // }
        // else if (SceneManager.GetActiveScene().name == "BonusStage3")
        // {
        //     SceneSystem.instance.TestNextStage("BonusStage4");
        // }
    }
}