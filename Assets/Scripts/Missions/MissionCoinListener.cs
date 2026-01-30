using UnityEngine;

public class MissionCoinListener : MonoBehaviour
{
    public MissionManager missionManager;

    void OnEnable()
    {
        GameEvents.OnCoinCollected += OnCoin;
    }

    void OnDisable()
    {
        GameEvents.OnCoinCollected -= OnCoin;
    }

    void OnCoin(int amount)
    {
        if (missionManager.CurrentMissionType != MissionType.Coins)
            return;

        missionManager.AddProgress(amount);
    }
}
