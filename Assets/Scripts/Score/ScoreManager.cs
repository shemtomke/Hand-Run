using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;        // UI Text element for displaying the score
    public Text highScoreText, mainMenuHighScoreText;    // UI Text element for displaying the high score

    private int score;            // Current score
    private int highScore;        // High score

    private float scoreTimer;     // Timer to track fractional seconds

    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HighScore\n" + highScore;
        mainMenuHighScoreText.text = "HighScore\n" + highScore;

        UpdateScoreText();
    }
    private void Update()
    {
        if (gameManager.IsGameOver() || gameManager.IsGameWin() || !gameManager.IsStartGame() || gameManager.IsPause())
            return;

        // Increment the timer by the time elapsed since the last frame
        scoreTimer += Time.deltaTime;

        // If the timer reaches or exceeds 1 second, increment the score
        if (scoreTimer >= 1f)
        {
            int increment = Mathf.FloorToInt(scoreTimer); // Determine how many whole seconds have passed
            score += increment;                           // Increase the score
            scoreTimer -= increment;                      // Reduce the timer by the number of seconds added to the score

            UpdateScoreText();

            // Update the high score if the current score exceeds it
            if (score > highScore)
            {
                highScore = score;
                highScoreText.text = "HighScore\n" + highScore;

                // Save the new high score
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
    }
    // remember the two characters(female and mosquito) have a long but not too long head start(start with a higher points),
    // the mosquito has the longest.
    public void SetStartScore(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Boy:
                score = 0;
                scoreTimer = score;
                break;
            case CharacterType.Girl:
                score = 300;
                scoreTimer = score;
                break;
            case CharacterType.Mosquito:
                score = 200;
                scoreTimer = score;
                break;
            default:
                break;
        }
    }
    private void UpdateScoreText()
    {
        // Check if the score is divisible by 50
        if (score % 50 == 0)
        {
            // Update the score text to show the score
            scoreText.text = "" + score;
        }
    }
    public void ResetScore()
    {
        // Reset the current score and timer
        score = 0;
        scoreTimer = 0f;
        UpdateScoreText();
    }
}
