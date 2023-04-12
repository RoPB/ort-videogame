using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovementController : MonoBehaviour
{
    private float initialXPosition;

    [SerializeField]
    private bool showGizmos = false;

    private void Start()
    {
        initialXPosition = transform.position.x;
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var gameManager = GameManager.Instance;

        var playerVelocity = gameManager.playerVelocity;

        var currentY = transform.position.y;
        var deltaY = y * playerVelocity * Time.deltaTime;
        var newY = currentY + deltaY;
        var finalY = gameManager.clampYInSceneBounds(newY, this.transform.localScale.y);

        var xPositionFactor = gameManager.changePlayerVelocity(x * Time.deltaTime);
        var finalX = initialXPosition + xPositionFactor * gameManager.maximumPlayerDisplacement;

        finalX = gameManager.clampXInSceneBounds(finalX, this.transform.localScale.x);

        transform.position = new Vector3(finalX, finalY, 0);

    }

    void OnDrawGizmos()
    {
        if (showGizmos && initialXPosition != 0)
        {
            Gizmos.DrawLine(new Vector3(initialXPosition + GameManager.Instance.maximumPlayerDisplacement, 100, 0), new Vector3(initialXPosition + GameManager.Instance.maximumPlayerDisplacement, -100, 0));
            Gizmos.DrawLine(new Vector3(initialXPosition - GameManager.Instance.maximumPlayerDisplacement, 100, 0), new Vector3(initialXPosition - GameManager.Instance.maximumPlayerDisplacement, -100, 0));
        }
    }
}

