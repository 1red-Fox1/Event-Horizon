using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackRain : MonoBehaviour
{
    public furaoController furao;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce;
    public float shootInterval;
    private float lastShootTime;

    private void Start()
    {
        lastShootTime = Time.time;
    }

    private void Update()
    {
        if (furao.isAttacking)
        {
            if (Time.time - lastShootTime >= shootInterval)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 shootDirection = transform.right; // A direção em que você deseja atirar (direita no exemplo).

        // Mova o objeto manualmente na direção desejada com a força desejada.
        projectile.transform.Translate(shootDirection * shootForce * Time.deltaTime);
    }
}
