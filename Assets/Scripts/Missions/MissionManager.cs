using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public DifficultyConfig difficultyConfig;

    MissionData currentMission;
    float progress;

    bool missionCompleted = false;

    [SerializeField] int maxMissionsPerRun = 3;
    int missionsCompleted = 0;

    float difficultyRamp = 1f;
    [SerializeField] float difficultyStep = 0.35f;

    void Start()
    {
        GenerateMission();
    }

    void GenerateMission()
    {
        GameEvents.OnNewMission?.Invoke(); // 🔥 STEP 3 RESET POINT

        MissionType type = (MissionType)Random.Range(0, 3);

        currentMission = new MissionData();
        currentMission.type = type;

        switch (type)
        {
            case MissionType.Distance:
                currentMission.targetValue =
                    150f * difficultyConfig.distanceMultiplier * difficultyRamp;
                break;

            case MissionType.Coins:
                currentMission.targetValue =
                    Mathf.Ceil(
                        10f * difficultyConfig.coinMultiplier * difficultyRamp
                    );
                break;

            case MissionType.Survival:
                currentMission.targetValue =
                    20f * difficultyConfig.survivalMultiplier * difficultyRamp;
                break;
        }

        progress = 0f;
        missionCompleted = false;

        Debug.Log(
            $"MISSION {missionsCompleted + 1}: {type} → {currentMission.targetValue}"
        );
    }

    public void AddProgress(float amount)
    {
        if (missionCompleted) return;

        progress += amount;

        if (progress >= currentMission.targetValue)
        {
            missionCompleted = true;
            missionsCompleted++;

            Debug.Log(
                $"MISSION COMPLETE ({missionsCompleted}/{maxMissionsPerRun})"
            );

            GameEvents.OnMissionCompleted?.Invoke();

            difficultyRamp += difficultyStep;

            if (missionsCompleted < maxMissionsPerRun)
            {
                Invoke(nameof(GenerateMission), 1.2f);
            }
            else
            {
                Debug.Log("RUN COMPLETE");
                GameEvents.OnRunCompleted?.Invoke();
            }
        }
    }

    public MissionType CurrentMissionType
    {
        get { return currentMission.type; }
    }

    public MissionData GetMission() => currentMission;
    public float GetProgress() => progress;
}
