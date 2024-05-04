using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//포탈에 닿았을때 수행해야할 동작들
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-05-04
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