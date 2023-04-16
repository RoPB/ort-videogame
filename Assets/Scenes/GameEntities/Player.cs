using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CollisionController collisionController;
    public PlayerMovementController playerMovementController;

    public void Collided(PlayerLifes playerLifes)
    {
        var currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        var alpha = (float)playerLifes.currentLifes/(float)playerLifes.maxLifes;
        var newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        if (alpha == 0)
            newColor = new Color(255, 0, 0);
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }
}
