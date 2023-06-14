using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //NOT needed anymore
    //public CollisionController collisionController;
    public IPlayerMovementController playerMovementController;

    public float playerHeight => this.transform.localScale.x;

    public float playerWidth => this.transform.localScale.y;

    private Color _originalColor;

    private void Start()
    {
        _originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
    }

    public void Init()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = _originalColor;
    }

    public void Collided(PlayerLifes playerLifes)
    {
        var currentColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        var alpha = (float)playerLifes.currentLifes / (float)playerLifes.maxLifes;
        var newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        if (alpha == 0)
            newColor = new Color(255, 0, 0);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = newColor;
    }
}