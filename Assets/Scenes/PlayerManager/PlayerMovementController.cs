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

        var deltaY = y * playerVelocity * Time.deltaTime;
        transform.position += new Vector3(0, deltaY, 0);

        var currentX = transform.position.x;
        var deltaX = x * playerVelocity * Time.deltaTime;
        var newX = currentX + deltaX;

        var finalX = Mathf.Clamp(
            newX,
            initialXPosition - GameManager.Instance.maximumPlayerDisplacement,
            initialXPosition + GameManager.Instance.maximumPlayerDisplacement
        );
        
        transform.position = new Vector3(finalX, transform.position.y, 0);

        GameManager.Instance.changePlayerVelocity(x*Time.deltaTime);
    }

}

