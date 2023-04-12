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
        return GameManager.Instance.isLocatedAtTheLeftOfTheScene(this.transform.position, this.transform.localScale);
    }

    private void Spawn()
    {
        float randomY = GameManager.Instance.getRandomYInSceneBounds();
        float maxX = GameManager.Instance.getSceneMaxX();
        transform.position = new Vector3(maxX, randomY, 0);
    }

}

