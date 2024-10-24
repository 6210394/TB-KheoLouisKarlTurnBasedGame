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

    public virtual void StartTurn()
    {
        hasTurn = true;
        hasAttacked = false;
    }
    public virtual void EndTurn()
    {
        hasTurn = false;
        TurnManager.instance.CallStartNextEntityTurn();
    }
    public virtual void Init()
    {
        health = maxHealth;
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
        Destroy(gameObject);
    }

    public virtual void Attack(GameObject potentialTarget)
    {
        hasAttacked = true;
    }
}
