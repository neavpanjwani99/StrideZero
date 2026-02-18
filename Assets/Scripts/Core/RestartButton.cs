using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void OnRestartClicked()
    {
        if (GameManagerExists())
        {
            FindObjectOfType<GameManager>().RestartGame();
        }
    }

    bool GameManagerExists()
    {
        return FindObjectOfType<GameManager>() != null;
    }
}
