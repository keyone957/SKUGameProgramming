using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private GameObject keyBoardUI;
    [SerializeField] private List<Sprite> portalSpr = new List<Sprite>();
    [SerializeField] private List<AudioClip> doorSound = new List<AudioClip>();
    private AudioSource doorAudioSource;

    private void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            keyBoardUI.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = portalSpr[1];
            // doorAudioSource.clip=doorSound
            doorAudioSource.PlayOneShot(doorSound[0]);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        keyBoardUI.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = portalSpr[0];
        doorAudioSource.PlayOneShot(doorSound[1]);
    }
}