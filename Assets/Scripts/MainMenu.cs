using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public void playButton() {
        SceneManager.LoadScene("TanksGame");
    }    
}
