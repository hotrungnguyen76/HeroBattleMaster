using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 originScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            originScale.x * transform.localScale.x > 0 ? 1 : -1,
            originScale.y,
            originScale.z
            );

        if (projectile.transform.localScale.x < 0)
        {
            Vector3 currentRotation = projectile.transform.rotation.eulerAngles;
            projectile.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, -currentRotation.z);

            SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();
            //if (spriteRenderer != null)
            //{
            //    spriteRenderer.flipX = true;
            //}

        }
    }
}
