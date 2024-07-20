using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// distance from arms to character and distance from character to pick-up is displayed.
public class DistanceManager : MonoBehaviour
{
    [Header("UI")]
    public Text playerPickupDistText;
    public Text playerArmDistText;

    [Header("Transform Positions")]
    public Transform armPosition;
    public Transform pickupPosition;
    public Transform playerPosition;
    float playerPickupDist, playerArmDist;

    private void Start()
    {
        UpdateDistance();
    }
    public void UpdateDistance()
    {
        playerPickupDistText.text = "" + FindDistance(pickupPosition, playerPosition);
        playerArmDistText.text = "" + FindDistance(armPosition, playerPosition);
    }
    float FindDistance(Transform fromPos, Transform toPos)
    {
        float distance = Vector2.Distance(fromPos.position, toPos.position);
        return Mathf.Round(distance * 10f) / 10f;
    }
}
