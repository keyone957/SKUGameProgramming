using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��Ʈ hp ���� �Լ�
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

        // ��ü ��Ʈ�� ���� 'Back' �̹����� ����
        for (int i = 0; i < Hp_Max; i++)
        {
            Heart[i].sprite = Back;
        }

        // ���� ü�¿� �ش��ϴ� ��Ʈ�� 'Front' �̹����� ����
        for (int i = 0; i < Hp; i++)
        {
            Heart[i].sprite = Front;
        }
    }
}


