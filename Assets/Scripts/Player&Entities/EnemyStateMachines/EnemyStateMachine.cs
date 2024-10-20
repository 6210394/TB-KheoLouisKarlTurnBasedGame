using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public enum STATE { SEARCHING, CHASING, ATTACKING }
    public enum EVENT { ENTER, UPDATE, EXIT }
    
    public STATE currentState;
    protected EVENT currentEvent;
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
        RunStateMachine();
    }

    public virtual void Init()
    {
        enemyEntityScript = GetComponent<EnemyEntityScript>();
    }

    public virtual void Searching()
    {
        Debug.Log("Searching");
    }

    public virtual void Chasing()
    {
        Debug.Log("Approaching");
    }

    public virtual void Attacking()
    {
        Debug.Log("Attacking");
    }

    protected void RunStateMachine()
    {
        switch (currentState)
        {
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
                        Debug.Log("Raycast hit something other than the player: " + hit.collider.gameObject.name);
                        return false;
                    }
                    else
                    {
                        Debug.Log("Raycast hit the player.");
                        return true;
                    }
                }
                return false;
        }

}
