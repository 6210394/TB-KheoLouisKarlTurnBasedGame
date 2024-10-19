using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class TurnBasedEntity : MonoBehaviour
{
    public bool hasTurn = false;

    public int initiative = 0;
    int maxHealth = 1;
    public int health = 0;

    public int attackDamage = 1;

    public float maximumMovementDistance = 10f;
    public bool hasAttacked = true;


    ShootingScript shootingScript;

    public virtual void StartTurn()
    {
        hasTurn = true;
        hasAttacked = false;
    }
    protected virtual void EndTurn()
    {
        hasTurn = false;
        TurnManager.instance.CallStartNextEntityTurn();
    }
    public virtual void Init()
    {
        health = maxHealth;
        shootingScript = GetComponent<ShootingScript>();
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        TurnManager.instance.RemoveEntityFromEntityList(gameObject);
    }

    public virtual void Attack(Vector3 target)
    {
        shootingScript.Shoot(target, attackDamage, gameObject.tag);
        hasAttacked = true;
    }
}
