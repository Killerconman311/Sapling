using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunspotCollisions : MonoBehaviour
{
    private bool playerIsColliding = false;
    private GameObject player;
    private PlayerAbilities playerAbilities;
    private float timer = 0;
    public int level;
    public float chargeTime = 3f;

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
            if(playerAbilities != null){
                playerAbilities.SetAbilityLevel(level);
            }
        }
        if(!playerIsColliding){
            timer = 0f;
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = true;
            player = other.gameObject;
            playerAbilities = player.GetComponent<PlayerAbilities>();
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = false;
            player = null;
            playerAbilities = null;
        }
    }
}
