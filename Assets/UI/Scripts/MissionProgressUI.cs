using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionProgressUI : MonoBehaviour
{
    public MissionType missionType;

    [Header("UI References")]
    public Image icon;
    public Image progressFill;
    public TextMeshProUGUI progressText;

    [Header("Mission Config")]
    public MissionType listenType;

    MissionManager missionManager;
    bool isCompleted = false; // 🔥 STEP 3 FIX

    void Awake()
    {
        missionManager = FindFirstObjectByType<MissionManager>();
    }

    void OnEnable()
    {
        Refresh();
        GameEvents.OnMissionCompleted += OnMissionCompleted;
        GameEvents.OnNewMission += OnNewMission;
    }

    void OnDisable()
    {
        GameEvents.OnMissionCompleted -= OnMissionCompleted;
        GameEvents.OnNewMission -= OnNewMission;
    }

    void Update()
    {
        if (missionManager == null) return;
        if (isCompleted) return; // 🔥 COMPLETE ke baad overwrite band

        if (missionManager.CurrentMissionType != listenType)
        {
            progressFill.fillAmount = 0f;
            progressFill.color = Color.grey;
            progressText.text = "0 / 0";
            return;
        }

        float progress = missionManager.GetProgress();
        float target = missionManager.GetMission().targetValue;

        progressFill.fillAmount = Mathf.Clamp01(progress / target);
        progressText.text =
            $"{Mathf.FloorToInt(progress)} / {Mathf.FloorToInt(target)}";
    }

    void OnMissionCompleted()
    {
        if (missionManager.CurrentMissionType != listenType) return;

        isCompleted = true;

        progressFill.fillAmount = 1f;
        progressFill.color = Color.green;
        progressText.text = "COMPLETE";
    }

    void OnNewMission()
    {
        isCompleted = false;

        progressFill.fillAmount = 0f;
        progressFill.color = Color.grey;
        progressText.text = "0 / 0";
    }

    void Refresh()
    {
        icon.sprite = MissionIconLibrary.Instance.GetIcon(listenType);
    }
}
