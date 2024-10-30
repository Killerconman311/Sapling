using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private int abilityLevel;
    private int previousLevel;
    public static bool canGlide;
    public static bool canGrab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(previousLevel!=abilityLevel){
            SetAbilities();
        }
    }
    public void SetAbilityLevel(int level){
        previousLevel = abilityLevel;
        abilityLevel = level;
    }
    public void SetAbilities(){
        switch(abilityLevel){
            case 0: {
                canGlide = false;
                canGrab = false;
            }break;
            case 1: {
                canGlide = true;
            }break;
            case 2: {
                canGrab = false;
            }break;
        }
    }

}
