using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //NOT needed anymore
    //public CollisionController collisionController;
    public EnemyMovementController enemyMovementController;

    private EnemyPooler _enemyPooler;

    public void SetOriginPool(EnemyPooler enemyPooler)
    {
        this._enemyPooler = enemyPooler;
    }

    public void ReturnToOriginPool()
    {
        this._enemyPooler.ReturnToPool(this.gameObject);
    }

    public void Collided()
    {
        this._enemyPooler.ReturnToPool(this.gameObject);
    }
}
