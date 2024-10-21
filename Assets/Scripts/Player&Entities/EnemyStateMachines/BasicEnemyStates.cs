using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicEnemyStates : EnemyStateMachine
{
    public float attackRange = 2;
    public float detectionRange = 20;

    public AutomaticMovementScript automaticMovementScript;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        target = PlayerScript.instance.gameObject;
        automaticMovementScript = GetComponent<AutomaticMovementScript>();

        attackRange = enemyEntityScript.enemyStats.attackRange;
        detectionRange = enemyEntityScript.enemyStats.detectionRange;
    }

    public override void Searching()
    {
        base.Searching();
        switch(currentEvent)
        {
            case EVENT.ENTER:
            {
                SwitchToNextEvent(EVENT.UPDATE);
                break;
            }

            case EVENT.UPDATE:
            {
                if(Vector3.Distance(transform.position, target.transform.position) <= detectionRange)
                {
                    SwitchToNextEvent(EVENT.EXIT);
                }
                else
                {
                    
                }
            
                break;
            }
            case EVENT.EXIT:
            {
                SwitchToNextState(STATE.CHASING);
                SwitchToNextEvent(EVENT.ENTER);
                break;
            }
        }
    }

    public override void Chasing()
    {
        base.Chasing();
        switch(currentEvent)
        {
            case EVENT.ENTER:
            {
                SwitchToNextEvent(EVENT.UPDATE);
                break;
            }

            case EVENT.UPDATE:
            {
                if(Vector3.Distance(transform.position, target.transform.position) < attackRange)
                {
                    SwitchToNextEvent(EVENT.EXIT);
                }
                if (automaticMovementScript.movementRemaining > 0)
                {
                    automaticMovementScript.MoveWithNavMesh(target.transform.position);
                }
                else
                {
                    SwitchToNextEvent(EVENT.EXIT);
                }
            
                break;
            }

            case EVENT.EXIT:
            {
                if(Vector3.Distance(transform.position, target.transform.position) < attackRange)
                {
                    SwitchToNextState(STATE.ATTACKING);
                    SwitchToNextEvent(EVENT.ENTER);
                }
                else
                {
                    enemyEntityScript.EndTurn();
                    SwitchToNextState(STATE.SEARCHING);
                    SwitchToNextEvent(EVENT.ENTER);
                }
                break;
            }    
        }
    }

    public override void Attacking()
    {
        base.Attacking();
        switch(currentEvent)
        {
            case EVENT.ENTER:
                currentEvent = EVENT.UPDATE;
                break;

            case EVENT.UPDATE:
            {
                Debug.Log("Attacking");
                enemyEntityScript.Attack(target.transform.position);
                currentEvent = EVENT.EXIT;
                break;
            }

            case EVENT.EXIT:
            {
                SwitchToNextEvent(EVENT.ENTER);
                SwitchToNextState(STATE.SEARCHING);
                enemyEntityScript.EndTurn();
                break;
            }
                
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
}
