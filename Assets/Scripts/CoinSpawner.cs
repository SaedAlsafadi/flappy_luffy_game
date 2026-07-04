using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;
    
    // Coin spawn probability (0-1, where 1 = always spawn a coin with each pipe)
    public float coinSpawnChance = 0.7f;
    
    // Range for coin Y position offset within the gap
    public float coinVerticalRange = 1f;
    
    // Cache for gap size
    private float cachedGapSize = 4f;

    void Start()
    {
        // Apply difficulty settings
        if (GameManager.Instance != null)
        {
            spawnRate = GameManager.Instance.GetCurrentLevelSettings().spawnRate;
            cachedGapSize = GameManager.Instance.GetCurrentLevelSettings().pipeGapSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnCoin();
            timer = 0;
        }
    }

    void SpawnCoin()
    {
        // Only spawn coin based on probability
        if (Random.value > coinSpawnChance)
            return;

        // Spawn coin STRICTLY WITHIN the pipe gap
        // Gap is centered at Y=0, with size = gapSize
        // So gap ranges from -gapSize/2 to +gapSize/2
        float gapHalfSize = cachedGapSize / 2f;
        
        // Spawn coin at random Y position within the gap (with small margin)
        float margin = 0.3f; // Small margin to ensure it's safely inside
        float minGapY = -gapHalfSize + margin;
        float maxGapY = gapHalfSize - margin;
        
        float coinY = Random.Range(minGapY, maxGapY);
        
        // Slight additional variation (much smaller to stay in gap)
        coinY += Random.Range(-coinVerticalRange, coinVerticalRange);
        
        // Clamp to ensure it stays within gap
        coinY = Mathf.Clamp(coinY, minGapY, maxGapY);
        
        // Spawn coin at the same X position as this spawner (off-screen to the right)
        GameObject spawnedCoin = Instantiate(coinPrefab, new Vector3(transform.position.x, coinY, 0), Quaternion.identity);
        
        // Debug log to verify spawn position
        Debug.Log($"Coin spawned at: X={transform.position.x}, Y={coinY}, Gap Range: [{minGapY:F2}, {maxGapY:F2}]");
        
        // Apply current pipe speed to coin
        Coin coinScript = spawnedCoin.GetComponent<Coin>();
        if (coinScript != null && GameManager.Instance != null)
        {
            coinScript.moveSpeed = GameManager.Instance.GetCurrentLevelSettings().pipeSpeed;
        }
    }
}