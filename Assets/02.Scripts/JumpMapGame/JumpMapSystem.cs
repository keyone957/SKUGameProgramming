using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

// �������� ����ϴ� ������Ʈ ����
// �������� �˾�â ���� �ڵ� �߰�, ���� �ð��� ���� ���� �ο�, �������� �ٸ� ��������
// �׽�Ʈ �ڵ� �߰�
// ���� �ۼ��� : ������
// ������ : ȫ����
// ���� ������ : 2024-06-11

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

    // �̱��� �ν��Ͻ�
    public static JumpMapSystem instance;

    private void Awake()
    {
        // �ν��Ͻ��� �������� ������ ���� �ν��Ͻ��� ����
        if (instance == null)
        {
            instance = this;
        }
        // �ν��Ͻ��� �̹� �����ϰ� ���� �ν��Ͻ��� �ƴ϶�� �� �ν��Ͻ��� �ı�
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
        remainingTimeText.text = $" Rank : {message} \n\n ����� ����� : {timer.ToString("F2")}���Դϴ�. ";
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
        //�Ʒ��� �׽�Ʈ�ڵ�
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