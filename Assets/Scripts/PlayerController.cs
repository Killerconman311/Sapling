using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float jumpSpeed;
    
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpButtonGracePeriod;

    [SerializeField]
    private float jumpHorizontalSpeed;

    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        
        if (!Input.GetKey(KeyCode.LeftShift) || !Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }

        animator.SetFloat("inputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        Vector3 cameraRelativeInput = ConvertToCameraSpace(movementDirection) * moveSpeed;
        movementDirection = Quaternion.AngleAxis(cameraRelativeInput.z, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);
            
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("isFalling", true);
            }
        }

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (isGrounded == false)
        {
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        // if (isGrounded)
        // {
        //     Vector3 velocity = animator.deltaPosition;
        //     velocity.y = ySpeed * Time.deltaTime;

        //     characterController.Move(velocity);
        // }
    }
    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        // Get camera directions
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Flatten camera directions on XZ plane
        forward.y = 0f;
        right.y = 0f;

        // Normalize vectors
        forward.Normalize();
        right.Normalize();

        // Calculate camera-relative movement
        return vectorToRotate.z * forward + vectorToRotate.x * right;
    }
}
