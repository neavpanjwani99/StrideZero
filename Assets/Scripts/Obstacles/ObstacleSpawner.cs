using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameConfig config;
    public GameObject[] obstaclePrefabs;

    public int poolSize = 8;

    private List<GameObject> pool = new List<GameObject>();
    private bool isGameOver = false;

    void OnEnable()
    {
        GameEvents.OnGameOver += StopSpawning;
    }

    void OnDisable()
    {
        GameEvents.OnGameOver -= StopSpawning;
    }

   void Start()
{
    for (int i = 0; i < poolSize; i++)
    {
        // Random prefab choose while creating pool
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
    }

    InvokeRepeating(nameof(SpawnObstacle), 1f, config.obstacleInterval);
}

    void SpawnObstacle()
    {
        if (isGameOver) return;

        GameObject obj = GetInactiveObstacle();
        if (obj == null) return;

        float x = Random.Range(-config.laneLimit, config.laneLimit);
        obj.transform.position = new Vector3(x, 0.5f, config.obstacleSpawnZ);
        obj.SetActive(true);
    }

    GameObject GetInactiveObstacle()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        return null;
    }

    void StopSpawning()
    {
        isGameOver = true;
        CancelInvoke();
    }
}