using System;

public static class GameEvents
{
    public static Action OnGameOver;
    public static Action OnPlayerHit;
    public static Action<int> OnScoreChanged;
    public static Action<int> OnHealthChanged;
    public static Action<int> OnCoinCollected;
    public static Action OnMissionCompleted;
    public static Action<int> OnLevelUp;
public static System.Action OnRunCompleted;

}
