using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 4f;

    public float spawnZ = 80f;
    public float minX = -2f;
    public float maxX = 2f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        float x = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(x, 0.6f, spawnZ);

        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }
}
