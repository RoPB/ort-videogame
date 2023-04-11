using System;
using Unity.VisualScripting;
using UnityEngine;

public struct SceneBounds{
    public Vector3 topRightCorner;
    public Vector3 bottomRightCorner;
    public Vector3 topLeftCorner;
    public Vector3 bottomLeftCorner;
}

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
            Instance.sceneBounds = new SceneBounds()
            {
                topRightCorner = this.sceneTopRightCorner(),
                bottomRightCorner = this.sceneBottomRightCorner(),
                topLeftCorner = this.sceneTopLeftCorner(),
                bottomLeftCorner = this.sceneBottomLeftCorner()
            };
        }
    }

    public SceneBounds sceneBounds;

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

    #region auxiliar

    private Vector3 sceneTopRightCorner()
    {
        var topRightCornerRef = GameObject.FindGameObjectWithTag("topRightCorner");
        return topRightCornerRef.transform.position;
    }

    private Vector3 sceneBottomRightCorner()
    {
        var bottomRightCornerRef = GameObject.FindGameObjectWithTag("bottomRightCorner");
        return bottomRightCornerRef.transform.position;
    }

    private Vector3 sceneTopLeftCorner()
    {
        var topLeftCornerRef = GameObject.FindGameObjectWithTag("topLeftCorner");
        return topLeftCornerRef.transform.position;
    }

    private Vector3 sceneBottomLeftCorner()
    {
        var bottomLeftCornerRef = GameObject.FindGameObjectWithTag("bottomLeftCorner");
        return bottomLeftCornerRef.transform.position;
    }

    #endregion
}

