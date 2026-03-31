using UnityEngine;
using System;

public class DayNightManager : MonoBehaviour
{
    public Material daySkybox;
    public Light playerLight;
    public Material nightSkybox;
    public Light sunLight;
    Color dayColor = new Color(0.35f, 0.65f, 1f);
    Color sunsetColor = new Color(0.4f, 0.7f, 1f);
    Color nightColor = new Color(0.05f, 0.1f, 0.35f);
    Color dayLight = new Color(1f, 0.98f, 0.92f);
    float dayIntensity = 0.9f;
    Color startCol = new Color(1f, 0.7f, 0.4f);
    Color endCol = new Color(1f, 0.3f, 0.2f);

    void Start()
    {
        InvokeRepeating(nameof(UpdateEnvironment), 0f, 5f);
    }

    void UpdateEnvironment()
    {
        //float hour = 22f;
        float hour = DateTime.Now.Hour + DateTime.Now.Minute / 60f;
        if (hour >= 6f && hour < 18f)
        {
            RenderSettings.skybox = daySkybox;
            Apply(dayColor, dayLight, dayIntensity, 50f, 0.25f);
            playerLight.enabled = false;
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", 0.4f);
            RenderSettings.skybox.SetFloat("_Exposure", 1f);
            RenderSettings.skybox.SetColor("_GroundColor", new Color(0.28f, 0.38f, 0.5f));
        }
        else if (hour >= 18f && hour < 20f)
        {
            RenderSettings.skybox = daySkybox;
            float t = (hour - 18f) / 2f;
            playerLight.enabled = false;
            Color bg = Color.Lerp(dayColor, sunsetColor, t);
            Color lightCol = Color.Lerp(startCol, endCol, t);
            float intensity = Mathf.Lerp(1.05f, 1.05f, t);
            float angle = Mathf.Lerp(50f, 0f, t);
            Apply(bg, lightCol, intensity, angle, 0.25f);
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1.2f);
            RenderSettings.skybox.SetFloat("_Exposure", 0.9f);
            RenderSettings.skybox.SetColor("_GroundColor", new Color(0.8f, 0.6f, 0.5f));
        }
        else
        {
            RenderSettings.skybox = nightSkybox;

            Apply(nightColor, new Color(0.3f, 0.4f, 0.8f), 0.12f, -20f, 0.7f);
            playerLight.enabled = true;
            DynamicGI.UpdateEnvironment();
        }
    }

    void Apply(Color bg, Color lightCol, float intensity, float sunAngle, float ambientMultiplier)
    {
        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetColor("_SkyTint", bg);
        }

        if (sunLight != null)
        {
            sunLight.color = lightCol;
            sunLight.intensity = intensity;
            sunLight.transform.rotation = Quaternion.Euler(sunAngle, 0, 0);
        }

        RenderSettings.ambientLight = bg * ambientMultiplier;
        //RenderSettings.skybox.SetFloat("_AtmosphereThickness", 0.4f);
        //RenderSettings.skybox.SetFloat("_Exposure", 1f);
        //RenderSettings.skybox.SetColor("_GroundColor", new Color(0.2f, 0.25f, 0.3f));
    }
}