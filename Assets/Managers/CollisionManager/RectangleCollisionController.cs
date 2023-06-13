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
            center = transform.position + new Vector3(xOffset * transform.localScale.x, yOffset * transform.localScale.y),
            height = this.height > 0 ? this.height : transform.localScale.y,
            width = this.width > 0 ? this.width : transform.localScale.x,

        };

    }

    override protected void UpdateShape()
    {
        var rectangle = (Rectangle)shape;
        rectangle.center = transform.position + new Vector3(xOffset * transform.localScale.x, yOffset * transform.localScale.y);
        rectangle.height = this.height > 0 ? this.height : transform.localScale.y;
        rectangle.width = this.width > 0 ? this.width : transform.localScale.x;
    }

    public void OnDrawGizmos()
    {
        if (showGizmos && shape != null)
        {
            var rectangle = (Rectangle)shape;
            Gizmos.DrawWireCube(rectangle.center, new Vector3(rectangle.width, rectangle.height));
        }
    }
}


