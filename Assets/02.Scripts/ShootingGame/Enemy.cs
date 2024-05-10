using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 몬스터 공격했을 시 사라지는 기능 구현
// 최초 작성자: 하경림
// 최종 수정일: 2024-05-11
public class Enemy : MonoBehaviour
{
    public int hp = 1;

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}