using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] throwObjects;
    public int totalThrows = 5;
    private int score = 0;
    private int throwCounter = 0;
    private bool gameEnded = false;

    private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> initialRotations = new Dictionary<GameObject, Quaternion>();
    private Dictionary<GameObject, bool> hasBeenThrown = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, float> throwTimers = new Dictionary<GameObject, float>();

    public float throwDelay = 0.5f;

    void Start()
    {
        // Initialize the score display
        scoreText.text = "Score: " + score;

        // Save initial positions/rotations
        foreach (GameObject obj in throwObjects)
        {
            initialPositions[obj] = obj.transform.position;
            initialRotations[obj] = obj.transform.rotation;
            hasBeenThrown[obj] = false;
            throwTimers[obj] = 0f;
        }
    }

    void Update()
    {
        CheckThrows();
    }

    void CheckThrows()
    {
        foreach (GameObject obj in throwObjects)
        {
            if (!hasBeenThrown[obj] && Vector3.Distance(obj.transform.position, initialPositions[obj]) > 1.5f)
            {
                throwTimers[obj] += Time.deltaTime;
                if (throwTimers[obj] >= throwDelay)
                {
                    throwCounter++;
                    hasBeenThrown[obj] = true;
                    Debug.Log("Throw counter: " + throwCounter + " / " + totalThrows);
                    if (throwCounter >= totalThrows)
                    {
                        Debug.Log("In CheckThrows: Max throws reached! Ending game.");
                        EndGame();
                    }
                }
            }
            else
            {
                throwTimers[obj] = 0f;
            }
        }
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
        UpdateScore();
        if (score >= 60)
        {
            Debug.Log("In AddScore: Winning score achieved! Ending game.");
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
        scoreText.text = score < 60 ? $"You lose! Score: {score}" : $"You win! Score: {score}";

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
            obj.transform.position = initialPositions[obj];
            obj.transform.rotation = initialRotations[obj];
            hasBeenThrown[obj] = false;
            throwTimers[obj] = 0f;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; // Reset linear velocity
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
                rb.Sleep(); // Ensure the Rigidbody is not moving
            }
        }
    }
}
