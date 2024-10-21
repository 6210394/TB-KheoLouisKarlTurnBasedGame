using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    ShootingScript shootingScript;

    public KeyCode endTurnKey;
    public GameObject endTurnButton;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        shootingScript = GetComponent<ShootingScript>();
        playerMovement = GetComponent<PlayerMovement>();
        EndTurnButtonDisplay(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(endTurnKey) && hasTurn)
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
        EndTurnButtonDisplay(false);

        base.StartTurn();
        playerMovement.movementRemaining = maximumMovementDistance;
    }

    public override void EndTurn()
    {
        playerMovement.movementRemaining = 0;
        hasAttacked = true;
        base.EndTurn();
        EndTurnButtonDisplay(false);
    }

    public override void Attack(Vector3 target)
    {
        shootingScript.Shoot(target, attackDamage, gameObject.tag);
        base.Attack(target);
        if(playerMovement.movementRemaining <= 0)
        {
            EndTurnButtonDisplay(true);
            Debug.Log("Out of movement");
            return;
        }
    }

    public void EndTurnButtonDisplay(bool display)
    {
        if(endTurnButton == null)
        {
            endTurnButton = GameObject.Find("EndTurnButton");
        }

        switch(display)
        {
            case true:
                endTurnButton.GetComponent<Button>().enabled = true;
                endTurnButton.GetComponent<Image>().enabled = true;
                endTurnButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                break;
            
            case false:
                endTurnButton.GetComponent<Button>().enabled = false;
                endTurnButton.GetComponent<Image>().enabled = false;
                endTurnButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                break;
        }
    }

}
