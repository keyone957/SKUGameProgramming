using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//플레이어 피격시 체력닳기
// 최초 작성자 : 홍원기
// 수정자 : 
// 최종 수정일 : 2024-05-27
public class MonsterAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}