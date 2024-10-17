using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityScript : TurnBasedEntity
{
    public EnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init()
    {
        health = enemyStats.maxHealth;
        attackDamage = enemyStats.attackDamage;
        maximumMovementDistance = enemyStats.maximumMovementDistance;
        initiative = enemyStats.initiative;
        base.Init();
    }
}
