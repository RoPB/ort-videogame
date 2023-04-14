using System;
using UnityEngine;

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
                bottomRightCorner = this.sceneBottomRightCorner(),
                topLeftCorner = this.sceneTopLeftCorner()
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
                    GameManager.Instance.sceneBounds.left + width / 2,
                    GameManager.Instance.sceneBounds.right - width / 2
                );
    }

    public float clampYInSceneBounds(float newY, float height)
    {
        return Math.Clamp(
            newY,
            GameManager.Instance.sceneBounds.bottom + height / 2,
            GameManager.Instance.sceneBounds.top - height / 2
        );
    }

    public float getRandomYInSceneBounds(Vector3 scale)
    {
        var maxY = GameManager.Instance.sceneBounds.top - scale.y / 2;
        var minY = GameManager.Instance.sceneBounds.bottom + scale.y / 2;
        return UnityEngine.Random.Range(minY, maxY);
    }

    public float getSceneMaxX()
    {
        return this.sceneBounds.right;
    }

    #region Bounds Auxiliar Methods

    private Vector3 sceneTopLeftCorner()
    {
        var topLeftCornerRef = GameObject.FindGameObjectWithTag("topLeftCorner");
        return topLeftCornerRef.transform.position;
    }

    private Vector3 sceneBottomRightCorner()
    {
        var bottomRightCornerRef = GameObject.FindGameObjectWithTag("bottomRightCorner");
        return bottomRightCornerRef.transform.position;
    }

    #endregion
}

public struct SceneBounds
{
    public float top
    {
        get
        {
            return topLeftCorner.y;
        }
    }

    public float bottom
    {
        get
        {
            return bottomRightCorner.y;
        }
    }

    public float left
    {
        get
        {
            return topLeftCorner.x;
        }
    }

    public float right
    {
        get
        {
            return bottomRightCorner.x;
        }
    }

    public Vector3 topLeftCorner;
    public Vector3 bottomRightCorner;
}