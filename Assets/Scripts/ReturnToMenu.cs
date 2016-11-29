using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnToMenu : MonoBehaviour {
    public void returnToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
