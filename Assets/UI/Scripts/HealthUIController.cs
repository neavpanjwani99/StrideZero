using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private List<Image> hearts = new List<Image>();

    private int lastShownHealth = -1;

    public void UpdateHealth(float currentHealth)
    {
        int healthInt = Mathf.CeilToInt(currentHealth);

        if (healthInt == lastShownHealth) return;
        lastShownHealth = healthInt;

        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = i < healthInt;
        }
    }

    public void ClearAll()
    {
        foreach (var heart in hearts)
            heart.enabled = false;
    }
}
