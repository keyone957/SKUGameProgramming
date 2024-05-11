using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildGenerator : MonoBehaviour
{
    public Transform gridLayout;
    public GameObject[] childPrefabs;
    public float xOffset = 2f;
    public int hp = 1;

    private List<Transform> childList = new List<Transform>();

    void Update()
    {
        if (childList.Count <= 7)
        {
            GameObject newChild = Instantiate(childPrefabs[Random.Range(0, childPrefabs.Length)], gridLayout);
            childList.Add(newChild.transform);
        }

        for (int i = 0; i < childList.Count; i++)
        {
            childList[i].SetSiblingIndex(i);
            childList[i].localPosition = new Vector3(-i * xOffset, 0f, 0f);
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
        }
    }


    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
