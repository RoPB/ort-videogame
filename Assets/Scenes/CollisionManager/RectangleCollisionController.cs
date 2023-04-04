using System;
using UnityEngine;

public class RectangleCollisionController : CollisionController
{

    override protected void CreateShape()
    {
        shape = new Rectangle()
        {
            coordinates = transform.position,
            height = transform.localScale.y,
            width = transform.localScale.x,

        };

    }

    override protected void UpdateShape()
    {
        var rectangle = (Rectangle)shape;
        rectangle.coordinates = transform.position;

    }
}


