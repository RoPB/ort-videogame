using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Reaction
{
    public ImageOrientation elementOrientation;
    public GameObject elementPrefab;
    public GameObject refDirection;
    public GameObject negativeRefDirection;
    public List<GameObject> triggerReferences;

    public Fire() : base("EnemyFire")
    {

    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        if(executionData.elapsed % 150 == 0)
        {
            foreach (var triggerRef in triggerReferences)
            {
                var elementToFire = (GameObject)Instantiate(elementPrefab);
                elementToFire.transform.position = triggerRef.transform.position;
                elementToFire.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                var elementToFireRigidBody = elementToFire.GetComponent<Rigidbody2D>();

                Vector2 forceDirection;

                if (refDirection != null)
                {
                    forceDirection = refDirection.GetComponent<Transform>().position - elementToFire.transform.position;
                }
                else if (negativeRefDirection != null)
                {
                    forceDirection = elementToFire.transform.position - negativeRefDirection.GetComponent<Transform>().position;

                }
                else
                {
                    forceDirection = collider.transform.position - elementToFire.transform.position;
                }

                forceDirection.Normalize();

                var rotation = Helper.getRotationToTarget(elementOrientation, transform, collider.transform.position);
                elementToFire.transform.rotation = rotation;
                elementToFireRigidBody.SetRotation(rotation);
                elementToFireRigidBody.AddForce(forceDirection * 1f, ForceMode2D.Impulse);
            }
        }

    }
}
