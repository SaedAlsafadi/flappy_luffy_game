using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenuScript : MonoBehaviour
{
    public GameObject levelSelectionPanel;
    public TextMeshProUGUI selectedLevelText;
    public ShopUIManager shopUIManager;
    
    void Start()
    {
        // Hide level selection panel at start
        if (levelSelectionPanel != null)
        {
            levelSelectionPanel.SetActive(false);
        }
        
        // Load saved level preference
        if (PlayerPrefs.HasKey("SelectedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SelectedLevel");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetDifficulty(savedLevel);
            }
        }
        
        UpdateLevelDisplay();
    }
    
    public void PlayGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("SampleScene");
    }
    
    public void OpenLevelSelection()
    {
        if (levelSelectionPanel != null)
        {
            levelSelectionPanel.SetActive(true);
        }
    }
    
    public void CloseLevelSelection()
    {
        if (levelSelectionPanel != null)
        {
            levelSelectionPanel.SetActive(false);
        }
    }
    
    public void SelectLevel(int level)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetDifficulty(level);
        }
        UpdateLevelDisplay();
        CloseLevelSelection();
    }
    
    void UpdateLevelDisplay()
    {
        if (selectedLevelText != null && GameManager.Instance != null)
        {
            int currentLevel = GameManager.Instance.GetSelectedLevel();
            string levelName = currentLevel == 1 ? "Easy" : "Hard";
            selectedLevelText.text = "Current Level: " + levelName;
        }
    }
    
    public void OpenShop()
    {
        if (shopUIManager != null)
        {
            shopUIManager.OpenShop();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}