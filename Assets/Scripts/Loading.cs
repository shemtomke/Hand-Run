using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Text messageText;
    private void Start()
    {
        StartCoroutine(ShowText());
    }
    IEnumerator ShowText()
    {
        messageText.text = "Put Sound ON!".ToUpper();

        yield return new WaitForSeconds(3f);

        messageText.text = "The game’s purpose is to show human suffering and struggle and not to make fun of anyone.".ToUpper();

        yield return new WaitForSeconds(9f);

        SceneManager.LoadScene(1);
    }
}
