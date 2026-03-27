using System;

[Serializable]
public class RunData
{
    public string playerName;
    public int score;
    public int coins;
    public float survivalTime;
    public string date;
     public long timestamp;

    public RunData(string name, int score, int coins, float time)
    {
        this.playerName = name;
        this.score = score;
        this.coins = coins;
        this.survivalTime = time;
        this.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}