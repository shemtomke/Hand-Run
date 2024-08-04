using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [NonReorderable]
    public List<Character> characterList;
    Character selectedCharacter;
    [SerializeField] Button nextButton, prevButton;
    [SerializeField] Button selectCharacterButton;
    [SerializeField] Text characterNameText;
    [SerializeField] Image characterImage;

    private int currentIndex;
    private void Start()
    {
        nextButton.onClick.AddListener(NextCharacter);
        prevButton.onClick.AddListener(PreviousCharacter);
        selectCharacterButton.onClick.AddListener(SelectCharacter);

        currentIndex = 0;
        UpdateUI();
    }
    private void Update()
    {
        
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
    }
    private void UpdateUI()
    {
        characterNameText.text = characterList[currentIndex].characterName;
        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < characterList.Count - 1;
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
