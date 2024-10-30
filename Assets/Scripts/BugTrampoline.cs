using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugTrampoline : MonoBehaviour
{
    public float bounceForce;
    public float gravity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            PlayerMovement.playerGravity = gravity;
            other.gameObject.GetComponent<PlayerMovement>().Jump(bounceForce);
        }
    }
}
