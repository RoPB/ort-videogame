using System;
using Unity.VisualScripting;
using UnityEngine;

public struct SceneBounds
{
    public Vector3 topRightCorner;
    public Vector3 bottomRightCorner;
    public Vector3 topLeftCorner;
    public Vector3 bottomLeftCorner;
}

public class GameManager : MonoBehaviour
{

    private SceneBounds sceneBounds;


    public float enemiesVelocity = 1.0f;
    public float enemiesVelocityMultiplier = 1;

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
            this.sceneBounds = new SceneBounds()
            {
                topRightCorner = this.sceneTopRightCorner(),
                bottomRightCorner = this.sceneBottomRightCorner(),
                topLeftCorner = this.sceneTopLeftCorner(),
                bottomLeftCorner = this.sceneBottomLeftCorner()
            };
            Instance = this;
        }
    }

    //horizontalMovement is a float between -1,1
    // Returns a value between -1 and 1
    public float changePlayerVelocity(float horizontalMovement)
    {
        enemiesVelocityMultiplier = Mathf.Clamp(enemiesVelocityMultiplier + horizontalMovement, 0.5f, 1.5f);
        return enemiesVelocityMultiplier * 2 - 2;
    }

    public bool isLocatedAtTheLeftOfTheScene(Vector3 transform, Vector3 localScale)
    {
        return transform.x + localScale.x / 2 < GameManager.Instance.sceneBounds.topLeftCorner.x;
        //|| transform.x + localScale.x / 2 > GameManager.Instance.sceneBounds.topRightCorner.x
        //|| transform.y + localScale.y / 2 > GameManager.Instance.sceneBounds.topLeftCorner.y
        //|| transform.y + localScale.y / 2 < GameManager.Instance.sceneBounds.bottomLeftCorner.y;
    }

    public float clampXInSceneBounds(float newX, float width)
    {
        return Math.Clamp(
                    newX,
                    GameManager.Instance.sceneBounds.bottomLeftCorner.x + width / 2,
                    GameManager.Instance.sceneBounds.bottomRightCorner.x - width / 2
                );
    }

    public float clampYInSceneBounds(float newY, float height)
    {
        return Math.Clamp(
            newY,
            GameManager.Instance.sceneBounds.bottomLeftCorner.y + height / 2,
            GameManager.Instance.sceneBounds.topLeftCorner.y - height / 2
        );
    }

    public float getRandomYInSceneBounds()
    {
        var sceneTopRightCorner = GameManager.Instance.sceneBounds.topRightCorner;
        var sceneBottomRightCorner = GameManager.Instance.sceneBounds.bottomRightCorner;
        var maxY = sceneTopRightCorner.y - transform.localScale.y / 2;
        var minY = sceneBottomRightCorner.y + transform.localScale.y / 2;
        System.Random random = new System.Random();
        double randomY = (random.NextDouble() * (maxY - minY) + minY);
        return (float)randomY;
    }

    public float getSceneMaxX()
    {
        return this.sceneBounds.topRightCorner.x;
    }

    #region Bounds Auxiliar Methods

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

