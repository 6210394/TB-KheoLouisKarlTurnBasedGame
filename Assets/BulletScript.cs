using UnityEngine;

public class BulletScript : MonoBehaviour
{
    AutomaticMovementScript automaticMovementScript;
    public string tagToIgnore;
    public int bulletDamage; 
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet hit " + other.name);
        if (other.GetComponent<TurnBasedEntity>() && other.tag != tagToIgnore)
        {
            Debug.Log("Bullet hit " + other.name);
            other.GetComponent<TurnBasedEntity>().TakeDamage(bulletDamage);
        }
    }

    void Start()
    {
        automaticMovementScript = GetComponent<AutomaticMovementScript>();
    }

    void Update()
    {
        automaticMovementScript.MoveToTarget();
    }
}
