using UnityEngine;

[CreateAssetMenu(menuName = "Game/Difficulty Config")]
public class DifficultyConfig : ScriptableObject
{
    public GameDifficulty difficulty;

    [Header("Mission Scaling")]
    public float distanceMultiplier = 1f;
    public float coinMultiplier = 1f;
    public float survivalMultiplier = 1f;

    [Header("Gameplay Pressure")]
    public float energyDrainMultiplier = 1f;
}
