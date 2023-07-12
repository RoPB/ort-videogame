using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{

    public GameObject bulletPrefab;

    [SerializeField]
    [Range(0, 10)]
    private float bulletSpeed = 5;



    private bool isAnimating = false;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    void Shoot()
    {
        isAnimating = true;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
    }

    void Update()
    {
        if (isAnimating)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale * 1.5f, 0.1f);
            if (transform.localScale.x > initialScale.x * 1.4f)
            {
                isAnimating = false;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, 0.1f);
        }
    }
}
