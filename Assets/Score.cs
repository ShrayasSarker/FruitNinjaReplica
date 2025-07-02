using UnityEngine;
using TMPro; // Add this namespace

public class Score : MonoBehaviour
{
    public static Score instance;

    public int score = 0;
    public TextMeshProUGUI scoreText; // Changed to TMP component

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
}
