using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    //ATTACH THIS SCRIPT TO THE GUN OBJECT

    public GameObject bulletPrefab;
    public Vector3 spawnOriginOffset;


    public void Shoot(Vector3 target, int damageToPassOnToBullet, string tagToIgnore)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * spawnOriginOffset, transform.rotation);
        bullet.GetComponent<MoveToScript>().target = target;
        bullet.GetComponent<BulletScript>().bulletDamage = damageToPassOnToBullet;
        bullet.GetComponent<BulletScript>().tagToIgnore = tagToIgnore;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * spawnOriginOffset, 0.1f);
    }
}
