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

    public SkinnedMeshRenderer[] playerMeshes;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sappy");
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
                StopCoroutine(LevelUpEffect(0.5f, 0.25f));
            }
        }
        if(!playerIsColliding){
            timer = 0f;
        }
    }
    IEnumerator LevelUpEffect( float oscilationRate, float glowSpeed )
    {
        while(playerIsColliding){
            Debug.Log("Level Up Started");
            foreach (SkinnedMeshRenderer mesh in playerMeshes)
            {
                objectGlow.EnableGlow(mesh.material, glowSpeed);
                yield return null;    
            }
            yield return new WaitForSeconds( oscilationRate );
            foreach (SkinnedMeshRenderer mesh in playerMeshes)
            {
                objectGlow.DisableGlow(mesh.material, glowSpeed);
                yield return null;    
            }
            yield return new WaitForSeconds( oscilationRate );
        }
        Debug.Log("Level Up Ended");
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = true;
            playerAbilities = player.GetComponent<PlayerAbilities>();
            StartCoroutine(LevelUpEffect(0.25f, 0.25f));
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            playerIsColliding = false;
            playerAbilities = null;
            foreach (SkinnedMeshRenderer mesh in playerMeshes)
            {
                StopCoroutine(LevelUpEffect(0.5f, 0.25f));
                objectGlow.ResetGlow(mesh.material);    
            }
        }
    }
}
