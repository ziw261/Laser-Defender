using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 100;
    [SerializeField] EnemySpawner enemySpawner;

    public int GetDamage() {
        return damage;
    }

    public void Hit() {
        if (SceneManager.GetActiveScene().buildIndex == 8) {
            return;
        }
        else {
           
            if (gameObject.layer == 11) {
                Destroy(gameObject);
            }

        }

    }
   
}
