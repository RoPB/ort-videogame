using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabsPickups : MonoBehaviour
{
    public GameObject shield;

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
                    HandleShield();
                    break;
                case PickupType.ExtraWeapon:
                    HandleExtraWeapon();
                    break;
                case PickupType.AttackSpeed:
                    HandleAttackSpeed();
                    break;
                default:
                    break;
            }
        }
    }

    void HandleHealth()
    {
        GameManager.Instance.UpdatePlayerLife(1);
    }

    void HandleShield()
    {
        shield.SetActive(true);
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
