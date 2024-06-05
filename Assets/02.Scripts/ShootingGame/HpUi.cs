using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 하트 소멸 함수
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-04
public class HpUi : MonoBehaviour
{
    public ShootingTimer shootingTimer;
    public Image[] Heart;
    public int Hp { get; private set; }
    private int Hp_Max;
    public Sprite Back, Front;

    private void Awake()
    {
        Hp_Max = Heart.Length;
        Hp = Hp_Max;

        for (int i = 0; i < Hp_Max; i++)
        {
            if (Hp > i)
            {
                Heart[i].sprite = Front;
            }
        }
    }

    public void SetHp(int val)
    {
        Hp += val;
        Hp = Mathf.Clamp(Hp, 0, Hp_Max);

        for (int i = 0; i < Hp_Max; i++)
        {
            Heart[i].sprite = Back;
        }

        for (int i = 0; i < Hp; i++)
        {
            Heart[i].sprite = Front;
        }

        if (Hp == 0)
        {
            if (shootingTimer != null)
            {
                shootingTimer.SetTimerToZero();
            }
            ChildGenerator.Instance.DestroyAllChildren();
        }
    }
}
