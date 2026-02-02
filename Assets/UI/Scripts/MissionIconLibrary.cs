using UnityEngine;

public class MissionIconLibrary : MonoBehaviour
{
    public static MissionIconLibrary Instance;

    public Sprite distanceIcon;
    public Sprite coinIcon;
    public Sprite survivalIcon;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Sprite GetIcon(MissionType type)
    {
        switch (type)
        {
            case MissionType.Distance:
                return distanceIcon;
            case MissionType.Coins:
                return coinIcon;
            case MissionType.Survival:
                return survivalIcon;
            default:
                return null;
        }
    }
}
