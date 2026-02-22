using UnityEngine;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    public Transform entryContainer;
    public GameObject entryPrefab;

    void OnEnable()
    {
        Refresh();
    }

    void Refresh()
    {
        foreach (Transform child in entryContainer)
            Destroy(child.gameObject);

        List<RunData> runs = LeaderboardManager.Instance.GetTopRuns(5);

        for (int i = 0; i < runs.Count; i++)
        {
            GameObject obj = Instantiate(entryPrefab, entryContainer);
            obj.GetComponent<LeaderboardEntryUI>().Setup(i + 1, runs[i]);
        }
    }
}