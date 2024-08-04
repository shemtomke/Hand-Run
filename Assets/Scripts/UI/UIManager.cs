using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI/Panels")]
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject selectCharacterUI;
    [SerializeField] GameObject menuUI;

    [Header("Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button privacyPolicyButton;
    [SerializeField] Button selectCharacterButton;

    GameManager gameManager;
    FlameManager flameManager;
    private void Start()
    {
        flameManager = FindObjectOfType<FlameManager>();
        gameManager = FindObjectOfType<GameManager>();

        startButton.onClick.AddListener(() =>
        {
            menuUI.SetActive(false);
            selectCharacterUI.SetActive(true);
        });
        selectCharacterButton.onClick.AddListener(() =>
        {
            mainMenuUI.SetActive(false);
            gameUI.SetActive(true);
            gameManager.SetStartGame(true);
            flameManager.GenerateFlameBalls();
        });
    }
}
