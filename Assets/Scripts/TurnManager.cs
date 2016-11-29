using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnManager : MonoBehaviour {
    public Text turnText;
    public Timer timer;
    public GameObject gameOverPanel;
    public Text winnerText;

    public bool playerTurn = true;

    int turnNumber = 1;

	void Start () {
        updateTurnText();
    }

    public void nextTurn(bool forced = false) {
        playerTurn = !playerTurn;
        turnNumber++;
        updateTurnText();
        timer.reset();
    }   

    public void gameOver(bool playerWin) {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        winnerText.text = "Winner: " + (playerWin ? "Player" : "Enemy");
    }

    void updateTurnText() {
        turnText.text = "Turn " + turnNumber.ToString() + ": " + (playerTurn ? "Player" : "Enemy");
    }
}
