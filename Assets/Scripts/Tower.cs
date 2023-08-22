using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("General")]  
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 10f; // скорость атаки
    private float fireCountdown = 0f; // перерыв между атаками


    [Header("Unity setup fields")]
    public string enemyTag = "Enemy";

    public Transform firePoint;

    private bool isSupported = false;
    private bool stopSupport = false;

    public void Support()
    {
        isSupported = true;
    }

    public void StopSupport()
    {
        stopSupport = true;
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // вызов метода UpdateTarget каждые пол секунды
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (isSupported && !stopSupport)
        {
            range += 0.5f; // увеличение радиуса стрельбы
            fireRate *= 1.2f; // ускорение стрельбы на 20%
            isSupported = false;
        }
        if (stopSupport && !isSupported)
        {
            range -= 0.5f;
            fireRate /= 1.2f;
            stopSupport = false;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
