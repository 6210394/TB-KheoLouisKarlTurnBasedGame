using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    TurnBasedEntity turnBasedEntity;

    // Start is called before the first frame update
    void Start()
    {
        turnBasedEntity = GameObject.Find("Player").GetComponent<TurnBasedEntity>();
    }

    public void EndTurn()
    {
        turnBasedEntity.EndTurn();
    }
}
