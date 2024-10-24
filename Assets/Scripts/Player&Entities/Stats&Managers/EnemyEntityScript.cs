using UnityEngine;
using System.Collections;

public class EnemyEntityScript : TurnBasedEntity
{
    public EnemyStats enemyStats;
    public AutomaticMovementScript automaticMovementScript;
    public EnemyStateMachine enemyStateMachine;

    public Animator animator;

    public EnemyEntityScript(EnemyStats enemyStats)
    {
        this.enemyStats = enemyStats;
    }

        // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            EndTurn();
        }
    }

    public override void Die()
    {
        GameManager.instance.AddScore(100);
        base.Die();
    }

    public override void Init()
    {
        automaticMovementScript = GetComponent<AutomaticMovementScript>();
        animator = GetComponentInChildren<Animator>();
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        AquireStats();
        base.Init();
    }

    public override void StartTurn()
    {
        base.StartTurn();
        automaticMovementScript.movementRemaining = maximumMovementDistance;
        automaticMovementScript.hasReachedTarget = true;
        automaticMovementScript.navMeshAgent.isStopped = false;
        enemyStateMachine.currentState = EnemyStateMachine.STATE.SEARCHING;
        enemyStateMachine.currentEvent = EnemyStateMachine.EVENT.ENTER;
        // Do enemy AI stuff here
    }

    public void AquireStats()
    {
        health = enemyStats.maxHealth;
        attackDamage = enemyStats.attackDamage;
        maximumMovementDistance = enemyStats.maximumMovementDistance;
        automaticMovementScript.movementRemaining = maximumMovementDistance;
        automaticMovementScript.speed = enemyStats.speed;
        initiative = enemyStats.initiative;
    }

    public override void Attack(GameObject potentialTarget)
    {
        base.Attack(potentialTarget);
        
        if(potentialTarget != null)
        {
            animator.SetTrigger("AttackTrigger");
            StartCoroutine(DealDamageAfterDelay(potentialTarget, animator.GetCurrentAnimatorStateInfo(0).length));
            
        }
    }

    private IEnumerator DealDamageAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.GetComponent<TurnBasedEntity>().TakeDamage(attackDamage);
    }

    /*
        public void MoveTo(Vector3 targetPosition)
        {
            navMeshAgent.SetDestination(targetPosition);
        }
        public Vector3 PerformRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit Point " + hit.point);
                return hit.point;
            }
            return new Vector3(0, 0, 0);
        }

        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                MoveTo(PerformRaycast());
            }
        }
    */
}
