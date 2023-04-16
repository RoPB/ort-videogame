using System;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    private bool _enemyMovementControllerInitiated;

    public void Init()
    {
        _enemyMovementControllerInitiated = true;
    }

    public void Stop()
    {
        _enemyMovementControllerInitiated = false;
    }

    private void Update()
    {
        if (_enemyMovementControllerInitiated)
        {
            transform.position +=
            Vector3.left *
                GameManager.Instance.enemiesVelocity *
                    GameManager.Instance.enemiesVelocityMultiplier *
                        Time.deltaTime;

            if (this.IsOutOfScene())
            {
                GameManager.Instance.enemyPooler.ReturnToPool(this.gameObject);
            }
        }
        
    }

    private bool IsOutOfScene()
    {
        return GameManager.Instance.IsLocatedAtTheLeftOfTheScene(this.transform.position, this.transform.localScale);
    }

}

