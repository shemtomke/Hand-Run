using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public List<string> messages;
    public Text wordText;
    int currentMessage = -1;
    public void NextMessage()
    {
        // Increment the message index
        currentMessage++;

        // Check if the index is within bounds
        if (currentMessage < messages.Count)
        {
            // Update the wordText with the current message
            wordText.text = messages[currentMessage];
        }
        else
        {
            // Reset to -1 if at the end of the list (optional)
            currentMessage = -1;
            wordText.text = ""; // Clear text or handle as needed
        }
    }
    public void ShowText()
    {
        wordText.text = messages[currentMessage];
    }
    public void EmptyText()
    {
        wordText.text = "";
    }
}
