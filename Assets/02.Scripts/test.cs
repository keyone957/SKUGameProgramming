using System.Collections.Generic;
using UnityEngine;
//중복 랜덤수 안나오게
public class test : MonoBehaviour
{
   
    private List<int> usedNormalStages = new List<int>();
    
    private void Start()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            int uniqueNumber = GetUniqueRandomNumber(1, 3, usedNormalStages);
            if (uniqueNumber != -1)
            {
                Debug.Log(uniqueNumber);
                usedNormalStages.Add(uniqueNumber);
            }
            else
            {
                Debug.Log("No unique numbers left!");
            }
        }
    }
    
    private int GetUniqueRandomNumber(int min, int max, List<int> usedNumbers)
    {
        List<int> possibleNumbers = new List<int>();
        for (int i = min; i <= max; i++)
        {
            if (!usedNumbers.Contains(i))
            {
                possibleNumbers.Add(i);
            }
        }

        if (possibleNumbers.Count == 0)
        {
            return -1;
        }

        int randomIndex = Random.Range(0, possibleNumbers.Count);
        return possibleNumbers[randomIndex];
    }
}
