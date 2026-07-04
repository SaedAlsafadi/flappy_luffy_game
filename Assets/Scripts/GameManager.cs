using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public enum DifficultyLevel
    {
        Easy = 1,
        Hard = 2
    }
    
    public DifficultyLevel selectedLevel = DifficultyLevel.Easy;
    
    // Difficulty settings
    [System.Serializable]
    public class LevelSettings
    {
        public float birdFlapStrength;
        public float pipeSpeed;
        public float pipeGapSize;
        public float spawnRate;
    }
    
    public LevelSettings easySettings = new LevelSettings
    {
        birdFlapStrength = 10f,
        pipeSpeed = 3f,
        pipeGapSize = 8f,  // Increased for larger gap
        spawnRate = 3f
    };
    
    public LevelSettings hardSettings = new LevelSettings
    {
        birdFlapStrength = 12f,
        pipeSpeed = 7f,
        pipeGapSize = 5f,  // Increased for playable gap
        spawnRate = 2f
    };
    
    void Awake()
    {
        // Singleton pattern to persist between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Load saved difficulty level (for when testing directly from game scene)
            int savedLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
            selectedLevel = (DifficultyLevel)savedLevel;
            Debug.Log($"GameManager initialized with difficulty: {selectedLevel}");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public LevelSettings GetCurrentLevelSettings()
    {
        return selectedLevel == DifficultyLevel.Easy ? easySettings : hardSettings;
    }
    
    public void SetDifficulty(int level)
    {
        selectedLevel = (DifficultyLevel)level;
        PlayerPrefs.SetInt("SelectedLevel", level);
        PlayerPrefs.Save();
    }
    
    public int GetSelectedLevel()
    {
        return (int)selectedLevel;
    }
}