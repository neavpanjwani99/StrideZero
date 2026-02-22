using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
    public GameObject leaderboardPanel;

    public void OnLeaderboardClicked()
    {
        leaderboardPanel.SetActive(true);
    }
}