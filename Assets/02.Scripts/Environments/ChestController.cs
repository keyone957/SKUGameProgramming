using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//게임 클리어시 나타나는 보물상자 및 w누르면 돈 추가
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-05-14
public class ChestController : MonoBehaviour
{
    public enum CHESTTYPE
    {
        Wooden,
        Iron,
        Golden
    }
    [SerializeField] private GameObject keyBoardUI;
    [SerializeField] private List<Sprite> chestSpr = new List<Sprite>();
    [SerializeField] private CHESTTYPE chestType;
    [SerializeField] private AudioSource chestSound;
    private float duration = 1.0f;
    private int chestMoney;
    private Animator chestAnim;
    private SpriteRenderer chestSprRenderer;
    private bool isFadingIn = false;
    private bool isEnter;

    private void Start()
    {
        chestSprRenderer = GetComponent<SpriteRenderer>();
        chestAnim = GetComponent<Animator>();
        SetRandomChest(GetRandomChestType());
        isEnter = false;
    }

    private void Update()
    {
        if (SceneSystem.instance.isClearStage && !isFadingIn)
        {
            StartCoroutine(AppearChest());
        }

        if (isEnter && Input.GetKeyDown(KeyCode.W))
        {
            chestSound.Play();
            ChestAnim();
        }

        switch (chestType)
        {
            case CHESTTYPE.Wooden:
                chestAnim.SetBool("IdleWooden",true);
                break;
            case CHESTTYPE.Iron :
                chestAnim.SetBool("IdleIron",true);
                break;
            case CHESTTYPE.Golden:
                chestAnim.SetBool("IdleGolden",true);
                break;
        }
    }

    private void ChestAnim()
    {
        switch (chestType)
        {
            case CHESTTYPE.Wooden:
                chestAnim.SetTrigger("OpenWooden");
                break;
            case CHESTTYPE.Iron:
                chestAnim.SetTrigger("OpenIron");
                break;
            case CHESTTYPE.Golden:
                chestAnim.SetTrigger("OpenGolden");
                break;
        }
    }

    private void SetRandomChest(CHESTTYPE chesttype)
    {
        chestType = chesttype;
        switch (chestType)
        {
            case CHESTTYPE.Wooden:
                chestSprRenderer.sprite = chestSpr[0];
                chestMoney = 100;
                break;
            case CHESTTYPE.Iron:
                 chestSprRenderer.sprite = chestSpr[1];
                 chestMoney = 200;
                break;
            case CHESTTYPE.Golden:
                chestSprRenderer.sprite = chestSpr[2];
                chestMoney = 300;
                break;
        }
    }
    private CHESTTYPE GetRandomChestType()
    {
        Array values = Enum.GetValues(typeof(CHESTTYPE));
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        return (CHESTTYPE)values.GetValue(randomIndex);
    }

    private IEnumerator AppearChest()
    {
        isFadingIn = true;
        Color color = chestSprRenderer.color;
        float startAlpha = color.a;
        float endAlpha = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = newAlpha;
            chestSprRenderer.color = color;
            yield return null;
        }
        color.a = endAlpha;
        chestSprRenderer.color = color;
        isFadingIn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&SceneSystem.instance.isClearStage)
        {
            keyBoardUI.SetActive(true);
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        keyBoardUI.SetActive(false);
        isEnter = false;
    }

    public void OpenChestEndEvent()
    {
        PlayerManager.instance.playerMoney += chestMoney;
        AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
        Destroy(gameObject);
    }
}