using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string tagToIgnore;
    public int bulletDamage; 
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TurnBasedEntity>() && other.tag != tagToIgnore)
        {
            Debug.Log("Bullet hit " + other.name);
            other.GetComponent<TurnBasedEntity>().TakeDamage(bulletDamage);
        }
    }
}
