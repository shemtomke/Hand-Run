using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenView : MonoBehaviour
{
    float maxWidth, minWidth, center;
    private void Start()
    {
        minWidth = 0;
        maxWidth = Screen.width;
    }
    public float GetMaxWidth() {  return maxWidth; }
    public float GetMinWidth() { return minWidth; }
}
