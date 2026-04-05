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
    if (missionManager == null) return;

    if (missionManager.CurrentMissionType != MissionType.Coins)
        return;

    missionManager.AddProgress(amount);
}

} /* 
coin collect hone pe mission ka progress update ho, aur check karna ki current mission coin collect 
karne ka hai ki nahi, agar hai toh hi progress update karna hai, taki distance ya survival mission me 
coin collect karne se progress na badhe
*/