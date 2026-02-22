using System.Collections.Generic;
using UnityEngine;

public class DrinkSpawner : MonoBehaviour
{
    public GameObject drinkPrefab;
    public int poolSize = 2;

    public float checkInterval = 4f;
    [Range(0f, 1f)]
    public float spawnChance = 0.2f;

    public float spawnZ = 80f;
    public float minX = -1.6f;
    public float maxX = 1.6f;

    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(drinkPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }

        InvokeRepeating(nameof(TrySpawnDrink), 2f, checkInterval);
    }

    void TrySpawnDrink()
    {
        if (Random.value > spawnChance)
            return;

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