using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : TurnBasedEntity
{    
    PlayerMovement playerMovement;
    ShootingScript shootingScript;

    public KeyCode endTurnKey;
    public GameObject endTurnButton;


    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Start()
    {
        EndTurnButtonDisplay(false);
    }

    public override void Init()
    {
        base.Init();
        shootingScript = GetComponent<ShootingScript>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(endTurnKey) && hasTurn)
        {
            EndTurn();
        }
        if(Input.GetMouseButtonDown(0) && hasTurn && !hasAttacked)
        {
            Attack(null);
        }

        //debug
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }
    }

    public override void Die()
    {
        GameManager.instance.LoseLife();
        base.Die();
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

    public override void Attack(GameObject potentialTarget)
    {
        base.Attack(potentialTarget);

        shootingScript.Shoot(PerformRaycast(), attackDamage, gameObject.tag);
        if(playerMovement.movementRemaining <= 0)
        {
            EndTurnButtonDisplay(true);
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
