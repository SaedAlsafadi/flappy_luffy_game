using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour 
    {
    public GameObject pipe;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;

    void Start()
    {
        // Apply difficulty settings
        if (GameManager.Instance != null)
        {
            spawnRate = GameManager.Instance.GetCurrentLevelSettings().spawnRate;
            // Adjust pipe gap based on difficulty
            // Note: You'll need to adjust the pipe prefab's gap in the inspector
            // or modify the pipe prefab to use the gap size from GameManager
        }
        spawnPipe(); 
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
            spawnPipe();
            timer = 0;
        }
    }
    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
 
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
