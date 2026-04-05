using UnityEngine;
/* 
    we can do hardcode but using scriptable objects helps us to easily manage 
    and change values through inspector without changing code and recompiling, 
    also it allows us to create multiple difficulty configs for different levels 
    or game modes    
*/
[CreateAssetMenu(menuName = "Game/Difficulty Config")]
public class DifficultyConfig : ScriptableObject
{
    public GameDifficulty difficulty;

    [Header("Mission Scaling")]
    public float distanceMultiplier = 1f;
    public float coinMultiplier = 1f; // difficulty ke hissab se coins swamp 
    public float survivalMultiplier = 1f; // survival time calculation 

    [Header("Gameplay Pressure")]
    public float energyDrainMultiplier = 1f; // health kitni fast decrease hogi
}
