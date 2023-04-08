using System;
using UnityEngine;

public abstract class Shape
{


    public abstract bool isColliding(Shape otherShape);

    protected bool checkCircleRectangleColliding(Circle circle, Rectangle rectangle)
    {
        return false;
    }

    protected bool checkCircleCircleColliding(Circle circle1, Circle circle2)
    {
        var dx = circle1.center.x - circle2.center.x;
        var dy = circle1.center.y - circle2.center.y;
        var distanceSQ = dx * dx + dy * dy;
        var radiusSum = circle1.radius + circle2.radius;
        return distanceSQ <= radiusSum * radiusSum;
    }

    protected bool checkRectangleRectangleColliding(Rectangle rectangle1, Rectangle rectangle2)
    {
        return false;
    }

}

public class Circle : Shape
{
    public Vector3 center;
    public float radius;

    override public bool isColliding(Shape otherShape)
    {
        if (otherShape is Circle)
        {
            return this.checkCircleCircleColliding(this, (Circle)otherShape);
        }
        else if (otherShape is Rectangle)
        {
            return this.checkCircleRectangleColliding(this, (Rectangle)otherShape);
        }

        throw new Exception($"isColliding: {otherShape.GetType()} Shape not supported");
    }

}

public class Rectangle : Shape
{
    public Vector3 coordinates;
    public float height;
    public float width;

    override public bool isColliding(Shape otherShape)
    {
        if (otherShape is Circle)
        {
            return this.checkCircleRectangleColliding((Circle)otherShape, this);
        }
        else if (otherShape is Rectangle)
        {
            return this.checkRectangleRectangleColliding(this, (Rectangle)otherShape);
        }

        throw new Exception($"isColliding: {otherShape.GetType()} Shape not supported");
    }
}
