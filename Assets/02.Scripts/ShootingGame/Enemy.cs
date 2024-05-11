using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� �������� �� ������� ��� ����
// ���� �ۼ���: �ϰ渲
// ���� ������: 2024-05-11
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