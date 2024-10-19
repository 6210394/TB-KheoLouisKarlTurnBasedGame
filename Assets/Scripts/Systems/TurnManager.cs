using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region Singleton
        public static TurnManager instance;
        void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    List<GameObject> gameEntities = new List<GameObject>();

    int nextEntityTurnIndex = 0;

    void Start()
    {
        Init();
        SortListByInitiative();
    }

    void Init()
    {
        foreach (TurnBasedEntity entity in FindObjectsOfType<TurnBasedEntity>())
        {
            AddEntityToEntityList(entity.gameObject);
        }
    }
    
    public void AddEntityToEntityList(GameObject entity)
    {
        gameEntities.Add(entity);
    }

    public void RemoveEntityFromEntityList(GameObject entity)
    {
        gameEntities.Remove(entity);
    }

    public void CallStartNextEntityTurn()
    {
        if (nextEntityTurnIndex >= gameEntities.Count)
        {
            nextEntityTurnIndex = 0;
        }
        gameEntities[nextEntityTurnIndex].GetComponent<TurnBasedEntity>().StartTurn();
        nextEntityTurnIndex += 1;

    }

    public void SortListByInitiative()
    {
        gameEntities.Sort((x, y) => x.GetComponent<TurnBasedEntity>().initiative.CompareTo(y.GetComponent<TurnBasedEntity>().initiative));
        gameEntities.Reverse();

        // Debugging purposes
        foreach (GameObject entity in gameEntities)
        {
            Debug.Log(entity.name + " has an initiative of " + entity.GetComponent<TurnBasedEntity>().initiative);
        }
    }

    
}
