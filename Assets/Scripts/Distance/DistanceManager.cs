using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// distance from arms to character and distance from character to pick-up is displayed.
// distances are in meters.
// distance from pick-up to character is 50000 m and distance from arms to character is 25000 m.

public class DistanceManager : MonoBehaviour
{
    [Header("UI")]
    public Text playerArmDistText;

    [Header("Transform Positions")]
    public Transform armPosition;
    public Transform playerPosition;
    public Transform closeArmPosition;
    float playerArmDist;

    GameManager gameManager;
    SoundManager soundManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        if(IsCaught()) { gameManager.SetGameOver(true); }
    }
    // if character is touched by flame distance from arm to character reduces and distance from character to pick-up increases
    // remember distance from arms to character and character to pick-up is displayed.
    public void UpdateDistance()
    {
        playerArmDistText.text = "" + FindDistance(armPosition, playerPosition);
    }
    float FindDistance(Transform fromPos, Transform toPos)
    {
        float distance = Vector2.Distance(fromPos.position, toPos.position);
        return distance;
    }
    public void SetPlayerPosition(Transform transform) { playerPosition = transform; }
    public bool IsCloseToDoor()
    {
        if (playerPosition.position.x <= closeArmPosition.transform.position.x)
            return true;

        return false;
    }
    public bool IsCaught()
    {
        if (playerPosition == null)
            return false;

        if (playerPosition.position.x <= armPosition.transform.position.x)
            return true;
        return false;
    }
}
