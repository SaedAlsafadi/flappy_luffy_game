using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSystem : MonoBehaviour
{
    [Header("Cup Reward Settings")]
    public int silverCupPipesRequired = 10;
    public int goldCupPipesRequired = 20;
    
    [Header("Shield Reward Settings")]
    public int shieldCoinsRequired = 10;
    public float shieldDuration = 5f;
    
    [Header("UI References")]
    public Image cupDisplay;  // Image component to show cup (silver or gold)
    public TextMeshProUGUI shieldTimerDisplay;  // Display remaining shield time
    public Text shieldTimerDisplayLegacy;  // Legacy text component for shield timer
    
    [Header("Cup Sprites")]
    public Sprite silverCupSprite;
    public Sprite goldCupSprite;
    
    private int currentCupLevel = 0;  // 0 = none, 1 = silver, 2 = gold
    private float shieldTimeRemaining = 0f;
    private bool shieldActive = false;
    private LogicScript logic;
    private PlayerScript player;
    
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        
        // Hide cup initially
        if (cupDisplay != null)
            cupDisplay.enabled = false;
            
        // Hide shield timer initially
        if (shieldTimerDisplay != null)
            shieldTimerDisplay.enabled = false;
        if (shieldTimerDisplayLegacy != null)
            shieldTimerDisplayLegacy.enabled = false;
    }
    
    void Update()
    {
        // Update shield timer
        if (shieldActive && shieldTimeRemaining > 0)
        {
            shieldTimeRemaining -= Time.deltaTime;
            
            // Update shield timer display
            string shieldDisplay = "Shield: " + shieldTimeRemaining.ToString("F1") + "s";
            
            if (shieldTimerDisplay != null)
                shieldTimerDisplay.text = shieldDisplay;
            if (shieldTimerDisplayLegacy != null)
                shieldTimerDisplayLegacy.text = shieldDisplay;
                
            if (shieldTimeRemaining <= 0)
            {
                DeactivateShield();
            }
        }
    }
    
    /// <summary>
    /// Called by LogicScript when pipes are passed to check for cup rewards
    /// </summary>
    public void CheckCupReward(int pipesPassed)
    {
        // Check for silver cup (10 pipes)
        if (pipesPassed >= silverCupPipesRequired && currentCupLevel == 0)
        {
            ActivateSilverCup();
        }
        
        // Check for gold cup (20 pipes) - upgrade from silver
        if (pipesPassed >= goldCupPipesRequired && currentCupLevel == 1)
        {
            UpgradeToGoldCup();
        }
    }
    
    /// <summary>
    /// Called by LogicScript when coins are collected to check for shield reward
    /// </summary>
    public void CheckShieldReward(int coinsCollected)
    {
        if (coinsCollected >= shieldCoinsRequired && !shieldActive)
        {
            ActivateShield();
        }
    }
    
    private void ActivateSilverCup()
    {
        currentCupLevel = 1;
        
        if (cupDisplay != null && silverCupSprite != null)
        {
            cupDisplay.sprite = silverCupSprite;
            cupDisplay.enabled = true;
            Debug.Log("Silver Cup Unlocked! Passed 10 pipes!");
        }
    }
    
    private void UpgradeToGoldCup()
    {
        currentCupLevel = 2;
        
        if (cupDisplay != null && goldCupSprite != null)
        {
            cupDisplay.sprite = goldCupSprite;
            cupDisplay.enabled = true;
            Debug.Log("Gold Cup Unlocked! Passed 20 pipes!");
        }
    }
    
    private void ActivateShield()
    {
        shieldActive = true;
        shieldTimeRemaining = shieldDuration;
        
        if (shieldTimerDisplay != null)
            shieldTimerDisplay.enabled = true;
        if (shieldTimerDisplayLegacy != null)
            shieldTimerDisplayLegacy.enabled = true;
            
        Debug.Log($"Shield Activated for {shieldDuration} seconds!");
    }
    
    private void DeactivateShield()
    {
        shieldActive = false;
        shieldTimeRemaining = 0f;
        
        if (shieldTimerDisplay != null)
            shieldTimerDisplay.enabled = false;
        if (shieldTimerDisplayLegacy != null)
            shieldTimerDisplayLegacy.enabled = false;
            
        Debug.Log("Shield Deactivated!");
    }
    
    /// <summary>
    /// Check if shield is currently active (call from PlayerScript collision)
    /// </summary>
    public bool IsShieldActive()
    {
        return shieldActive;
    }
    
    /// <summary>
    /// Get current cup level (0 = none, 1 = silver, 2 = gold)
    /// </summary>
    public int GetCupLevel()
    {
        return currentCupLevel;
    }
    
    /// <summary>
    /// Get remaining shield time
    /// </summary>
    public float GetShieldTimeRemaining()
    {
        return shieldTimeRemaining;
    }
}