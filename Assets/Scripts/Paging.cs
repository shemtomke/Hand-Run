using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paging : MonoBehaviour
{
    [SerializeField] private List<Text> textContents;
    [SerializeField] private List<Button> pageButtons;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button previousPageButton;

    private int currentPage = 0;

    private void Start()
    {
        nextPageButton?.onClick.AddListener(NextPage);
        previousPageButton?.onClick.AddListener(PreviousPage);
        UpdateUI();

        UpdatePageButtons();
        UpdateNavigationButtons();
    }
    private void UpdateUI()
    {
        // Set the current page active and others inactive
        for (int i = 0; i < textContents.Count; i++)
        {
            textContents[i].gameObject.SetActive(i == currentPage);
        }

        // Update the ScrollRect content to the current active text
        if (textContents.Count > 0)
        {
            scrollRect.content = textContents[currentPage].rectTransform;
            ResetScrollPosition();
        }
    }
    void UpdateNavigationButtons()
    {
        if (nextPageButton == null && previousPageButton == null)
            return;

        // Update button interactability based on current page
        nextPageButton.interactable = currentPage < textContents.Count - 1;
        previousPageButton.interactable = currentPage > 0;
    }
    void UpdatePageButtons()
    {
        for (int i = 0; i < pageButtons.Count; i++)
        {
            pageButtons[i].interactable = i != currentPage;
        }
    }
    private void ResetScrollPosition()
    {
        // Ensure the ScrollRect is reset to the top
        if (scrollRect.verticalScrollbar != null)
        {
            scrollRect.verticalScrollbar.value = 1; // Scroll to top
        }
        else
        {
            scrollRect.verticalNormalizedPosition = 1; // Scroll to top
        }
    }
    public void OpenPage(int index)
    {
        currentPage = index;
        UpdateUI();
        UpdatePageButtons();
    }
    private void NextPage()
    {
        if (currentPage < textContents.Count - 1)
        {
            currentPage++;
            UpdateUI();
            UpdateNavigationButtons();
        }
    }
    private void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateUI();
            UpdateNavigationButtons();
        }
    }
}
