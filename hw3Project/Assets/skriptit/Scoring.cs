using System.Collections;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] throwObjects;
    public Transform startPosition;
    public int totalThrows = 5;
    private int score = 0;
    private int throwCounter = 0;
    private bool gameEnded = false;

    void Start()
    {
        // Initialize the score display
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int points)
    {
        if (gameEnded || throwCounter >= totalThrows)
        {
            Debug.Log("Game over or max throws reached. Ignoring score update in AddScore");
            return;
        }
        Debug.Log("In AddScore: Score before: " + score + " | Adding: " + points);
        score += points;
        throwCounter++;
        UpdateScore();
        if (throwCounter >= totalThrows)
        {
            Debug.Log("In AddScore: Max throws reached! Ending game.");
            EndGame();
        }
    }

    void UpdateScore()
    {
        Debug.Log("Updating score display: " + score);
        scoreText.text = "Score: " + score;
    }

    void EndGame()
    {
        gameEnded = true;
        scoreText.text = score < 40 ? $"You lose! Score: {score}" : $"You win! Score: {score}";

        // Reset Game
        Invoke(nameof(ResetGame), 3f);
    }

    void ResetGame()
    {
        Debug.Log("Resetting Game!");

        score = 0;
        throwCounter = 0;
        gameEnded = false;
        UpdateScore();

        foreach (GameObject obj in throwObjects)
        {
            Debug.Log("Resetting: " + obj.name);
            obj.transform.position = startPosition.position;
            obj.transform.rotation = Quaternion.identity;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
