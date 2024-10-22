using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverSetupScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI turns;

    void Start()
    {
        scoreText.text = GameManager.instance.score.ToString();
        turns.text = TurnManager.instance.turnCount.ToString();
    }

}
