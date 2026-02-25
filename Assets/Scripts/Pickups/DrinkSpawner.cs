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

        // Raycast from above to detect ground properly
        Vector3 rayStart = new Vector3(x, 10f, spawnZ);
        RaycastHit hit;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, 20f))
{
    float height = obj.GetComponent<Collider>().bounds.extents.y;
    
    float extraHeight = 1.1f;   
    
    obj.transform.position = hit.point + Vector3.up * (height + extraHeight);
}
        else
        {
            // fallback (in case raycast fails)
            float height = obj.GetComponent<Collider>().bounds.extents.y;
            obj.transform.position = new Vector3(x, height, spawnZ);
        }

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