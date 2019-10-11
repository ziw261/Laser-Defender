using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] SceneLoader sceneLoader;


    int totalEnemy = -1;
    int enemyNum = -1;



    // Start is called before the first frame update
    IEnumerator Start() {
       

        do {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves() {
        for(int i=startingWave; i<waveConfigs.Count; i++) {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    public void killOneEnemy ()
    {   
        if(totalEnemy == -1 && enemyNum == -1) {
            if (SceneManager.GetActiveScene().buildIndex == 2) {
                enemyNum = 9;
                totalEnemy = enemyNum;
            } else if (SceneManager.GetActiveScene().buildIndex == 4) {
                enemyNum = 18;
               //Debug.Log("I'm here once");
                totalEnemy = enemyNum;
            } else if (SceneManager.GetActiveScene().buildIndex == 6) {
                enemyNum = 25;
                totalEnemy = enemyNum;
            } else if (SceneManager.GetActiveScene().buildIndex == 8) {
                enemyNum = 10;
                totalEnemy = enemyNum;
            } else if (SceneManager.GetActiveScene().buildIndex == 12) {
                enemyNum = 1;
                totalEnemy = enemyNum;
            } else if (SceneManager.GetActiveScene().buildIndex == 14) {
                enemyNum = 12;
                totalEnemy = enemyNum;
            } else if (SceneManager.GetActiveScene().buildIndex == 16) {
                enemyNum = 60;
                totalEnemy = enemyNum;
            } else if (SceneManager.GetActiveScene().buildIndex == 20) {
                enemyNum = 1;
                totalEnemy = enemyNum;
            }
        }


        enemyNum--;
        //Debug.LogError(enemyNum);
        if (enemyNum == 0)
        {
            sceneLoader.LoadNextScene();
            enemyNum = -1; totalEnemy = -1;
            //Debug.Log("totalEnemy: "+ totalEnemy);


        }
    }


    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {

        for (int i = 0; i < waveConfig.GetNubmerOfEnemies(); i++) {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
