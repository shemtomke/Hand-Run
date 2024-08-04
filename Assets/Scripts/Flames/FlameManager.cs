using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FlameManager : MonoBehaviour
{
    public GameObject[] flameBalls;
    public float initialSpawnDelay = 1.0f;
    public float spawnAcceleration = 0.1f;  // Decrease in delay time after each spawn
    public float minimumSpawnDelay = 0.1f;
    public float maxSpeed = 20f;
    public float speedIncrement = 0.1f;

    private float currentSpawnDelay;
    int passedFlames = 0;

    GameManager gameManager;
    MessageManager messageManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        messageManager = FindObjectOfType<MessageManager>();

        currentSpawnDelay = initialSpawnDelay;
        ResetPassedFlames();
    }
    // if character misses 15 flames distance is reduced and increased by 1.
    public void SetPassedFlames(int numberOfPassedFlames) { passedFlames = numberOfPassedFlames; }
    public int GetPassedFlames() {  return passedFlames; }
    public void PassedFlames()
    { 
        passedFlames++;
        if (passedFlames % 50 == 0)
        {
            messageManager.NextMessage();
            messageManager.ShowText();
        }
        else
        {
            messageManager.EmptyText();
        }
    }
    public void GenerateFlameBalls()
    {
        StartCoroutine(StartGeneratingFlameBalls());
    }
    void ResetPassedFlames() { passedFlames = 0; }
    IEnumerator StartGeneratingFlameBalls()
    {
        Debug.Log("About to Start Flame! -> " + gameManager.IsStartGame());

        while (gameManager.IsStartGame() && !(gameManager.IsGameOver() || gameManager.IsGameWin() || gameManager.IsPause()))
        {
            Debug.Log("Starting Flame.... -> " + gameManager.IsStartGame());

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
        GameObject flameBallPrefab = flameBalls[Random.Range(0, flameBalls.Length)];

        // Instantiate the flame ball at the random position
        GameObject flameBall = Instantiate(flameBallPrefab);

        Flame flameScript = flameBall.GetComponent<Flame>();
        flameScript.speed = Mathf.Min(flameScript.speed + speedIncrement, maxSpeed);

        PassedFlames();
    }
}
