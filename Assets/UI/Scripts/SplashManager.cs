using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    public Image fadeImage;

    void Start()
    {
        fadeImage.canvasRenderer.SetAlpha(1f);
        fadeImage.CrossFadeAlpha(0f, 1.5f, false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}