using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Transform characterGridContainer;
    [SerializeField] private GameObject characterButtonPrefab;
    [SerializeField] private Image characterPreviewImage;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button closeButton;

    private int currentPreviewCharacterId = -1;
    private List<Button> characterButtons = new List<Button>();

    private void Start()
    {
        // Initialize shop UI
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseShop);
        }

        if (selectButton != null)
        {
            selectButton.onClick.AddListener(SelectCharacter);
        }

        // Populate character list
        PopulateCharacterList();

        // Show first character in preview
        if (CharacterManager.Instance != null)
        {
            var characters = CharacterManager.Instance.GetAllCharacters();
            if (characters.Count > 0)
            {
                ShowCharacterPreview(characters[0].characterId);
            }
        }
    }

    private void PopulateCharacterList()
    {
        if (CharacterManager.Instance == null)
        {
            Debug.LogError("CharacterManager instance not found!");
            return;
        }

        var characters = CharacterManager.Instance.GetAllCharacters();

        // Clear existing buttons
        foreach (Transform child in characterGridContainer)
        {
            Destroy(child.gameObject);
        }
        characterButtons.Clear();

        // Create character selection buttons
        foreach (var character in characters)
        {
            GameObject buttonObj = Instantiate(characterButtonPrefab, characterGridContainer);
            Button button = buttonObj.GetComponent<Button>();
            
            if (button != null)
            {
                characterButtons.Add(button);
                
                // Set button image
                Image buttonImage = buttonObj.GetComponent<Image>();
                if (buttonImage != null && character.sprite != null)
                {
                    buttonImage.sprite = character.sprite;
                }

                // Add click listener
                int characterId = character.characterId;
                button.onClick.AddListener(() => ShowCharacterPreview(characterId));
            }
        }
    }

    private void ShowCharacterPreview(int characterId)
    {
        currentPreviewCharacterId = characterId;

        var character = CharacterManager.Instance.GetCharacterById(characterId);
        if (character != null)
        {
            if (characterPreviewImage != null && character.sprite != null)
            {
                characterPreviewImage.sprite = character.sprite;
            }

            if (characterNameText != null)
            {
                characterNameText.text = character.characterName;
            }

            // Highlight selected button
            HighlightSelectedButton(characterId);
        }
    }

    private void HighlightSelectedButton(int characterId)
    {
        int selectedCharacterId = CharacterManager.Instance.GetSelectedCharacterId();

        for (int i = 0; i < characterButtons.Count; i++)
        {
            Button button = characterButtons[i];
            var characters = CharacterManager.Instance.GetAllCharacters();
            
            if (i < characters.Count && characters[i].characterId == characterId)
            {
                // Highlight this button
                Image buttonImage = button.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = Color.yellow;
                }
            }
            else
            {
                // Normal color
                Image buttonImage = button.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = Color.white;
                }
            }
        }
    }

    private void SelectCharacter()
    {
        if (currentPreviewCharacterId >= 0)
        {
            CharacterManager.Instance.SelectCharacter(currentPreviewCharacterId);
            HighlightSelectedButton(currentPreviewCharacterId);
            Debug.Log("Character selected: " + currentPreviewCharacterId);
        }
    }

    public void OpenShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
        }
    }

    public void CloseShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }
}