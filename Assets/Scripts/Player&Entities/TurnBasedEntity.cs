using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class TurnBasedEntity : MonoBehaviour
{
    public int initiative = 0;
    int maxHealth = 1;
    public int health = 0;

    public int attackDamage = 1;

    public float maximumMovementDistance = 10f;
    public bool hasAttacked = true;

    public virtual void StartTurn()
    {
        hasAttacked = false;
    }

    public virtual void Init()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
