using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    TurnBasedEntity turnBasedEntity;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onPlayerSpawned += SetEntity;
    }

    public void SetEntity()
    {
        turnBasedEntity = GameManager.instance.playerReference.GetComponent<TurnBasedEntity>();
    }

    public void EndTurn()
    {
        turnBasedEntity.EndTurn();
    }
}
