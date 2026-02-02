using UnityEngine;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void Update()
    {
        if (ScoreManager.Instance == null) return;

        scoreText.text = "Score: " + ScoreManager.Instance.GetFinalScore();
    }
}
