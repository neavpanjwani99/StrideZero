using UnityEngine;

public class PlayerDistanceTracker : MonoBehaviour
{
    public MissionManager missionManager;
    public PlayerMovement playerMovement;

    [SerializeField] float distancePerSecond = 4f;
    [SerializeField] float speedInfluence = 0.25f;

    void Update()
    {
        if (missionManager == null || playerMovement == null)
            return;

        if (missionManager.CurrentMissionType != MissionType.Distance)
            return;

        float speed = playerMovement.GetCurrentSpeed();

        float progress =
            Time.deltaTime * (distancePerSecond + speed * speedInfluence);

        missionManager.AddProgress(progress);
    }
}
