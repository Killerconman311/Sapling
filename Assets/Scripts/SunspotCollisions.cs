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
    public ObjectGlow objectGlow;
    public SkinnedMeshRenderer playerBody;
    public SkinnedMeshRenderer playerRHand;
    public SkinnedMeshRenderer playerLHand;
    public SkinnedMeshRenderer playerLeaf;
    public SkinnedMeshRenderer playerFlowerStem;
    public SkinnedMeshRenderer playerBulb;
    public SkinnedMeshRenderer[] playerMeshes;
    private bool leveledUp = false;
    private AudioSource audioSource;
    private bool soundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sappy");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Leveled Up: "+leveledUp);
        if(leveledUp){
            if (!soundPlayed)
            {
                audioSource.PlayOneShot(audioSource.clip);
                soundPlayed = true;
            }
            StopCoroutine(LevelUpEffect(0.5f, 0.25f));
        }
        if(playerIsColliding && !leveledUp){
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        if(timer >= chargeTime){
            if(playerAbilities != null){
                leveledUp = true;
                playerAbilities.SetAbilityLevel(level);
                StopCoroutine(LevelUpEffect(0.5f, 0.25f));
            }
        }
        if(!playerIsColliding){
            timer = 0f;
        }
        
    }
    IEnumerator LevelUpEffect( float oscilationRate, float glowSpeed )
    {
        if(!objectGlow.godEnabled){
            while(playerIsColliding && !leveledUp){
                //Debug.Log("Level Up Started");
                foreach (SkinnedMeshRenderer mesh in playerMeshes)
                {
                    objectGlow.SetGlow(mesh.sharedMaterial, -40f);
                }
                    // objectGlow.SetGlow(playerBody.material, -40f);
                    // objectGlow.SetGlow(playerLeaf.material, -40f);
                    // objectGlow.SetGlow(playerLHand.material, -40f);
                    // objectGlow.SetGlow(playerRHand.material, -40f);
                yield return new WaitForSeconds( oscilationRate );
                foreach (SkinnedMeshRenderer mesh in playerMeshes)
                {
                    objectGlow.SetGlow(mesh.sharedMaterial, -0.5f);
                }
                    // objectGlow.SetGlow(playerBody.material, -0.5f);
                    // objectGlow.SetGlow(playerLeaf.material, -0.5f);
                    // objectGlow.SetGlow(playerLHand.material, -0.5f);
                    // objectGlow.SetGlow(playerRHand.material, -0.5f);
                yield return new WaitForSeconds( oscilationRate );
            }
            //Debug.Log("Level Up Ended");
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = true;
            playerAbilities = player.GetComponent<PlayerAbilities>();
            if(playerAbilities.abilityLevel != level){
                leveledUp = false;
                soundPlayed = false;
            }
            if(!leveledUp){
                StartCoroutine(LevelUpEffect(0.5f, 0.25f));
            }
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = false;
            playerAbilities = null;
            if(!objectGlow.godEnabled){
                foreach (SkinnedMeshRenderer mesh in playerMeshes)
                {
                    objectGlow.ResetGlow(mesh.sharedMaterial);
                }
            }
            // objectGlow.ResetGlow(playerBody.material);
            // objectGlow.ResetGlow(playerLeaf.material);
            // objectGlow.ResetGlow(playerLHand.material);
            // objectGlow.ResetGlow(playerRHand.material);
        }
    }
}
