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
    private bool isGrounded;
    private bool isJumping;
    private float horizontalInput;
    private float verticalInput;

    private float timeInAir; 

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
        //Debug.Log("isGrounded: " + isGrounded);
        //Debug.Log("rb.velocity.y: " + rb.velocity.y);
        GetInput();
        CheckGroundStatus();
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded){
                playerGravity = -9.81f;
                Jump(jumpForce);
                animHandler.UpdateAnimationState("jumpStart");
            }
            
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
            //isJumping = false;
        }
        if (isJumping && PlayerAbilities.canGlide){
            //Debug.Log("Jumping: " +isJumping);
            timeInAir += Time.deltaTime;
            //Debug.Log(Input.GetButton("Jump"));
            if(timeInAir >= 0.7f && Input.GetButton("Jump")){
                //Debug.Log("Gliding");
                glider.SetActive(true);
                rb.drag = 5f;
                //Debug.Log("Gravity");
            }else{
                glider.SetActive(false);
                rb.drag = 0f;
            }
        }
        if(isGrounded){
            glider.SetActive(false);
            timeInAir = 0f;
        }

        HandleAnimations();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 20f;
            jumpForce = 25f;
            animHandler.UpdateAnimationState("idle_run", 0f);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            moveSpeed = 10f;
            jumpForce = 15f;
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
        //Debug.Log("CameraRelative: "+cameraRelativeInput);
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

    public void Jump(float force)
    {
        Debug.Log("isJumping: " + isJumping);
        isJumping = true;
        Debug.Log("isJumping: " + isJumping);
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
