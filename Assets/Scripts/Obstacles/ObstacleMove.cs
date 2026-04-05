using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 12f;
    public float deactivateZ = -10f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // If the obstacle is moving too fast, we can deactivate it
        if (transform.position.z < deactivateZ)
        {
            gameObject.SetActive(false); // no destroy
        }
    }
}