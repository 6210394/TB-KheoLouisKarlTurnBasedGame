using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityScript : TurnBasedEntity
{
    public EnemyStats enemyStats;

    public enum STATE
    {
        IDLE,
        PATROL,
        ATTACKING
    }

    public enum eventTypes
    {
        ENTER,
        UPDATE,
        EXIT
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTurn && Input.GetKeyDown(KeyCode.E))
        {
            EndTurn();
        }
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
        // Do enemy AI stuff here
    }
}
