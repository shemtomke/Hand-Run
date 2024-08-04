using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    public float xOffset = 0.5f; // The offset from the left edge of the screen in world units
    public float depth = 10f; // The depth (z-axis) at which the sprite should be placed

    void Start()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        // Calculate the world position for the left side of the screen with offset
        // Calculate the left edge of the screen in world coordinates
        Vector3 leftEdgeViewport = new Vector3(0, 0.5f, depth);
        Vector3 leftPosition = mainCamera.ViewportToWorldPoint(leftEdgeViewport);

        // Set the sprite's position with the xOffset and depth
        transform.position = new Vector3(leftPosition.x + xOffset, transform.position.y, transform.position.z);
    }
}
