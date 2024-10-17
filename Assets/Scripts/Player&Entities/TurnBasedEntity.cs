using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedEntity : MonoBehaviour
{
    public float maximumMovementDistance = 10f;
    public bool hasAttacked = true;

    public virtual void StartTurn()
    {
        hasAttacked = false;
    }


}
