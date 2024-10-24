using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public enum STATE { SEARCHING, CHASING, ATTACKING, STOP }
    public enum EVENT { ENTER, UPDATE, EXIT }
    
    public STATE currentState;
    public EVENT currentEvent;
    protected STATE nextState;

    public EnemyStateMachine()
    {
        currentState = STATE.SEARCHING;
        currentEvent = EVENT.ENTER;
    }

    public EnemyEntityScript enemyEntityScript;
    public GameObject target;

    public virtual void Update()
    {
        if(enemyEntityScript.hasTurn)
        {
            RunStateMachine();
        }
        else
        {
            currentState = STATE.SEARCHING;
        }
    }

    public virtual void Init()
    {
        enemyEntityScript = GetComponent<EnemyEntityScript>();
    }

    public virtual void Searching()
    {
    }

    public virtual void Chasing()
    {
    }

    public virtual void Attacking()
    {
    }

    public virtual void Stop()
    {
        
    }

    protected void RunStateMachine()
    {
            switch (currentState)
            {
                case STATE.STOP:
                    Stop();
                    break;    

                case STATE.SEARCHING:
                    Searching();
                    break;

                case STATE.CHASING:
                    Chasing();
                    break;

                case STATE.ATTACKING:
                    Attacking();
                    break;
            }
    }

    protected void SwitchToNextState(STATE nextState)
    {
        currentState = nextState;
    }
    protected void SwitchToNextEvent(EVENT nextEvent)
    {
        currentEvent = nextEvent;
    }

    protected bool CheckIfObjectInSight(GameObject gameObject)
        {
            
                Vector3 directionToPlayer = gameObject.transform.position - transform.position;
                RaycastHit hit;

                if (Physics.Raycast(transform.position, directionToPlayer, out hit))
                {
                    if (hit.collider.gameObject.tag != "Player")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
        }

}
