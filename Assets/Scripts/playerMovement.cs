using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private CapsuleCollider coll;
    private CharacterAnimations animHandler;
    public float heightTest = 0.1f;

    [Header("Movement Variables")]
    public float moveSpeed = 5f; 
    public float jumpForce = 5f; 
    public float gravity = -9.81f;  
    public LayerMask groundLayer; 

    private Vector3 movementInput;
    private Vector3 cameraRelativeInput;
    private bool isGrounded;
    private bool isJumping;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        animHandler = GetComponentInChildren<CharacterAnimations>();
        isJumping = false;
        rb.freezeRotation = true;
    }

    void Update()
    {
        Debug.Log("isGrounded: " + isGrounded);
    Debug.Log("isJumping: " + isJumping);
    Debug.Log("rb.velocity.y: " + rb.velocity.y);
        GetInput();
        CheckGroundStatus();

        if (isGrounded && Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
            animHandler.UpdateAnimationState("jumpStart");
            isJumping = true;
        }
        else if (!isGrounded && isJumping)
        {
            animHandler.UpdateAnimationState("inAir");
        }
        // check if player has recently landed after being in the air
        else if (isGrounded && rb.velocity.y == 0f && isJumping) // this is probably needing a logic fix to make animations work correctly.
        {
            animHandler.UpdateAnimationState("jumpLand");
            StartCoroutine(Delay(0.1f));
            isJumping = false;
        }

        HandleAnimations();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animHandler.UpdateAnimationState("idle_run", 0f);
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyGravity();
    }

    private void GetInput()
    {
        // Get input from WASD/Arrow keys
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Create movement vector based on input
        movementInput = new Vector3(horizontalInput, 0f, verticalInput);
        cameraRelativeInput = ConvertToCameraSpace(movementInput) * moveSpeed;
    }

    private void MovePlayer()
    {
        // Move player in the direction of input
        Vector3 movement = cameraRelativeInput * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate the player to face movement direction
        if (cameraRelativeInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * moveSpeed);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * Mathf.Abs(gravity), ForceMode.Acceleration);
        }
    }

    private void CheckGroundStatus()
    {
        // Check if the player is grounded
        RaycastHit hit;
        isGrounded = Physics.SphereCast(coll.bounds.center, coll.radius, Vector3.down, out hit, heightTest, groundLayer);
        Debug.DrawRay(coll.bounds.center, Vector3.down * heightTest, Color.red);
        Debug.Log("ground status: " + isGrounded);
    }

    private void HandleAnimations()
    {
        // Only update idle_run animation if the player is grounded and not jumping
        if (isGrounded && !isJumping)
        {
            float inputMagnitude = Mathf.Clamp(cameraRelativeInput.magnitude, 0f, 0.5f);
            animHandler.UpdateAnimationState("idle_run", inputMagnitude);
        }
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
