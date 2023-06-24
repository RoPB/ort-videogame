using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyMovementController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    public float rotateFactor;
    private Vector3 rotateAngle;

    protected void RotateEnemy()
    {
        if(rotateFactor!=0)
            transform.RotateAround(transform.position, Vector3.forward, rotateFactor*10);
    }
}

