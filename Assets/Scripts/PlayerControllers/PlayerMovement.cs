using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float playerVelocity = 1.0f;

    [SerializeField]
    [Range(0, 1)]
    public float maxZRotation = 0;
    [SerializeField]
    [Range(0, 1)]
    public float maxYRotation = 0;

    private Rigidbody2D _rigidbody2D;

    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D not found");
        }
        // TODO: Set initial position
    }

    private void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        RotatePlayer(x);


        _rigidbody2D.velocity = new Vector2(x * playerVelocity, y * playerVelocity);
    }

    private void RotatePlayer(float x)
    {
        var rotation = transform.rotation;
        rotation.z = Mathf.Clamp(-x, -maxZRotation, maxZRotation);
        rotation.y = Mathf.Clamp(-x, -maxYRotation, maxYRotation);
        transform.rotation = rotation;
    }
}

