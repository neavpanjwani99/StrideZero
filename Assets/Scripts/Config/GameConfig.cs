using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Speed")]
    public float startSpeed = 12f;
    public float maxSpeed = 25f;
    public float speedIncrease = 0.5f;
    public float speedIncreaseInterval = 5f;

    [Header("Player")]
    public float laneLimit = 1.6f;
    public float jumpForce = 7f;
    public float sideSpeed = 6f;

    [Header("Obstacles")]
    public float obstacleSpawnZ = 80f;
    public float obstacleInterval = 2f;
}
