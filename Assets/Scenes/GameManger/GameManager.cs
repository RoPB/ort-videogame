using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public float playerVelocity = 1.0f;
    [SerializeField]
    [Range(0, 5)]
    public float maximumPlayerDisplacement = 0;

    public float enemiesVelocity = 1.0f;
    public float enemiesVelocityMultiplier = 1;

    //horizontalMovement is a float between -1,1
    public void changePlayerVelocity(float horizontalMovement)
    {
        enemiesVelocityMultiplier += horizontalMovement;
    }
}

