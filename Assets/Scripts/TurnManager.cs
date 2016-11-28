using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnManager : MonoBehaviour {
    public Text turnText;
    public Timer timer;

    public bool playerTurn = true;

    int turnNumber = 1;

	void Start () {
        updateTurnText();
    }
	
	void Update () {
	    
	}

    public void nextTurn(bool forced = false) {
        playerTurn = !playerTurn;
        turnNumber++;
        updateTurnText();
        timer.reset();
    }   

    public void gameOver(bool playerWin) {
        Debug.Log("game over, winner: " + (playerWin ? "player" : "enemy"));
    }

    void updateTurnText() {
        turnText.text = "Turn " + turnNumber.ToString() + ": " + (playerTurn ? "Player" : "Enemy");
    }
}
