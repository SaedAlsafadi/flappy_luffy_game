using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -45;
 
    // Start is called before the first frame update
    void Start()
    {
        // Apply difficulty settings
        if (GameManager.Instance != null)
        {
            moveSpeed = GameManager.Instance.GetCurrentLevelSettings().pipeSpeed;
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
