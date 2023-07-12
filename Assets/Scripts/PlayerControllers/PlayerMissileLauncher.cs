using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileLauncher : MonoBehaviour
{
    public GameObject missilePrefab;

    public GameObject missileChild;
    [SerializeField]
    [Range(0, 10)]
    private float bulletSpeed = 5;

    void Shoot()
    {
        var missile = Instantiate(missilePrefab, missileChild.transform.position, Quaternion.identity);
        missile.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
    }

    void Update()
    {
        missileChild.SetActive(GetComponentInParent<GunGroup>().CanShoot);
    }
}
