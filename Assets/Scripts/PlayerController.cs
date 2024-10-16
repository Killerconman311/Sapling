using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMult = 1f;
    bool isGrounded;
    public float maxSpeed;    
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Vertical");
        float zInput = Input.GetAxis("Horizontal");
        
        if (Input.GetAxis("Vertical") != 0 && playerRb.velocity.magnitude < maxSpeed)
        {
            playerRb.AddRelativeForce(Vector3.forward * xInput * speedMult, ForceMode.Force);
            //playerRb.velocity += transform.forward * xInput * speedMult;
            //Debug.Log(playerRb.velocity);
        }
        if (Input.GetAxis("Horizontal") != 0 && playerRb.velocity.magnitude < maxSpeed)
        {
            playerRb.AddRelativeForce(Vector3.right * zInput * speedMult, ForceMode.Force);
            //Debug.Log(playerRb.velocity);
        }
        
    }
}
