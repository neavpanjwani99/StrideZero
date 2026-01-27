using UnityEngine;

public class DrinkSpawner : MonoBehaviour
{
    public GameObject drinkPrefab;

    public float checkInterval = 4f;   
    [Range(0f, 1f)]
    public float spawnChance = 0.2f;

    public float spawnZ = 80f;
    public float minX = -1.6f;
    public float maxX = 1.6f;


    void Start()
    {
        InvokeRepeating(nameof(TrySpawnDrink), 2f, checkInterval);
    }

    void TrySpawnDrink()
    {
        if (Random.value > spawnChance)
            return;

        float x = Random.Range(minX, maxX);

        Vector3 spawnPos = new Vector3(x, 0.6f, spawnZ);
        Instantiate(drinkPrefab, spawnPos, Quaternion.identity);
    }
}
