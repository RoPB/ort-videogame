using System;
using UnityEngine;

public class EnemyMovementController : IEnemyMovementController
{
    private void FixedUpdate()
    {
        transform.position +=
        Vector3.left *
            GameManager.Instance.enemiesVelocity *
                GameManager.Instance.enemiesVelocityMultiplier *
                    Time.deltaTime;

        if (this.IsOutOfScene())
        {
            var enemy = GameObject.FindFirstObjectByType<Enemy>();
            enemy.ReturnToOriginPool();
        }
        
    }

    private bool IsOutOfScene()
    {
        return GameManager.Instance.IsLocatedAtTheLeftOfTheScene(this.transform.position, this.transform.localScale);
    }

}

