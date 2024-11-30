using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameManager : MonoBehaviour
{
    public GameObject[] flameBalls;
    public float initialSpawnDelay = 1.0f;
    public float spawnAcceleration = 0.1f;  // Decrease in delay time after each spawn
    public float minimumSpawnDelay = 0.1f;
    public float maxSpeed = 20f;
    public float speedIncrement = 0.1f;

    private float currentSpawnDelay;
    private float currentSpeed;  // Track the speed globally

    int passedFlames = -1;

    GameManager gameManager;
    MessageManager messageManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        messageManager = FindObjectOfType<MessageManager>();

        currentSpawnDelay = initialSpawnDelay;
        currentSpeed = 15f;  // Initialize global speed
        ResetPassedFlames();
    }

    // if character misses 15 flames distance is reduced and increased by 1.
    public void SetPassedFlames(int numberOfPassedFlames) { passedFlames = numberOfPassedFlames; }
    public int GetPassedFlames() { return passedFlames; }
    public void PassedFlames()
    {
        passedFlames++;
        if (passedFlames % 25 == 0)
        {
            messageManager.NextMessage();
            messageManager.ShowText();
        }
    }

    public void GenerateFlameBalls()
    {
        StartCoroutine(StartGeneratingFlameBalls());
    }

    void ResetPassedFlames() { passedFlames = -1; }

    IEnumerator StartGeneratingFlameBalls()
    {
        while (gameManager.IsStartGame() && !(gameManager.IsGameOver() || gameManager.IsGameWin() || gameManager.IsPause()))
        {
            // Generate a flame ball
            GenerateFlameBall();

            // Wait for the current spawn delay
            yield return new WaitForSeconds(currentSpawnDelay);

            // Decrease the spawn delay to increase the speed of generating flame balls
            currentSpawnDelay = Mathf.Max(minimumSpawnDelay, currentSpawnDelay - spawnAcceleration);
        }
    }

    void GenerateFlameBall()
    {
        // Choose a random flame ball prefab
        GameObject flameBallPrefab = flameBalls[UnityEngine.Random.Range(0, flameBalls.Length)];

        // Instantiate the flame ball at the random position
        GameObject flameBall = Instantiate(flameBallPrefab);

        Flame flameScript = flameBall.GetComponent<Flame>();

        // Increase the global speed, ensuring it doesn't exceed maxSpeed
        currentSpeed = Mathf.Min(currentSpeed + speedIncrement, maxSpeed);

        // Set the flame's speed to the current global speed
        flameScript.speed = currentSpeed;

        PassedFlames();
    }
}
