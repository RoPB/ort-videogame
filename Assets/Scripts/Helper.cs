using System;
using UnityEngine;

public enum ImageOrientation { Up, Down, Left, Right};

public static class Helper
{
	public static Quaternion getRotationToTarget(ImageOrientation imageOrientation, Transform currentTransform, Vector3 targetPosition)
	{
        Vector3 direction = targetPosition - currentTransform.position;

        Quaternion targetRotation = Quaternion.identity;

        switch (imageOrientation)
        {
            case ImageOrientation.Up:
                targetRotation = (direction.x > 0 ?
                    Quaternion.AngleAxis(90, Vector3.back) : Quaternion.AngleAxis(90, Vector3.forward)) * Quaternion.LookRotation(direction);
                break;
            case ImageOrientation.Down:
                targetRotation = (direction.x < 0 ?
                    Quaternion.AngleAxis(90, Vector3.back) : Quaternion.AngleAxis(90, Vector3.forward)) * Quaternion.LookRotation(direction);
                break;
            default:
                throw new Exception("NOT DEFINED");
        }
     

        float rotationSpeed = 360f;

        var newRotation = Quaternion.RotateTowards(currentTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        return newRotation;
    }
}

