using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 하트 hp 관련 함수
public class HpUi : MonoBehaviour
{
    public Image[] Heart;
    public int Hp { get; private set;}
    private int Hp_Max;
    public Sprite Back, Front;

    private void Awake()
    {
        Hp_Max = Heart.Length;

        Hp = Hp_Max;

        for (int i = 0; i < Hp_Max; i++)
            if (Hp > i)
            {
                Heart[i].sprite = Front;
            }
    }

    public void SetHp(int val)
    {
        Hp += val;

        Hp = Mathf.Clamp(Hp, 0, Hp_Max);

        // 전체 하트를 먼저 'Back' 이미지로 설정
        for (int i = 0; i < Hp_Max; i++)
        {
            Heart[i].sprite = Back;
        }

        // 현재 체력에 해당하는 하트만 'Front' 이미지로 설정
        for (int i = 0; i < Hp; i++)
        {
            Heart[i].sprite = Front;
        }
    }
}


