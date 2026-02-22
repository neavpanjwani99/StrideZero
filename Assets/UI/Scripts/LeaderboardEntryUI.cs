using TMPro;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    public TMP_Text rankText;
    public TMP_Text nameText;
    public TMP_Text scoreText;
    public TMP_Text coinText;
    public TMP_Text timeText;

    public void Setup(int rank, RunData data)
    {
        rankText.text = "#" + rank;
        nameText.text = data.playerName;
        scoreText.text = data.score.ToString();
        coinText.text = data.coins.ToString();
        timeText.text = data.survivalTime.ToString("F1") + "s";
    }
}