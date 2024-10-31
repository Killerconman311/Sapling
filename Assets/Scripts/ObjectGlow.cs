using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlow : MonoBehaviour
{
    float glowLevel = 0f;
    Material material;
    bool changingGlow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(changingGlow){
            material.SetFloat("_Shades", glowLevel);
        }
    }

    public void EnableGlow(Material target, float glowSpeed){
        material = target;
        StartCoroutine( ChangeGlow( 1f, -30f, glowSpeed ) );
    }
    public void DisableGlow(Material target, float glowSpeed){
        material = target;
        StartCoroutine( ChangeGlow( -30f, 1f, glowSpeed ) );
    }
    public void ResetGlow(Material target){
        material = target;
        material.SetFloat("_Shades", -0.5f);
    }
    public void SetGlow(Material target, float glow){
        material = target;
        material.SetFloat("_Shades", glow);
    }
    IEnumerator ChangeGlow( float v_start, float v_end, float duration )
    {
        changingGlow = true;
        float elapsed = 0.0f;
        while (elapsed < duration )
        {
            glowLevel = Mathf.Lerp( v_start, v_end, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }
        glowLevel = v_end;
        changingGlow = false;
    }
}
