using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChestController : MonoBehaviour
{
    private int chestMoney;
    private Animator chestAnim;
    private bool isEnter;

    [SerializeField]private GameObject camera;

    [SerializeField] private PlayerInputController player;
    //Animator �Ķ������ �ؽð� ����
    private readonly int hashChestOpen = Animator.StringToHash("OpenChest");
    private void Start()
    {
        chestAnim = GetComponent<Animator>();
        isEnter = false;
    }

    private void Update()
    {

        if (isEnter && Input.GetKeyDown(KeyCode.W))
        {
            player.enabled = false;
            SoundManager._instance.PlaySound(Define._clearChestSound);
            camera.SetActive(true);
            ChestAnim();
        }
    }

    private void ChestAnim()
    {
        chestAnim.SetTrigger(hashChestOpen);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isEnter = false;
    }

    public void OpenChestEndEvent()
    {
        // PlayerManager.instance.playerMoney += chestMoney;
        // AllSceneCanvas.instance.SetMoney(PlayerManager.instance.playerMoney);
        Destroy(gameObject);
    }
}