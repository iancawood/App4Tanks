using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/*
 * Source of toon explosion animation: https://www.assetstore.unity3d.com/en/#!/content/66932
 * Originally written by: Red Shark
 */

public class MainMenu : MonoBehaviour {
    public GameObject mainPanel;
    public GameObject howToPlayPanel;

    public void playButton() {
        SceneManager.LoadScene("TanksGame");
    }    

    public void displayHowToPlay() {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void returnToMainMenu() {
        howToPlayPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void exit() {
        Application.Quit();
    }
}
