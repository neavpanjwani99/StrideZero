using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameConfig config;
    public GameObject obstaclePrefab;

    bool isGameOver = false;

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
        InvokeRepeating(nameof(SpawnObstacle), 1f, config.obstacleInterval);
    }

    void SpawnObstacle()
    {
        if (isGameOver) return;

        float x = Random.Range(-config.laneLimit, config.laneLimit);

        Vector3 spawnPos = new Vector3(
            x,
            0.5f,
            config.obstacleSpawnZ
        );

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }

    void StopSpawning()
    {
        isGameOver = true;
        CancelInvoke();
    }
}
