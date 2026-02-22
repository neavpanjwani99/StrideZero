using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int poolSize = 5;
    public float spawnInterval = 4f;

    public float spawnZ = 80f;
    public float minX = -2f;
    public float maxX = 2f;

    private List<GameObject> pool = new List<GameObject>();
    private float timer;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(coinPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

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
        GameObject obj = GetInactive();
        if (obj == null) return;

        float x = Random.Range(minX, maxX);
        obj.transform.position = new Vector3(x, 0.6f, spawnZ);
        obj.SetActive(true);
    }

    GameObject GetInactive()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        return null;
    }
}
    //     void SpawnCoin()
    // {
    //     float x = Random.Range(minX, maxX);
    //     Vector3 spawnPos = new Vector3(x, 0.6f, spawnZ);

    //     Quaternion rotation = Quaternion.Euler(90f, 90f, 35f);

    //     Instantiate(coinPrefab, spawnPos, rotation);
    // }

