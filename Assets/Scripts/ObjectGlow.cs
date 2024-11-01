using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlow : MonoBehaviour
{
    float glowLevel = 0f;
    Material material;
    public bool changingGlow = false;
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
