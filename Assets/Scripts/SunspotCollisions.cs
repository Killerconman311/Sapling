using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunspotCollisions : MonoBehaviour
{
    private bool playerIsColliding = false;
    private GameObject player;
    private float timer = 0;
    public int level;
    public float chargeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsColliding){
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        if(timer >= chargeTime){
            UnlockAbility();
        }
        if(!playerIsColliding){
            timer -= Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = true;
            player = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = false;
            player = null;
        }
    }
    private void UnlockAbility(){
        PlayerAbilities.abilityLevel = level;
    }
}
