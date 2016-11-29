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
    public GameObject selectMapPanel;
    public Dropdown mapSelectDropdown;

    void Start() {
        mapSelectDropdown.value = PlayerPrefs.GetInt("SelectedMap");
    }

    public void playButton() {
        SceneManager.LoadScene("TanksGame");
    }    

    public void displayHowToPlay() {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        selectMapPanel.SetActive(false);
    }

    public void displaySelectMap() {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        selectMapPanel.SetActive(true);
    }

    public void returnToMainMenu() {
        mainPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        selectMapPanel.SetActive(false);
    }

    public void exit() {
        Application.Quit();
    }

    public void selectMap() {
        PlayerPrefs.SetInt("SelectedMap", mapSelectDropdown.value);
    }
}
