using UnityEngine;
using UnityEngine.AI;

public class AutomaticMovementScript : MonoBehaviour
{
    public Vector3 target;
    public GameObject targetObject;
    public float speed;

    public float movementRemaining;
    public NavMeshAgent navMeshAgent;


    public bool destroyable = false;
    public float lifeTime = 1f;

    void Start()
    {
        Init();
    }

    // Update is called once per frame

    public void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        if (destroyable)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    public void MoveWithNavMesh(Vector3 target)
    {
        if (movementRemaining <= 0)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        navMeshAgent.SetDestination(target);

        Vector3 movement = navMeshAgent.velocity.normalized;
        movementRemaining -= movement.magnitude * speed * Time.deltaTime;   

        if (movementRemaining <= 0)
        {
            navMeshAgent.isStopped = true;
            return;
        } 
    }

    public void Init()
    {
        Debug.Log("Init");
        if(GetComponent<NavMeshAgent>() != null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        navMeshAgent.speed = speed;
    }
}
