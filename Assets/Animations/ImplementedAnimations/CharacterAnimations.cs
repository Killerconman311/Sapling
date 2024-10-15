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
    inAir
    jumpLand

    The inputMagnitude is used for the blend tree in the animator and uses normalized values (0-1)
    idle = 0
    walking = 0.5
    run = 1
    */

    private Animator anim;
    private enum MoveState { idle_run = 0, jumpStart = 1, inAir = 2, jumpLand = 3 }
    private MoveState currentState;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Call this to change the animation state
    public void UpdateAnimationState(string animationState, float inputMagnitude = 0f)
    {
        MoveState newState;

        switch (animationState)
        {
            case "idle_run":
                newState = MoveState.idle_run;
                anim.SetFloat("InputMagnitude", inputMagnitude);
                break;
            case "jumpStart":
                newState = MoveState.jumpStart;
                break;
            case "inAir":
                newState = MoveState.inAir;
                break;
            case "jumpLand":
                newState = MoveState.jumpLand;
                break;
            default:
                Debug.LogWarning("Invalid animation state: " + animationState);
                return; // Exit if an invalid state is passed
        }

        // Only change the state if it's different from the current state
        if (newState != currentState)
        {
            currentState = newState;
            anim.SetInteger("State", (int)newState);
            Debug.Log("Animation State changed to: " + animationState);
        }
    }
}


