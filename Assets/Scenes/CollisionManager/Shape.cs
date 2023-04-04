using System;
using UnityEngine;

public abstract class Shape {}

public class Circle : Shape
{
    public Vector3 center;
    public float radius;
}

public class Rectangle : Shape
{
    public Vector3 coordinates;
    public float height;
    public float width;
}
