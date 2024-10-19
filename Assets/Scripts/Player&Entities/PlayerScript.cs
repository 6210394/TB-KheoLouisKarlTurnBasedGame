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
