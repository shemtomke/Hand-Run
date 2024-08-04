using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public List<string> messages;
    public Text wordText;
    int currentMessage = -1;
    public void NextMessage() {  currentMessage++; }
    public void ShowText()
    {
        wordText.text = messages[currentMessage];
    }
    public void EmptyText()
    {
        wordText.text = "";
    }
}
