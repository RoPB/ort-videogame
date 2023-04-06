using System;
using UnityEngine;

public class RectangleCollisionController : CollisionController
{
    [SerializeField]
    [Range(0, 20)]
    public float width = 0;

    [SerializeField]
    [Range(0, 20)]
    public float height = 0;

    [SerializeField]
    [Range(-5, 5)]
    public float xOffset = 0;

    [SerializeField]
    [Range(-5, 5)]
    public float yOffset = 0;

    public bool showGizmos = false;


    override protected void CreateShape()
    {
        shape = new Rectangle()
        {
            coordinates = transform.position + new Vector3(xOffset * transform.localScale.x, yOffset * transform.localScale.y),
            height = this.height > 0 ? this.height : transform.localScale.y,
            width = this.width > 0 ? this.width : transform.localScale.x,

        };

    }

    override protected void UpdateShape()
    {
        var rectangle = (Rectangle)shape;
        rectangle.coordinates = transform.position + new Vector3(xOffset * transform.localScale.x, yOffset * transform.localScale.y);
        rectangle.height = this.height > 0 ? this.height : transform.localScale.y;
        rectangle.width = this.width > 0 ? this.width : transform.localScale.x;
    }

    public void OnDrawGizmos()
    {
        if (showGizmos)
        {
            var rectangle = (Rectangle)shape;
            var center = rectangle.coordinates + new Vector3(rectangle.width / 2, -rectangle.height / 2);
            Gizmos.DrawWireCube(rectangle.coordinates, new Vector3(rectangle.width, rectangle.height));
        }
    }
}


