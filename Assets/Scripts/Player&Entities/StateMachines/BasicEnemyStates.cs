using UnityEngine;


public class BasicEnemyStates : EnemyStateMachine
{
    public float attackRange = 2;
    public float detectionRange = 20;

    public AutomaticMovementScript automaticMovementScript;

    void Start()
    {
        target = GameManager.instance.playerReference;
        GameManager.instance.onPlayerSpawned += GetNewPlayerReference;
    }

    void OnDestroy()
    {
        GameManager.instance.onPlayerSpawned -= GetNewPlayerReference;
    }

    void Awake()
    {
        Init();
    }

    public void GetNewPlayerReference()
    {
        target = GameManager.instance.playerReference;
    }

    public override void Init()
    {
        base.Init();
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
                    automaticMovementScript.MoveRandomly();
                    if(automaticMovementScript.movementRemaining <= 0)
                    {
                        SwitchToNextEvent(EVENT.EXIT);
                    }
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
                automaticMovementScript.navMeshAgent.isStopped = true;
                currentEvent = EVENT.UPDATE;
                break;

            case EVENT.UPDATE:
            {
                enemyEntityScript.Attack(target);
                currentEvent = EVENT.EXIT;
                break;
            }

            case EVENT.EXIT:
            {
                if(enemyEntityScript.hasTurn)
                {
                    enemyEntityScript.EndTurn();
                }
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
