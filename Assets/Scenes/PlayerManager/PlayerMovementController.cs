using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovementController : MonoBehaviour
{
    private float _initialXPosition;

    public float playerVelocity = 1.0f;

    [SerializeField]
    [Range(0, 1)]
    public float maximumPlayerDisplacement = 0;

    [SerializeField]
    public bool showGizmos = false;

    private void Start()
    {
        _initialXPosition = transform.position.x;
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var gameManager = GameManager.Instance;

        var currentY = transform.position.y;
        var deltaY = y * playerVelocity * Time.deltaTime;
        var finalY = currentY + deltaY;
        finalY = gameManager.ClampYInSceneBounds(finalY, this.transform.localScale.y);

        var xPositionFactor = gameManager.ChangePlayerVelocity(x * Time.deltaTime);
        var finalX = _initialXPosition + xPositionFactor * maximumPlayerDisplacement;
        finalX = gameManager.ClampXInSceneBounds(finalX, this.transform.localScale.x);

        transform.position = new Vector3(finalX, finalY, 0);

    }

    void OnDrawGizmos()
    {
        if (showGizmos && _initialXPosition != 0)
        {
            Gizmos.DrawLine(new Vector3(_initialXPosition + maximumPlayerDisplacement, 100, 0), new Vector3(_initialXPosition + maximumPlayerDisplacement, -100, 0));
            Gizmos.DrawLine(new Vector3(_initialXPosition - maximumPlayerDisplacement, 100, 0), new Vector3(_initialXPosition - maximumPlayerDisplacement, -100, 0));
        }
    }
}

