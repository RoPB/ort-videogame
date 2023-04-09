using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
    }

}

