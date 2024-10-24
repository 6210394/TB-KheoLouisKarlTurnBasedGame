using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI turnNum;
    public TextMeshProUGUI currentEntity;

    public int turnCount = 1;
    public int totalTurnCount;

    public void Init()
    {
        ClearEntityList();
        totalTurnCount += turnCount;
        turnCount = 0;

        foreach (TurnBasedEntity entity in FindObjectsOfType<TurnBasedEntity>())
        {
            AddEntityToEntityList(entity.gameObject);
        }

        SortListByInitiative();
        turnNum = GameObject.Find("TurnNum").GetComponent<TextMeshProUGUI>();
        currentEntity = GameObject.Find("CurrentEntity").GetComponent<TextMeshProUGUI>();
        turnNum.text = turnCount.ToString();

        CallStartNextEntityTurn();
    }
    
    public void AddEntityToEntityList(GameObject entity)
    {
        gameEntities.Add(entity);
    }

    public void RemoveEntityFromEntityList(GameObject entity)
    {
        gameEntities.Remove(entity);
        if(gameEntities.Count == 1)
        {
            CallStartNextEntityTurn();
        }
    }

    public void ClearEntityList()
    {
        gameEntities.Clear();
    }

    public void CallStartNextEntityTurn()
    {
        if (nextEntityTurnIndex >= gameEntities.Count)
        {
            nextEntityTurnIndex = 0;
            turnCount += 1;
            turnNum.text = turnCount.ToString();
        }
        gameEntities[nextEntityTurnIndex].GetComponent<TurnBasedEntity>().StartTurn();
        currentEntity.text = gameEntities[nextEntityTurnIndex].name + " is moving...";
        nextEntityTurnIndex += 1;
        
    }

    public void SortListByInitiative()
    {
        Debug.Log(gameEntities.Count);

        if(gameEntities.Count <= 1)
        {
            return;
        }
        gameEntities.Sort((x, y) => x.GetComponent<TurnBasedEntity>().initiative.CompareTo(y.GetComponent<TurnBasedEntity>().initiative));
        gameEntities.Reverse();

        // Debugging purposes
        foreach (GameObject entity in gameEntities)
        {
            Debug.Log(entity.name + " has an initiative of " + entity.GetComponent<TurnBasedEntity>().initiative);
        }
    }

    
}
