using System;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        transform.position +=
            Vector3.left *
                GameManager.Instance.enemiesVelocity *
                    GameManager.Instance.enemiesVelocityMultiplier *
                        Time.deltaTime;

        if (this.isOutOfScene())
        {
            this.Spawn();
        }
    }

    private bool isOutOfScene()
    {
        return transform.position.x + transform.localScale.x/2 < GameManager.Instance.sceneBounds.topLeftCorner.x;
    }

    private void Spawn()
    {
        var sceneTopRightCorner = GameManager.Instance.sceneBounds.topRightCorner;
        var sceneBottomRightCorner = GameManager.Instance.sceneBounds.bottomRightCorner;
        var maxY = sceneTopRightCorner.y - transform.localScale.y/2;
        var minY = sceneBottomRightCorner.y + transform.localScale.y/2;
        System.Random random = new System.Random();
        double randomY = (random.NextDouble() * (maxY - minY) + minY);
        transform.position = new Vector3(sceneTopRightCorner.x, (float)randomY, 0);
    }

}

