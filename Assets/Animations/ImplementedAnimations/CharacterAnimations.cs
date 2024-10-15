using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    /* This script is used to change the animation state of the character
    to reference it, you can use the following code:

    //in class
    private CharacterAnimations animHandler;

    //in start
    animHandler = GetComponent<CharacterAnimations>();

    //to change animation state
    animHandler.UpdateAnimationState("-------", optional inputMagnitude); //default is 0f

    The following are the animation states that can be used:
    idle_run
    jumpStart
    jumpAir
    jumpLand

    The inputMagnitude is used for the blend tree in the animator and uses normalized values (0-1)
    idle = 0
    walking = 0.5
    run = 1
    */

    private Animator anim;
    private enum MoveState {idle_run, jumpStart, jumpAir, jumpLand}
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // call this to change the animation state
    // animationState is for the animation you want to change to
    // inputMagnitude is for the blend tree
    public void UpdateAnimationState( string animationState, float inputMagnitude = 0f)
    {
        MoveState state;
        switch (animationState)
        {
            case "idle_run":
                anim.SetInteger("State", (int)MoveState.idle_run); 
                anim.SetFloat("InputMagnitude", inputMagnitude);
                break;
            case "jumpStart":
                anim.SetInteger("State", (int)MoveState.jumpStart);
                break;
            case "jumpAir":
                anim.SetInteger("State", (int)MoveState.jumpAir);
                break;
            case "jumpLand":
                anim.SetInteger("State", (int)MoveState.jumpLand);
                break;

            default:
                Debug.LogWarning("Invalid animation state: " + animationState);
                break;
        }
        Debug.Log ("Animation State: " + animationState);
    }
}
