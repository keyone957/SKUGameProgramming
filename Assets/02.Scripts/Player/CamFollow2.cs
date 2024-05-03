using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow2 : MonoBehaviour
{
    Vector3 dir;
    [SerializeField]
    GameObject player;
    
    private void Update()
    {
        transform.position = player.transform.position + dir;
        transform.LookAt(player.transform);
    }
  
}
