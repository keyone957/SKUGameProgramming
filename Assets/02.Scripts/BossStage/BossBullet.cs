using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("¾¾¹ß·Ã¾Æ!");
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}