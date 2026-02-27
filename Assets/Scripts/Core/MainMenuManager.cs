using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public AudioSource menuAudio;

    void Start()
    {
        volumeSlider.value = menuAudio.volume;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void OpenLeaderboard()
    {
        leaderboardPanel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    void ChangeVolume(float value)
    {
        menuAudio.volume = value;
    }
}