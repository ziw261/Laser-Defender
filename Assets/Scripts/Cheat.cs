using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] Player player;
    

    public void CheatHealth() {
        player.MoreHealth();
        FindObjectOfType<GameSession>().Kejin();
    }

    
}
