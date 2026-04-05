using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Speed")]
    public float startSpeed = 16f;
    public float maxSpeed = 45f;
    public float speedIncrease = 1.5f;
    public float speedIncreaseInterval = 3f;

    [Header("Player")]
    public float laneLimit = 1.6f;
    public float jumpForce = 10f;
    public float sideSpeed = 6f;

    [Header("Obstacles")] // obstacle settings ke liye central control config hai, yeh use kiya taki future me kuch changes lane ho tho direct yahan se ho jaye
    public float obstacleSpawnZ = 80f;
    public float obstacleInterval = 2f;

    [Header("Speed Settings")]
    public float baseForwardSpeed = 9f;
    public float easySpeedMultiplier = 1f;
    public float normalSpeedMultiplier = 1.15f;
    public float hardSpeedMultiplier = 1.35f;

}
