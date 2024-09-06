using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    
    public float moveSpeed = 5f; // Speed of the player movement

    void Update()
    {
        // Get input from WASD or arrow keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 move = new Vector3(moveX, 0f, moveZ);

        // Apply movement to the player
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }
}

