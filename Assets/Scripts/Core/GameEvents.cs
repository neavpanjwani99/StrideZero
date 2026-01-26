using System;

public static class GameEvents
{
    public static Action OnGameOver;
    public static Action OnPlayerHit;
    public static Action<int> OnScoreChanged;
    public static Action<int> OnHealthChanged;
    public static Action<int> OnCoinsChanged;
}
