using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameManager : MonoBehaviour
{
    public GameObject[] flameBalls;
    public float initialSpawnDelay = 1.0f;
    public float spawnAcceleration = 0.1f;  // Decrease in delay time after each spawn
    public float minimumSpawnDelay = 0.1f;

    private float currentSpawnDelay;

    private void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        GenerateFlameBalls();
    }
    public void GenerateFlameBalls()
    {
        StartCoroutine(StartGeneratingFlameBalls());
    }
    IEnumerator StartGeneratingFlameBalls()
    {
        while (true)
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
        GameObject flameBallPrefab = flameBalls[Random.Range(0, flameBalls.Length)];

        // Instantiate the flame ball at the random position
        Instantiate(flameBallPrefab);
    }
}
