using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovementController : MonoBehaviour
{
    private float initialXPosition;

    private void Start()
    {
        initialXPosition = transform.position.x;
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var playerVelocity = GameManager.Instance.playerVelocity;

        var currentY = transform.position.y;
        var deltaY = y * playerVelocity * Time.deltaTime;
        var newY = currentY + deltaY;
        var finalY = GameManager.Instance.clampYInSceneBounds(newY, this.transform.localScale.y);

        var currentX = transform.position.x;
        var deltaX = x * playerVelocity * Time.deltaTime;
        var newX = currentX + deltaX;
        var finalX = Mathf.Clamp(
            newX,
            initialXPosition - GameManager.Instance.maximumPlayerDisplacement,
            initialXPosition + GameManager.Instance.maximumPlayerDisplacement
        );
        finalX = GameManager.Instance.clampXInSceneBounds(finalX, this.transform.localScale.x);

        transform.position = new Vector3(finalX, finalY, 0);

        GameManager.Instance.changePlayerVelocity(x*Time.deltaTime);
    }

}

