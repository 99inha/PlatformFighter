using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;

    public void shoot()
    {
        GameObject shot = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        shot.tag = transform.tag;
    }
}
