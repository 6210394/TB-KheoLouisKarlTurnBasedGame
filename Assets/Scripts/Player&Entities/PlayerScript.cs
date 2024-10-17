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

    void Init()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartTurn();
        }
    }

    override public void StartTurn()
    {
        base.StartTurn();
        playerMovement.movementRemaining = maximumMovementDistance;
    }

}
