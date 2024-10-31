using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public int abilityLevel;
    private int previousLevel;
    public static bool canGlide;
    public static bool canGrab;
    public GameObject[] stems;
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
                stems[1].SetActive(false);
                stems[2].SetActive(false);
                stems[0].SetActive(true);    
            }break;
            case 1: {
                canGlide = true;
                stems[0].SetActive(false);
                stems[1].SetActive(true);
            }break;
            case 2: {
                canGrab = true;
                stems[1].SetActive(false);
                stems[2].SetActive(true);
            }break;
        }
    }

}
