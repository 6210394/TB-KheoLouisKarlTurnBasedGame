using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public int initiative = 0;
    public int maxHealth = 1;
    public int attackDamage = 1;
    public float attackRange = 2;
    public float detectionRange = 20;
    public float maximumMovementDistance = 5f;
    public float speed;
}
