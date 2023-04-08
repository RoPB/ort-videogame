using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public CollisionManager()
    {
    }

    private void FixedUpdate()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (players.Length > 0)
        {
            var player = players[0];
            var playerCollisionController = player.GetComponent<CollisionController>();

            foreach (var enemy in enemies)
            {
                var enemyCollisionController = enemy.GetComponent<CollisionController>();
                if (playerCollisionController.shape.isColliding(enemyCollisionController.shape))
                {
                    // Add code for handling collision event
                    Debug.Log("Collision");
                }
            }
        }

    }
}

