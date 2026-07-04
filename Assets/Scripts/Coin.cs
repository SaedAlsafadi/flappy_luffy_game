using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moveSpeed = 5;
    public int scoreValue = 5; // Points awarded for collecting this coin
    private LogicScript logic;
    private bool hasBeenCollected = false;

    void Start()
    {
        // Find the LogicScript
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
        // If GameManager exists and hasn't already set speed, update from it
        if (GameManager.Instance != null && moveSpeed == 5)
        {
            moveSpeed = GameManager.Instance.GetCurrentLevelSettings().pipeSpeed;
        }
        
        Debug.Log($"Coin started - moveSpeed: {moveSpeed}, Position: {transform.position}");
    }

    // Update is called once per frame
    void Update()
    {
        // Move coin to the left (following pipe movement)
        transform.position = transform.position + (Vector3.left * moveSpeed * Time.deltaTime);

        // Destroy coin if it goes off-screen to the left
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collected the coin
        if (collision.CompareTag("Player") && !hasBeenCollected)
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        hasBeenCollected = true;

        // Increment coin counter (no longer adds to score)
        if (logic != null)
        {
            logic.AddCoin();
            
            // Play coin collection sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayCoinSound();
            }
        }

        // Create a simple visual effect (optional - coins fade out)
        StartCoroutine(FadeOutCoin());
    }

    IEnumerator FadeOutCoin()
    {
        // Get the sprite renderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        // Fade out over 0.2 seconds
        float fadeTime = 0.2f;
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Destroy the coin
        Destroy(gameObject);
    }
}