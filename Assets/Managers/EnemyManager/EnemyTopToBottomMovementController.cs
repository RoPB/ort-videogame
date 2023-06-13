using System;
using UnityEngine;

public class EnemyTopToBottomMovementController : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position += Vector3.down * Time.deltaTime;

        if (this.IsOutOfScene())
        {
            GameManager.Instance.enemyPooler.ReturnToPool(this.gameObject);
        }

    }

    private bool IsOutOfScene()
    {
        return GameManager.Instance.IsLocatedAtTheBottomOfTheScene(this.transform.position, this.transform.localScale);
    }
}

