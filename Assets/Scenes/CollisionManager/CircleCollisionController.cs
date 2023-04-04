using System;
using UnityEngine;

public class CircleCollisionController : CollisionController
{
    [SerializeField]
    [Range(0,20)]
    public float radius = 0;

    override protected void CreateShape()
    {
        var radius = this.radius > 0 ? this.radius : transform.localScale.y / 2;

        shape = new Circle()
        {
            radius = radius,
            center = transform.position + new Vector3(radius, radius)

        };

    }

    override protected void UpdateShape()
    {
        var circle = (Circle)shape;
        circle.center = transform.position + new Vector3(circle.radius, circle.radius);
    }

    //public void OnDrawGizmos()
    //{
    //    if(shape!=null)
    //        Gizmos.DrawSphere(((Circle)shape).center, ((Circle)shape).radius);
    //}
}

