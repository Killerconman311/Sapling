using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float moveSpeed = 5f; // Speed of the player
    public float jumpForce = 5f; // Jump force of the player
    public float gravity = -5f; // Gravity of the player
    public float horizontalInput; // Input for horizontal movement  
    public float verticalInput; // Input for vertical movement  
    Vector3 playerInput; // Vector3 for player input
    Vector3 velocity; // Velocity for vertical movement


    private CharacterController controller; //variable for the character controller

    void Start()
    {
        // Initialize the controller
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from WASD 
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // movement direction
        playerInput.x = horizontalInput;
        playerInput.z = verticalInput;

        Vector3 cameraRelativeInput = ConvertToCameraSpace(playerInput) * moveSpeed;


        if (controller.isGrounded)
        {
            // Reset the vertical velocity when grounded
            velocity.y = 0f;

            // Check for spacebar input to jump
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
            }
            else
            {
                // Reset the vertical velocity when grounded and not jumping
                velocity.y = 0f;
            }
        }
        // Apply gravity to the velocity
        velocity.y += gravity * Time.deltaTime;

        // gotta blast
        controller.Move((cameraRelativeInput + velocity) * Time.deltaTime);
        // Rotate the character to face the direction of movement
        if (cameraRelativeInput != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cameraRelativeInput), Time.deltaTime * moveSpeed);
        }
    }
    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        //store Y value of the original vector to rotate
        float currentYValue = vectorToRotate.y;
        // Get the camera's forward and right vectors
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Project the vectors onto the XZ plane
        forward.y = 0f;
        right.y = 0f;

        // Normalize the vectors
        forward.Normalize();
        right.Normalize();

        // rotate the x and y vectortorotate valus to camera space
        Vector3 fowardZproduct = vectorToRotate.z * forward;
        Vector3 rightXproduct = vectorToRotate.x * right;

        // the sum of the two vectors is the vector in camera space
        Vector3 vectorRotatedToCameraSpace = fowardZproduct + rightXproduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;

    }

}

