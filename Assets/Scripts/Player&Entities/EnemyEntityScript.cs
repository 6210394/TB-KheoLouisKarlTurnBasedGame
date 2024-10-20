using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyEntityScript : TurnBasedEntity
{
    public EnemyStats enemyStats;
    public EnemyStateMachine enemyStateMachine;

    public EnemyEntityScript(EnemyStats enemyStats)
    {
        this.enemyStats = enemyStats;
    }

    public float movementRemaining;
    public float movementSpeed;

        // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void Init()
    {
        health = enemyStats.maxHealth;
        attackDamage = enemyStats.attackDamage;
        maximumMovementDistance = enemyStats.maximumMovementDistance;
        initiative = enemyStats.initiative;
        base.Init();
    }

    public override void StartTurn()
    {
        Debug.Log("Enemy Turn");
        base.StartTurn();
        movementRemaining = maximumMovementDistance;
        // Do enemy AI stuff here
    }
}
