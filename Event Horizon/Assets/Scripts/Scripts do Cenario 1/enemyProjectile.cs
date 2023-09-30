using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce;
    public aranhaCanhaoAnim aranhaCanhaoAnim;
    public spiderAlertColisor spiderAlertColisor;
    public float shootInterval;
    private float lastShootTime;

    private void Start()
    {
        lastShootTime = Time.time;
    }
    private void Update()
    {
        if (spiderAlertColisor.spiderAlert)
        {
            if (aranhaCanhaoAnim.isAttacking)
            {
                if(Time.time - lastShootTime >= shootInterval)
                {
                    Shoot();
                    lastShootTime = Time.time;
                }
            }
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(transform.right * shootForce, ForceMode2D.Impulse);
        }
    }
}
