using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private CapsuleCollider coll;
    private CharacterAnimations animHandler;
    public float heightTest = 0.1f;
    public GameObject glider;

    [Header("Movement Variables")]
    float moveSpeed = 10f; 
    float jumpForce = 15f; 
    public static float playerGravity;  
    public LayerMask groundLayer; 

    private Vector3 movementInput;
    private Vector3 cameraRelativeInput;

    [SerializeField]
    private float jumpButtonGracePeriod;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isGrounded;
    private bool isMoving;
    private bool isFalling;
    private bool isJumping;
    bool landed;
    private float horizontalInput;
    private float verticalInput;

    private float timeInAir; 
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        animHandler = GetComponentInChildren<CharacterAnimations>();
        animator = GetComponentInChildren<Animator>();
        isJumping = false;
        rb.freezeRotation = true;
    }

    void Update()
    {
        //Debug.Log("isGrounded: " + isGrounded);
        //Debug.Log("rb.velocity.y: " + rb.velocity.y);
        GetInput();
        if(cameraRelativeInput.magnitude > 0){
            MovePlayer();
        }else{
            animator.SetBool("isMoving", false);
        }
        
        CheckGroundStatus();
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);
            isFalling = false;
            
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                playerGravity = -9.81f;
                Jump(jumpForce);
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && rb.velocity.y < 0) || rb.velocity.y < -2)
            {
                animator.SetBool("isFalling", true);
                isFalling = true;
            }
        }
        if(isFalling && PlayerAbilities.canGlide){
            if(Input.GetButton("Jump")){
                glider.SetActive(true);
                rb.drag = 5f;
            }else{
                glider.SetActive(false);
                rb.drag = 0f;
            }
        }
        
        if(isGrounded){
            glider.SetActive(false);
            timeInAir = 0f;
        }

        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 20f;
            jumpForce = 25f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            moveSpeed = 10f;
            jumpForce = 15f;
        }
        //HandleAnimations();
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    private void GetInput()
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
        // Get input from WASD/Arrow keys
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Create movement vector based on input
        movementInput = new Vector3(horizontalInput, 0f, verticalInput);
        cameraRelativeInput = ConvertToCameraSpace(movementInput) * moveSpeed;
    }

    private void MovePlayer()
    {
        animator.SetBool("isMoving", true);
        // Move player in the direction of input
        Vector3 movement = cameraRelativeInput * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
        // Rotate the player to face movement direction
        if (cameraRelativeInput != Vector3.zero)
        {
            animator.SetBool("isMoving", true);

            Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void Jump(float force)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * playerGravity, ForceMode.Acceleration);
        }
    }

    private void CheckGroundStatus()
    {
        // Check if the player is grounded
        RaycastHit hit;
        isGrounded = Physics.SphereCast(coll.bounds.center, coll.radius, Vector3.down, out hit, heightTest, groundLayer);
        Debug.DrawRay(coll.bounds.center, Vector3.down * heightTest, Color.red);
        //Debug.Log("ground status: " + isGrounded);
    }

    private void HandleAnimations()
    {
        // Only update idle_run animation if the player is grounded and not jumping
        if (isMoving && !isJumping)
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
