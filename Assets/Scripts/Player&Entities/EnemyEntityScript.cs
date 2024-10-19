using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyEntityScript : TurnBasedEntity
{
    public EnemyStats enemyStats;
    GameObject player;

    public enum STATE { HIDING, SEARCHING, APPROACHING, ATTACKING }
    public enum EVENT { ENTER, UPDATE, EXIT }

    public STATE currentState;
    protected EVENT currentEvent;
    protected STATE nextState;

    public EnemyEntityScript()
    {
        currentEvent = EVENT.ENTER;
        currentState = STATE.SEARCHING;
    }

    public EnemyEntityScript(EnemyStats enemyStats)
    {
        this.enemyStats = enemyStats;
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

    public override void Die()
    {
        Destroy(gameObject);
    }

    public void HandleStates()
    {
        switch(currentState)
        {
            case STATE.HIDING:
                Hiding();
                break;

            case STATE.SEARCHING:
                Searching();
                break;

            case STATE.APPROACHING:
                Approaching();
                break;

            case STATE.ATTACKING:  
                Attacking();
                break;
        }
    }

    public void Hiding()
    {
        Debug.Log("Hiding");
        switch(currentEvent)
        {
            case EVENT.ENTER:
                Debug.Log("Hiding Enter");
                if(CheckIfPlayerInSight())
                {
                    currentState = STATE.ATTACKING;
                    currentEvent = EVENT.ENTER;
                }

                break;

            case EVENT.UPDATE:
                Debug.Log("Hiding Update");
                break;

            case EVENT.EXIT:
                Debug.Log("Hiding Exit");
                break;
        }
    }

    public void Searching()
    {
        Debug.Log("Searching");
    }

    public void Approaching()
    {
        Debug.Log("Approaching");
    }

    public void Attacking()
    {
        Debug.Log("Attacking");
    }


    public virtual void Enter() { }
    public virtual void UpdateState() { }
    public virtual void Exit()
    {
        HandleStates();
    }

    bool CheckIfPlayerInSight()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, directionToPlayer, out hit))
            {
                if (hit.collider.gameObject.tag != "Player")
                {
                    Debug.Log("Raycast hit something other than the player: " + hit.collider.gameObject.name);
                    return false;
                }
                else
                {
                    Debug.Log("Raycast hit the player.");
                    return true;
                }
            }
        }
        return false;
    }

    public override void Init()
    {
        health = enemyStats.maxHealth;
        attackDamage = enemyStats.attackDamage;
        maximumMovementDistance = enemyStats.maximumMovementDistance;
        initiative = enemyStats.initiative;
        base.Init();
        player = GameObject.FindWithTag("Player");
    }

    public override void StartTurn()
    {
        Debug.Log("Enemy Turn");
        base.StartTurn();
        // Do enemy AI stuff here
    }
}
