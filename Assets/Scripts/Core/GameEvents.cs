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
public static System.Action OnGamePaused;
public static System.Action OnGameStart;

public static System.Action OnGameResumed;
public static Action OnNewMission;


}
