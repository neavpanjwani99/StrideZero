using UnityEngine;

public class SegmentLooper : MonoBehaviour
{
    public float segmentLength = 200f;
    public int segmentCount = 3;

    void Update()
    {
        if (transform.position.z <= -segmentLength)
        {
            transform.position += Vector3.forward * segmentLength * segmentCount;
        }
    }
}
