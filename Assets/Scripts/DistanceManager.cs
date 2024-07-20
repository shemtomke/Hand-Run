using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// distance from arms to character and distance from character to pick-up is displayed.
public class DistanceManager : MonoBehaviour
{
    [Header("UI")]
    public Text playerPickupDistText, playerArmDistText;

    [Header("Transform Positions")]
    public Transform armPosition, pickupPosition, playerPosition;
    float playerPickupDist, playerArmDist;

    private void Update()
    {
        playerPickupDistText.text = "" + FindDistance(pickupPosition, playerPosition);
        playerArmDistText.text = "" + FindDistance(armPosition, playerPosition);
    }

    float FindDistance(Transform fromPos, Transform toPos)
    {
        return Vector2.Distance(fromPos.position, toPos.position);
    }
}
