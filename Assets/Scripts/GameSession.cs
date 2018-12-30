using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int money = 0;

    private void Awake() {
        SetUpSingleton();
    }

    private void SetUpSingleton() {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() {
        return score;
    }

    public int GetMoney() {
        return money;
    }

    public void Kejin() {
        money += 20;
    }

    public void AddToScore(int scoreValue) {
        score += scoreValue;
    }

    public void ResetGame() {
        Destroy(gameObject);
    }
}
