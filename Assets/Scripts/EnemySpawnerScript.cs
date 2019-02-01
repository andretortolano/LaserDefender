using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs = null;
    int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves() {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveIndex]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int enemyCount = 0; enemyCount < currentWave.getNumberOfEnemies(); enemyCount++) {
            var enemy = Instantiate(currentWave.getEnemyPrefab(), currentWave.getWayPoints()[0].transform.position, Quaternion.identity);

            enemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);

            yield return new WaitForSeconds(currentWave.getTimeBetweenSpams());
        }
    }
}
