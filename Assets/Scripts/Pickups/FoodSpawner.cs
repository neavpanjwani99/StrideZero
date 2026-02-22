using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public int poolSize = 5;

    public float spawnInterval = 6f;
    public float spawnZ = 25f;
    public float[] lanes = { -2f, 0f, 2f };

    private List<GameObject> pool = new List<GameObject>();
    private float timer;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(foodPrefab);
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
            SpawnFood();
        }
    }

    void SpawnFood()
    {
        GameObject obj = GetInactive();
        if (obj == null) return;

        float laneX = lanes[Random.Range(0, lanes.Length)];
        obj.transform.position = new Vector3(laneX, 0.5f, spawnZ);
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