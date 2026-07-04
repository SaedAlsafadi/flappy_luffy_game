using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGapController : MonoBehaviour
{
    public Transform topPipe;
    public Transform bottomPipe;
    public float defaultGapSize = 4f;
    
    void Start()
    {
        AdjustGap();
    }
    
    void AdjustGap()
    {
        // Check if references are assigned
        if (topPipe == null || bottomPipe == null)
        {
            Debug.LogError("PipeGapController: Top Pipe or Bottom Pipe reference is missing!");
            return;
        }
        
        // Get gap size from GameManager or use default
        float gapSize = defaultGapSize;
        if (GameManager.Instance != null)
        {
            gapSize = GameManager.Instance.GetCurrentLevelSettings().pipeGapSize;
            Debug.Log($"Using gap size from GameManager: {gapSize}");
        }
        else
        {
            Debug.LogWarning("GameManager not found, using default gap size: " + defaultGapSize);
        }
        
        // Adjust the vertical position of pipes to create the gap
        // Top pipe moves UP, bottom pipe moves DOWN
        topPipe.localPosition = new Vector3(topPipe.localPosition.x, gapSize / 2, topPipe.localPosition.z);
        bottomPipe.localPosition = new Vector3(bottomPipe.localPosition.x, -gapSize / 2, bottomPipe.localPosition.z);
        
        Debug.Log($"Pipes adjusted - Top: {topPipe.localPosition.y}, Bottom: {bottomPipe.localPosition.y}");
    }
}