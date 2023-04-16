using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private float _initalXPosition;
    private float _initialYPosition;
    public float playerVelocity = 1.0f;

    [SerializeField]
    [Range(0, 1)]
    public float maximumPlayerDisplacement = 0;

    [SerializeField]
    public bool showGizmos = false;

    public void Init()
    {
        _initalXPosition = -0.95f;
        _initialYPosition = -0.0014f;
        transform.position = new Vector3(_initalXPosition, _initialYPosition, 0);
    }

    private void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var gameManager = GameManager.Instance;

        var currentY = transform.position.y;
        var deltaY = y * playerVelocity * Time.deltaTime;
        var finalY = currentY + deltaY;
        finalY = gameManager.ClampYInSceneBounds(finalY, this.transform.localScale.y);

        var xPositionFactor = gameManager.ChangePlayerVelocity(x * Time.deltaTime);
        var finalX = _initalXPosition + xPositionFactor * maximumPlayerDisplacement;
        finalX = gameManager.ClampXInSceneBounds(finalX, this.transform.localScale.x);

        transform.position = new Vector3(finalX, finalY, 0);
    }

    void OnDrawGizmos()
    {
        if (showGizmos && transform.position.x != 0)
        {
            Gizmos.DrawLine(new Vector3(transform.position.x + maximumPlayerDisplacement, 100, 0), new Vector3(transform.position.x + maximumPlayerDisplacement, -100, 0));
            Gizmos.DrawLine(new Vector3(transform.position.x - maximumPlayerDisplacement, 100, 0), new Vector3(transform.position.x - maximumPlayerDisplacement, -100, 0));
        }
    }
}

