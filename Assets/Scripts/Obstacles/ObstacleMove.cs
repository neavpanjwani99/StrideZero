using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Player ke peeche chala gaya → destroy
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}
