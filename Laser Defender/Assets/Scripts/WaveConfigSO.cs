using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;



[CreateAssetMenu(menuName =  "Wave Config", fileName = "New Wave Config")]
//Creates a set of variables that control how the wave works and methods to access those variables 

public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private float timeBetweenEnemySpawns = 1f;
    [SerializeField] private float spawnTimeVariance = 0f;
    [SerializeField] private float minSpawnTime = 0.2f;
    [SerializeField] int enemyIncrease = 1;



    public Transform GetStartingWaypoint() 
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }

    public void IncreaseWaveDifficulty()
    {
        for(int i = 0; i < enemyIncrease; i++)
        {
            enemyPrefabs.Add(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
        }   
    }

}
    

