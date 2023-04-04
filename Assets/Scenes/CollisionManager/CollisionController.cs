using UnityEngine;
using System.Collections;
using System;
using UnityEditor.U2D.Path;

public class CollisionController : MonoBehaviour
{
    protected Shape shape;

    public void Start()
    {
        CreateShape();
    }

    public void Update()
    {
        UpdateShape();
    }

    protected virtual void CreateShape()
    {

    }

    protected virtual void UpdateShape()
    {

    }

}