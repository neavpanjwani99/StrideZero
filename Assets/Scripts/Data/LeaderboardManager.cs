using System.Collections.Generic;
using UnityEngine;

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
    }

    public void AddRun(RunData data)
    {
        runs.Add(data);

        // sort by score descending
        runs.Sort((a, b) => b.score.CompareTo(a.score));
    }

    public List<RunData> GetTopRuns(int count)
    {
        return runs.GetRange(0, Mathf.Min(count, runs.Count));
    }
}