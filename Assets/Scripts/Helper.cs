using System;
using UnityEngine;

public static class Helper
{
	public static Quaternion getRotationToTarget(Transform currentTransform, Vector3 targetPosition)
	{
        Vector3 direction = targetPosition - currentTransform.position;

        Quaternion targetRotation = (direction.z > 0 ?
                    Quaternion.AngleAxis(90, Vector3.back) : Quaternion.AngleAxis(90, Vector3.back)) * Quaternion.LookRotation(direction);


        float rotationSpeed = 360f;

        var newRotation = Quaternion.RotateTowards(currentTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        return newRotation;
    }
}

