using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    private List<RunData> runs = new List<RunData>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void AddRun(RunData data)
    {
        runs.Add(data);

        // sort by score descending
        runs.Sort((a, b) => b.score.CompareTo(a.score));
        SaveData();
    }

    public List<RunData> GetTopRuns(int count)
    {
        return runs.GetRange(0, Mathf.Min(count, runs.Count));
    }

    // save data function 

    void SaveData()
    {
        LeaderboardData data = new LeaderboardData();
        data.runs = runs;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/leaderboard.json", json);

        Debug.Log("DATA SAVED: " + Application.persistentDataPath);
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/leaderboard.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
            runs = data.runs;

            Debug.Log("DATA LOADED");
        }
        else
        {
            Debug.Log("NO SAVE FILE FOUND");
        }
    }
}