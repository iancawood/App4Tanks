using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnManager : MonoBehaviour {
    public Text turnText;

    public bool playerTurn = true;

    int turnNumber = 1;
    int turnTimeout = 30;

	void Start () {
        updateTurnText();
    }
	
	void Update () {
	    
	}

    public void nextTurn() {
        playerTurn = !playerTurn;
        turnNumber++;
        updateTurnText();
    }   

    public void gameOver() {
        Debug.Log("game over");
    }

    void updateTurnText() {
        turnText.text = "Turn " + turnNumber.ToString() + ": " + (playerTurn ? "Player" : "Enemy");
    }
}
