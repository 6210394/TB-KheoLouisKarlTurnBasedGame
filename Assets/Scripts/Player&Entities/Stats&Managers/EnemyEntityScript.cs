using UnityEngine;
using UnityEngine.AI;

public class EnemyEntityScript : TurnBasedEntity
{
    public EnemyStats enemyStats;
    public AutomaticMovementScript automaticMovementScript;

    public EnemyEntityScript(EnemyStats enemyStats)
    {
        this.enemyStats = enemyStats;
    }

        // Start is called before the first frame update
    void Start()
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
        base.Die();
        Destroy(gameObject);
    }

    public override void Init()
    {
        automaticMovementScript = GetComponent<AutomaticMovementScript>();
        AquireStats();
        base.Init();
    }

    public override void StartTurn()
    {
        Debug.Log("Enemy Turn");
        base.StartTurn();
        automaticMovementScript.movementRemaining = maximumMovementDistance;
        automaticMovementScript.hasReachedTarget = true;
        automaticMovementScript.navMeshAgent.isStopped = false;
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
