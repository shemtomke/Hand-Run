using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [NonReorderable]
    public List<Character> characterList;
    Character selectedCharacter;
    int selectedCharacterIndex;

    [Header("Character Selection UI")]
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;
    [SerializeField] Button selectCharacterButton;
    [SerializeField] Button purchaseCharacterButton;
    [SerializeField] Text characterNameText;
    [SerializeField] Image characterImage;
    [SerializeField] List<GameObject> charactersListRenderer;

    private int currentIndex;

    DistanceManager distanceManager;
    ScoreManager scoreManager;
    CharacterManager characterManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        characterManager = FindObjectOfType<CharacterManager>();
        distanceManager = FindObjectOfType<DistanceManager>();

        nextButton.onClick.AddListener(NextCharacter);
        prevButton.onClick.AddListener(PreviousCharacter);
        selectCharacterButton.onClick.AddListener(SelectCharacter);

        currentIndex = 0;
        UpdateUI();
    }
    private void Update()
    {
        SetActiveRenderedCharacter(charactersListRenderer, currentIndex);
    }
    public void SetActiveRenderedCharacter(List<GameObject> charactersListRenderer, int currentIndex)
    {
        for (int i = 0; i < charactersListRenderer.Count; i++)
        {
            charactersListRenderer[i].SetActive(i == currentIndex);
        }
    }
    public Character GetSelectedCharacter() {  return selectedCharacter; }
    void NextCharacter()
    {
        if (currentIndex < characterList.Count - 1)
        {
            currentIndex++;
            UpdateUI();
        }
    }
    void PreviousCharacter()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
    }
    void SelectCharacter()
    {
        selectedCharacter = characterList[currentIndex];
        selectedCharacterIndex = currentIndex;
        GameObject player = Instantiate(selectedCharacter.characterPrefab);
        distanceManager.SetPlayerPosition(player.transform);

        // Initialize score and timer
        scoreManager.SetStartScore(GetSelectedCharacter().CharacterType);
    }
    private void UpdateUI()
    {
        characterNameText.text = characterList[currentIndex].characterName;
        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < characterList.Count - 1;

        selectCharacterButton.interactable = selectedCharacter != characterList[selectedCharacterIndex];
        selectCharacterButton.gameObject.SetActive(!characterList[currentIndex].isLocked);
        purchaseCharacterButton.gameObject.SetActive(characterList[currentIndex].isLocked);
    }
    // there is in-app purchase-purchase the female and mosquito character

    // the female and mosquito have to be purchased to be used.

    //the male character is second fastest, the female is the slowest and the mosquito is the fastest.

    // male character reduces distance from pick-up 0.3m,female by 0.2m and mosquito by 0.4.this speed is reduced on every run or running cycle

    //names of characters:
    // male-GYMTERNET BOY
    // female-HOT LOTION
    // mosquito-AREA 51 MOSQUITO

    // to buy female character with diamonds it costs 1000 diamonds and mosquito it's 1500 diamonds
}
