using UnityEngine;
public class FoodSpawner : MonoBehaviour{    
    public GameObject foodPrefab;
    public float spawnInterval = 6f;
    public float spawnZ = 25f;
    public float[] lanes = { -2f, 0f, 2f };
    float timer;
    void Update(){
        timer += Time.deltaTime;
        if (timer >= spawnInterval){
            SpawnFood();
            timer = 0f;
        }
    }
    void SpawnFood(){
        float laneX = lanes[Random.Range(0, lanes.Length)];
        Vector3 spawnPos = new Vector3(laneX, 0.5f, spawnZ);
        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
    }
}
