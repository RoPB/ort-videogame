using System;
using UnityEngine;

public class EnemyTopToBottomMovementController : IEnemyMovementController
{
    private void FixedUpdate()
    {
        transform.position += Vector3.down * Time.deltaTime;

        if (this.IsOutOfScene())
        {
            var enemy = this.gameObject.GetComponent<Enemy>();
            enemy.ReturnToOriginPool();
        }

    }

    private bool IsOutOfScene()
    {
        return GameManager.Instance.IsLocatedAtTheBottomOfTheScene(this.transform.position, this.transform.localScale);
    }
}

