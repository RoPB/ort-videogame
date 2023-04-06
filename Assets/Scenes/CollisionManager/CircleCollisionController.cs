using System;
using UnityEngine;

public class CircleCollisionController : CollisionController
{
    [SerializeField]
    [Range(0, 20)]
    public float radius = 0;

    [SerializeField]
    [Range(-5, 5)]
    public float xOffset = 0;

    [SerializeField]
    [Range(-5, 5)]
    public float yOffset = 0;

    public bool showGizmos = false;

    override protected void CreateShape()
    {
        var radius = this.radius > 0 ? this.radius : transform.localScale.y / 2;

        shape = new Circle()
        {
            radius = radius,
            center = transform.position + new Vector3(xOffset * transform.localScale.x, -radius + yOffset * transform.localScale.y),

        };

    }

    override protected void UpdateShape()
    {
        var circle = (Circle)shape;
        circle.radius = this.radius > 0 ? this.radius : transform.localScale.y / 2;
        circle.center = transform.position + new Vector3(xOffset * transform.localScale.x, yOffset * transform.localScale.y);
    }

    public void OnDrawGizmos()
    {
        if (showGizmos)
        {
            var circle = (Circle)shape;
            Gizmos.DrawWireSphere(circle.center, circle.radius);
        }
    }
}

