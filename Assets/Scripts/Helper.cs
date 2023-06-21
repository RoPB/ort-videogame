using System;
using UnityEngine;

public static class Helper
{
	public static Quaternion rotateToTarget(Vector3 targetPosition, Transform currentTransform)
	{
        Quaternion targetRotation = Quaternion.LookRotation(currentTransform.position, targetPosition);

        float rotationSpeed = 200f;

        var newRotation = Quaternion.RotateTowards(currentTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        return newRotation;
    }
}

