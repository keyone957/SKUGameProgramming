using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적생성 함수 구현
//Dondestroy삭제
// 최초 작성자: 하경림
// 수정자: 홍원기
// 최종 수정일: 2024-06-06
public class ChildGenerator : MonoBehaviour
{
    public Transform gridLayout;
    public GameObject[] childPrefabs;
    public float xOffset = 2f;
    public int hp = 1;
    private List<GameObject> childList = new List<GameObject>();
    private static ChildGenerator instance;
    private int destroyedChildCount = 0;
    public static ChildGenerator Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private bool stopGeneration = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public string GetFirstTag()
    {
        return childList.Count > 0 ? childList[0].tag : string.Empty;
    }

    private void Update()
    {
        if (!stopGeneration && childList.Count <= 7)
        {
            GameObject newChild = Instantiate(childPrefabs[Random.Range(0, childPrefabs.Length)], gridLayout);
            childList.Add(newChild);
        }

        for (int i = 0; i < childList.Count; i++)
        {
            childList[i].transform.SetSiblingIndex(i);
            childList[i].transform.localPosition = new Vector3(i * xOffset, 0f, 0f);
        }

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

    public void RemoveFirstChild()
    {
        if (childList.Count > 0)
        {
            Destroy(childList[0].gameObject);
            childList.RemoveAt(0);
            destroyedChildCount++;
        }
    }

    public void DestroyAllChildren()
    {
        foreach (var child in childList)
        {
            Destroy(child);
        }
        childList.Clear();
        stopGeneration = true;
    }

    public int GetMonsterAttackCount()
    {
        return destroyedChildCount;
    }
}