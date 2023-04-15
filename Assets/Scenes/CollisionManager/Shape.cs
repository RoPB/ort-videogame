using System;
using UnityEngine;

public abstract class Shape
{
    public abstract bool isColliding(Shape otherShape);

    protected bool checkCircleRectangleColliding(Circle circle, Rectangle rectangle)
    {
        var collisionX = circle.center.x;
        var collisionY = circle.center.y;

        var rectangleOrigin = rectangle.origin();

        if (circle.center.x < rectangleOrigin.x)//left edge?
            collisionX = rectangleOrigin.x; 
        else if (circle.center.x > rectangleOrigin.x + rectangle.width)//right edge?
            collisionX = rectangleOrigin.x + rectangle.width;

        if (circle.center.y < rectangleOrigin.y)//top edge?
            collisionY = rectangleOrigin.y;
        else if (circle.center.y > rectangleOrigin.y + rectangle.height)//bottom edge?
            collisionY = rectangleOrigin.y + rectangle.height;

        var dx = circle.center.x - collisionX;
        var dy = circle.center.y - collisionY;
        var distance = Math.Sqrt((dx * dx) + (dy * dy));

        return distance <= circle.radius;
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
        else
        {
            //TODO: xq entra aca?
            Debug.Log($"isColliding: {otherShape?.GetType()} Shape not supported");
            return false;
            //throw new Exception($"isColliding: {otherShape.GetType()} Shape not supported");
        }

    }

}

public class Rectangle : Shape
{
    public Vector3 center;
    public float height;
    public float width;

    public Vector3 origin() => new Vector3(this.center.x - this.width / 2, this.center.y - this.height / 2,0);

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
