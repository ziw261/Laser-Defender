using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatDisplay : MonoBehaviour
{
    Text cheatText;
    GameSession gameSession;

    void Start() {
        cheatText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update() {
        cheatText.text = "已氪:" + gameSession.GetMoney().ToString() + "$";
    }
}
