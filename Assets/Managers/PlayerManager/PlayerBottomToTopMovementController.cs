using UnityEngine;
using System.Collections;

public class PlayerBottomToTopMovementController : IPlayerMovementController
{
    private float _initalXPosition;
    private float _initialYPosition;
    public float playerVelocity = 1.0f;

    [SerializeField]
    [Range(0, 1)]
    public float maxRotation = 0;

    [SerializeField]
    [Range(0, 1)]
    public float maximumPlayerDisplacement = 0;

    [SerializeField]
    public bool showGizmos = false;

    public override void Init()
    {
        _initalXPosition = -0.101f;
        _initialYPosition = -0.824f;
        transform.position = new Vector3(_initalXPosition, _initialYPosition, 0);
    }

    private void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var gameManager = GameManager.Instance;

        var currentX = transform.position.x;
        var deltaX = x * playerVelocity * Time.deltaTime;
        var finalX = currentX + deltaX;
        finalX = gameManager.ClampXInSceneBounds(finalX, this.transform.localScale.x);
        RotatePlayer(x);

        var yPositionFactor = gameManager.ChangePlayerVelocity(y * Time.deltaTime);
        var finalY = _initialYPosition + yPositionFactor * maximumPlayerDisplacement;
        finalY = gameManager.ClampYInSceneBounds(finalY, this.transform.localScale.y);
        

        transform.position = new Vector3(finalX, finalY, 0);
    }

    private void RotatePlayer(float x)
    {
        var rotation = transform.rotation;
        rotation.z = Mathf.Clamp(x, -maxRotation, maxRotation);
        transform.rotation = rotation;
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

