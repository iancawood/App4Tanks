using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
    public TurnManager turnManager;
    public Text timeLeftText;

    public float timeLeft = 0;
    public bool active = true;

    float timeout = 30f;

    void Update() {
        timeLeft -= Time.deltaTime;

        if (active) {
            if (timeLeft < 0) {
                turnManager.nextTurn(true);
            } else {
                updateTimeLeftText();
            }
        }
    }

    public void reset() {
        timeLeft = timeout;
    }

    public void toggle() {
        active = !active;
    }

    void updateTimeLeftText() {
        timeLeftText.text = "Time Left: " + Mathf.Round(timeLeft);
    }
}
