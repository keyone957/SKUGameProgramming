using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject failPanel;
    public CoinTimerScript timerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerFail();
        }
    }

    void TriggerFail()
    {
        if (failPanel != null)
        {
            failPanel.SetActive(true);
        }

        Time.timeScale = 0f;

        if (timerScript != null)
        {
            timerScript.StopCountdown();
        }
    }
}