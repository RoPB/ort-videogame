using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.Space;
    public GameObject bulletPrefab;

    [SerializeField]
    [Range(0, 1)]
    private float attackSpeed = 0.5f;

    [SerializeField]
    [Range(0, 10)]
    private float bulletSpeed = 5;

    private float _lastAttackTime = 0;

    private bool isAnimating = false;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shootKey) && Time.time - _lastAttackTime > attackSpeed)
        {
            _lastAttackTime = Time.time;
            isAnimating = true;
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.layer = gameObject.layer;
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
            GetComponent<AudioSource>().Play();
        }

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
