using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackRain : MonoBehaviour
{
    public furaoController1 furao1;
    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;
    public Transform shootPoint;
    public float shootForce;
    public float shootInterval;
    private float lastShootTime;

    private void Start()
    {
        lastShootTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (furao1.isAttacking)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (furao1.fliped)
        {
            furao1.isAttacking = false;
            GameObject projectile1 = Instantiate(projectilePrefab1, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
            if (rb1 != null)
            {
                rb1.AddForce(transform.right * shootForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            furao1.isAttacking = false;
            GameObject projectile2 = Instantiate(projectilePrefab2, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
            if (rb2 != null)
            {
                rb2.AddForce(transform.right * shootForce, ForceMode2D.Impulse);
            }
        }
    }
}
