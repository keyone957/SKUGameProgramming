using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 하트 소멸 함수
// 최초 작성자: 하경림
// 수정자: 하경림
// 최종 수정일: 2024-06-04
public class HpUi : MonoBehaviour
{
    public GameObject Finish;

    public ShootingTimer shootingTimer;
    public Image[] Heart;
    public TMP_Text monsterText;
    public TMP_Text Coin;
        public TMP_Text Fail;

    int num;


    public int Hp { get; private set; }
    private int Hp_Max;
    public Sprite Back, Front;
    private ChildGenerator childGenerator;

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
    private void Start()
    {
        childGenerator = ChildGenerator.Instance; 
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
                Finish.SetActive(true);
                ChildGenerator.Instance.DestroyAllChildren();
                int destroyedChildCount = childGenerator.GetMonsterAttackCount();
                Debug.Log("삭제된 몬스터 수: " + destroyedChildCount);
                num = destroyedChildCount * 50;
                Coin.text = Coin.text = num.ToString();
                Fail.text = "실패 !";

                monsterText.text = destroyedChildCount.ToString();


            }
        }
    }
