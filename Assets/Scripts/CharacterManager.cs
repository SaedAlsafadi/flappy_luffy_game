using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    [System.Serializable]
    public class Character
    {
        public string characterName;
        public Sprite sprite;
        public int characterId;
    }

    [SerializeField] private List<Character> availableCharacters = new List<Character>();
    private int selectedCharacterId = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load saved character preference
        if (PlayerPrefs.HasKey("SelectedCharacter"))
        {
            selectedCharacterId = PlayerPrefs.GetInt("SelectedCharacter");
        }
    }

    /// <summary>
    /// Get the currently selected character
    /// </summary>
    public Character GetSelectedCharacter()
    {
        foreach (Character character in availableCharacters)
        {
            if (character.characterId == selectedCharacterId)
            {
                return character;
            }
        }
        
        // Return first character if selected one not found
        return availableCharacters.Count > 0 ? availableCharacters[0] : null;
    }

    /// <summary>
    /// Get all available characters
    /// </summary>
    public List<Character> GetAllCharacters()
    {
        return availableCharacters;
    }

    /// <summary>
    /// Select a character by ID
    /// </summary>
    public void SelectCharacter(int characterId)
    {
        foreach (Character character in availableCharacters)
        {
            if (character.characterId == characterId)
            {
                selectedCharacterId = characterId;
                PlayerPrefs.SetInt("SelectedCharacter", characterId);
                PlayerPrefs.Save();
                return;
            }
        }

        Debug.LogWarning("Character with ID " + characterId + " not found!");
    }

    /// <summary>
    /// Get character by ID
    /// </summary>
    public Character GetCharacterById(int characterId)
    {
        foreach (Character character in availableCharacters)
        {
            if (character.characterId == characterId)
            {
                return character;
            }
        }
        return null;
    }

    /// <summary>
    /// Get the ID of the selected character
    /// </summary>
    public int GetSelectedCharacterId()
    {
        return selectedCharacterId;
    }
}