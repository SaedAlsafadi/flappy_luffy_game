using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    // Score tracking - separated into coins and pipes
    public int coinsCollected = 0;
    public int pipesPassed = 0;
    
    // UI References - TextMeshPro (preferred)
    public TextMeshProUGUI coinsText;      // Top-left (TextMeshPro)
    public TextMeshProUGUI pipesText;      // Top-right (TextMeshPro)
    public TextMeshProUGUI timerText;      // Timer display (TextMeshPro)
    
    // UI References - Legacy Text component (alternative)
    public Text coinsTextLegacy;           // Top-left (Legacy Text)
    public Text pipesTextLegacy;           // Top-right (Legacy Text)
    public Text timerTextLegacy;           // Timer display (Legacy Text)
    
    public GameObject gameOverScreen;
    public GameObject winScreen;
    
    [Header("Win Conditions")]
    public bool useTimeBasedWin = false;  // Toggle between time-based or score-based
    public float winTimeInSeconds = 60f;  // Win after 60 seconds
    public int winScore = 20;             // Win after passing 20 pipes
    
    private float gameTimer = 0f;
    private bool gameIsActive = true;
    private bool hasWon = false;
    
    // Win screen UI references (for displaying final scores)
    private TextMeshProUGUI winCoinsText;
    private TextMeshProUGUI winPipesText;
    private Text winCoinsTextLegacy;
    private Text winPipesTextLegacy;
    
    // Reward system reference
    private RewardSystem rewardSystem;
 
    void Start()
    {
        UpdateScoreDisplay();
        
        // Find reward system
        rewardSystem = FindObjectOfType<RewardSystem>();
        
        // Find win screen text displays if they exist
        if (winScreen != null)
        {
            // Try to find TextMeshPro components first
            winCoinsText = winScreen.transform.Find("CoinsCountText")?.GetComponent<TextMeshProUGUI>();
            winPipesText = winScreen.transform.Find("PipesCountText")?.GetComponent<TextMeshProUGUI>();
            
            // If TextMeshPro not found, try legacy Text components
            if (winCoinsText == null)
                winCoinsTextLegacy = winScreen.transform.Find("CoinsCountText")?.GetComponent<Text>();
            if (winPipesText == null)
                winPipesTextLegacy = winScreen.transform.Find("PipesCountText")?.GetComponent<Text>();
        }
    }
    
    void Update()
    {
        if (!gameIsActive || hasWon)
            return;
        
        // Update timer for time-based win condition
        if (useTimeBasedWin)
        {
            gameTimer += Time.deltaTime;
            
            // Update timer display
            float remainingTime = Mathf.Max(0, winTimeInSeconds - gameTimer);
            string timerDisplay = remainingTime.ToString("F1");
            
            if (timerText != null)
                timerText.text = timerDisplay;
            if (timerTextLegacy != null)
                timerTextLegacy.text = timerDisplay;
            
            if (gameTimer >= winTimeInSeconds)
            {
                TriggerWin();
            }
        }
        
        // Check score-based win condition
        if (!useTimeBasedWin && pipesPassed >= winScore)
        {
            TriggerWin();
        }
    }
 
    public void AddCoin()
    {
        if (!gameIsActive || hasWon)
            return;
            
        coinsCollected++;
        UpdateScoreDisplay();
        
        // Check for shield reward
        if (rewardSystem != null)
        {
            rewardSystem.CheckShieldReward(coinsCollected);
        }
        
        Debug.Log($"Coin collected! Total: {coinsCollected}");
    }
    
    public void AddPipe()
    {
        if (!gameIsActive || hasWon)
            return;
            
        pipesPassed++;
        UpdateScoreDisplay();
        
        // Check for cup reward
        if (rewardSystem != null)
        {
            rewardSystem.CheckCupReward(pipesPassed);
        }
        
        Debug.Log($"Pipe passed! Total: {pipesPassed}");
    }
    
    private void UpdateScoreDisplay()
    {
        // Update coins display
        string coinsDisplay = "Coins: " + coinsCollected.ToString();
        if (coinsText != null)
            coinsText.text = coinsDisplay;
        if (coinsTextLegacy != null)
            coinsTextLegacy.text = coinsDisplay;
            
        // Update pipes display
        string pipesDisplay = "Pipes: " + pipesPassed.ToString();
        if (pipesText != null)
            pipesText.text = pipesDisplay;
        if (pipesTextLegacy != null)
            pipesTextLegacy.text = pipesDisplay;
    }
    
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        if (!gameIsActive || hasWon)
            return;
        
        // For backward compatibility - if called, assume it's a pipe
        if (scoreToAdd == 1)
        {
            AddPipe();
        }
        else if (scoreToAdd == 5)
        {
            AddCoin();
        }
    }
 
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
    public void gameOver()
    {
        if (hasWon)
            return;  // Don't trigger game over if already won
            
        gameIsActive = false;
        gameOverScreen.SetActive(true);
        
        // Play game over sound with slight delay after collision sound
        if (AudioManager.Instance != null)
        {
            StartCoroutine(PlayGameOverSoundDelayed(0.3f));
        }
    }
    
    private IEnumerator PlayGameOverSoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlayGameOverSound();
    }
    
    private void TriggerWin()
    {
        hasWon = true;
        gameIsActive = false;
        
        // Update win screen with final scores
        if (winScreen != null)
        {
            string winCoinsDisplay = "Coins: " + coinsCollected.ToString();
            string winPipesDisplay = "Pipes: " + pipesPassed.ToString();
            
            // Update TextMeshPro components
            if (winCoinsText != null)
                winCoinsText.text = winCoinsDisplay;
            if (winPipesText != null)
                winPipesText.text = winPipesDisplay;
            
            // Update legacy Text components
            if (winCoinsTextLegacy != null)
                winCoinsTextLegacy.text = winCoinsDisplay;
            if (winPipesTextLegacy != null)
                winPipesTextLegacy.text = winPipesDisplay;
                
            winScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Win screen is not assigned!");
        }
        
        // Play win sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayWinSound();
        }
        
        Debug.Log($"Player Won! Coins: {coinsCollected}, Pipes: {pipesPassed}");
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    
    // Public getters for score data
    public int GetCoinsCollected() => coinsCollected;
    public int GetPipesPassed() => pipesPassed;
}
