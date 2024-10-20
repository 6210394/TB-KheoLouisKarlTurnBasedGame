using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicShooterStates : EnemyStateMachine
{
    public float attackRange = 2;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        target = PlayerScript.instance.gameObject;
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
                if(Vector3.Distance(transform.position, target.transform.position) < 10)
                {
                    SwitchToNextEvent(EVENT.EXIT);
                }
            
                break;
            }
            case EVENT.EXIT:
            {
                SwitchToNextState(STATE.CHASING);
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
                else if(enemyEntityScript.movementRemaining > 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemyEntityScript.movementSpeed * Time.deltaTime);
                }
                break;
            }

            case EVENT.EXIT:
            {
                SwitchToNextState(STATE.ATTACKING);
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
                Debug.Log("Attacking Enter");
                break;

            case EVENT.UPDATE:
                Debug.Log("Attacking Update");
                break;

            case EVENT.EXIT:
                Debug.Log("Attacking Exit");
                break;
        }
    }
    
}
