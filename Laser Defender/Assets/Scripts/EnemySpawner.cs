using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    [SerializeField] private bool isLooping;
    [SerializeField] int totalWaveNumber = 0;
    float waveNumber = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            foreach(WaveConfigSO wave in waveConfigs) //For every wave.
            {
                currentWave = wave;
                for(int i = 0; i < currentWave.GetEnemyCount(); i++) //Loop through each enemy in the current wave and spawn them at the first waypoint.
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform); 


                    yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
                    

                }
                totalWaveNumber += 1;
                waveNumber +=1;
                if(waveNumber == waveConfigs.Count) //Every time the final wave is reached, increase the difficulty of each wave.
                {
                    foreach(WaveConfigSO waveConfig in waveConfigs)
                    {
                        waveConfig.IncreaseWaveDifficulty();
                    }
                }
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }

        }
        while (isLooping); 
    }

    public int GetWaveNumber()
    {
        return totalWaveNumber + 1;
    }



}
