using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectGlow : MonoBehaviour
{
    float glowLevel = 0f;
    Material material;
    public bool changingGlow = false;

    [SerializeField] private SkinnedMeshRenderer[] playerMeshes;
    [SerializeField] private Renderer[] gliderMeshes; 
    public GameObject player;
    public bool godEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableGlow(Material target, float glowSpeed){
        StartCoroutine( ChangeGlow( -40f, glowSpeed, target ) );
    }
    public void DisableGlow(Material target, float glowSpeed){
        StartCoroutine( ChangeGlow( -0.5f, glowSpeed, target) );
    }
    public void ResetGlow(Material target){
        material = target;
        material.SetFloat("_Shades", -0.5f);
    }
    public void SetGlow(Material target, float glow){
        material = target;
        material.SetFloat("_Shades", glow);
    }
    IEnumerator ChangeGlow( float v_end, float duration, Material target )
    {
        if( !godEnabled ){
            float v_start = target.GetFloat("_Shades");
            changingGlow = true;
            float elapsed = 0.0f;
            while (elapsed < duration )
            {
                glowLevel = Mathf.Lerp( v_start, v_end, elapsed / duration );
                elapsed += Time.deltaTime;
                target.SetFloat("_Shades", glowLevel);
                yield return null;
            }
            glowLevel = v_end;
            changingGlow = false;
        }
        
    }
    public void BecomeGod(Toggle toggle){
        
        bool isGod = toggle.isOn;
        godEnabled = isGod;
        Debug.Log("IsGod: " + isGod);
        if(isGod){
            foreach(SkinnedMeshRenderer mesh in playerMeshes){
                Material target = mesh.material;
                SetGlow(target, 0f);
            }foreach(Renderer mesh in gliderMeshes){
                Material target = mesh.material;
                SetGlow(target, 0f);
            }
        }else{
            foreach(SkinnedMeshRenderer mesh in playerMeshes){
                Material target = mesh.material;
                SetGlow(target, -0.5f);
            }
            foreach(Renderer mesh in gliderMeshes){
                Material target = mesh.material;
                SetGlow(target, -0.5f);
            }
        }
    }
}
