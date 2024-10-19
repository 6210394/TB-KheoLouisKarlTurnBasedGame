using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : TurnBasedEntity
{    
    public static PlayerScript instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space ) && hasTurn)
        {
            EndTurn();
        }
        if(Input.GetMouseButtonDown(0) && hasTurn && !hasAttacked)
        {
            Vector3 target = PerformRaycast();
            Attack(target);
        }

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

    override public void StartTurn()
    {
        Debug.Log("Player Turn");
        base.StartTurn();
        playerMovement.movementRemaining = maximumMovementDistance;
    }

    protected override void EndTurn()
    {
        playerMovement.movementRemaining = 0;
        hasAttacked = true;
        base.EndTurn();
    }

}
