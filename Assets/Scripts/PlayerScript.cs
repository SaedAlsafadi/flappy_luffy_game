using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    private RewardSystem rewardSystem;
    
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        rewardSystem = FindObjectOfType<RewardSystem>();
        
        // Apply difficulty settings
        if (GameManager.Instance != null)
        {
            flapStrength = GameManager.Instance.GetCurrentLevelSettings().birdFlapStrength;
        }

        // Apply selected character sprite
        if (CharacterManager.Instance != null)
        {
            var selectedCharacter = CharacterManager.Instance.GetSelectedCharacter();
            if (selectedCharacter != null && selectedCharacter.sprite != null)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = selectedCharacter.sprite;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play collision sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayCollisionSound();
        }
        
        // Check if shield is active - if so, protect the bird
        if (rewardSystem != null && rewardSystem.IsShieldActive())
        {
            Debug.Log($"Shield protected the bird! Remaining time: {rewardSystem.GetShieldTimeRemaining():F1}s");
            return;  // Don't trigger game over
        }
        
        logic.gameOver();
        birdIsAlive = false;
    }
}
