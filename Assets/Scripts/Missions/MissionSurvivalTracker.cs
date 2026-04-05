using UnityEngine;

public class MissionSurvivalTracker : MonoBehaviour
{
    public MissionManager missionManager;

    void Update()
    {
        if (missionManager.CurrentMissionType != MissionType.Survival)
            return;

        missionManager.AddProgress(Time.deltaTime);
    }
}