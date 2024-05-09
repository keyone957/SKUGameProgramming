using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class test : MonoBehaviour
{
  private void Start()
  {
    SceneSystem.instance._fadeOverlay.gameObject.SetActive(false);
  }
}
