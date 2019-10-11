using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] float delayInSeconds = 2f;


    public void LoadStartMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadGame() {
        SceneManager.LoadScene(10);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad());
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadCharB() {
        SceneManager.LoadScene(1);
    }

    public void LoadCharA() {
        SceneManager.LoadScene(11);
    }

    public void LoadChooseCharacter() {
        SceneManager.LoadScene(10);
    }

    public void LoadStuffB() {
        SceneManager.LoadScene("Stuff_B");
    }

    public void LoadNotTheEnd() {
        SceneManager.LoadScene(25);
    }
    IEnumerator WaitAndLoad() {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");

    }
}
