using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public CollisionManager()
    {
    }

    private void FixedUpdate()
    {
        var player = GameObject.FindObjectOfType<Player>();
        var enemies = GameObject.FindObjectsOfType<Enemy>();

        if (player!=null)
        {
            var playerCollisionController = player.collisionController;

            foreach (var enemy in enemies)
            {
                var enemyCollisionController = enemy.collisionController;
                if (playerCollisionController.shape.isColliding(enemyCollisionController.shape))
                {
                    // Add code for handling collision event
                    Debug.Log("Collision");
                }
            }
        }

    }
}

