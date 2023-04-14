using System;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    private void Update()
    {
        transform.position +=
            Vector3.left *
                GameManager.Instance.enemiesVelocity *
                    GameManager.Instance.enemiesVelocityMultiplier *
                        Time.deltaTime;

        if (this.isOutOfScene())
        {
            EnemyPooler.Instance.ReturnToPool(this.gameObject);
        }
    }

    private bool isOutOfScene()
    {
        return GameManager.Instance.isLocatedAtTheLeftOfTheScene(this.transform.position, this.transform.localScale);
    }

}

