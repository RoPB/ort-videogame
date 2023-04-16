using System;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }
}

