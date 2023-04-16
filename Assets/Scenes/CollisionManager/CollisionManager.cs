using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private bool _canCollide = true;
    private float _collideTimeBox=0;

    public void Init()
    {
        _canCollide = true;
        _collideTimeBox = 0;
    }

    private void FixedUpdate()
    {
        if (_canCollide || _collideTimeBox > 2)
        {
            _canCollide = true;
            _collideTimeBox = 0;

            var player = GameObject.FindObjectOfType<Player>();
            var enemies = GameObject.FindObjectsOfType<Enemy>();

            if (player != null)
            {
                var playerCollisionController = player.collisionController;

                foreach (var enemy in enemies)
                {
                    var enemyCollisionController = enemy.collisionController;
                    if (playerCollisionController.shape.isColliding(enemyCollisionController.shape))
                    {
                        _canCollide = false;
                        GameManager.Instance.PlayerCollided(player);
                        break;
                    }
                }
            }
        }
        else
        {
            _collideTimeBox += Time.deltaTime;
        }
    }
}

