using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugPatrol : MonoBehaviour
{
    // Array of pairs of target positions
    [System.Serializable]
    public struct TargetPair
    {
        public Transform targetA;
        public Transform targetB;
    }
    
    public TargetPair[] targetPairs;
    public int targetIndex = 0; // Index to choose which pair of targets to move between
    public float speed = 5f; // Movement speed

    private Transform currentTarget;
    private float timer = 5;

    void Start()
    {
        // Initialize the current target to the first target in the selected pair
        if (targetPairs.Length > 0)
        {
            SetCurrentTarget(targetPairs[targetIndex].targetA);
        }
    }

    void Update()
    {
        if (targetPairs.Length == 0) return; // Exit if no target pairs are set
        if (targetIndex >= targetPairs.Length) targetIndex = 0; // Wrap index if out of range

        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Check if reached the current target
        if (transform.position == currentTarget.position)
        {
            // Switch to the other target in the current pair
            SetCurrentTarget(currentTarget == targetPairs[targetIndex].targetA 
                ? targetPairs[targetIndex].targetB 
                : targetPairs[targetIndex].targetA);
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SetTargetIndexRandom();
            timer = 5;
        }

        // Smoothly rotate towards the current target
        Vector3 direction = currentTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    void SetCurrentTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }

    public void SetTargetIndex(int index)
    {
        // Update target index and reset the current target
        if (index >= 0 && index < targetPairs.Length)
        {
            targetIndex = index;
            SetCurrentTarget(targetPairs[targetIndex].targetA); // Start at targetA for the new pair
        }
    }

    public void SetTargetIndexRandom()
    {
        // Choose a random target index
        SetTargetIndex(Random.Range(0, targetPairs.Length));
    }

}
