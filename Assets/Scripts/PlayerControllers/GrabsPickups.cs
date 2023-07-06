using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabsPickups : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var pickup = other.gameObject.GetComponent<Pickup>();
        if (pickup != null)
        {
            Debug.Log("Picked up " + pickup.pickupType);
            Destroy(other.gameObject);
            switch (pickup.pickupType)
            {
                case PickupType.Health:
                    HandleHealth();
                    break;
                case PickupType.Shield:

                    break;
                case PickupType.ExtraWeapon:

                    break;
                case PickupType.AttackSpeed:

                    break;
                default:
                    break;
            }
        }
    }

    void HandleHealth()
    {
        // TODO: Implement
    }
    void HandleShield()
    {
        // TODO: Implement
    }
    void HandleExtraWeapon()
    {
        // TODO: Implement
    }
    void HandleAttackSpeed()
    {
        // TODO: Implement
    }
}
