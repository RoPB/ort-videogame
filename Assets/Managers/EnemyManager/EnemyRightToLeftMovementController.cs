using System;
using UnityEngine;

public class EnemyRightToLeftMovementController : EnemyMovementController
{
    void FixedUpdate()
    {
        transform.position +=
        Vector3.left *
            GameManager.Instance.enemiesVelocity *
                GameManager.Instance.enemiesVelocityMultiplier *
                    Time.deltaTime;
        
    }


}

