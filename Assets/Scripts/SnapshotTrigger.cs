using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotTrigger : MonoBehaviour
{
    [SerializeField] private int[] snapshotIndices;
    [SerializeField] private float[] weights;
    private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
        AudioMixing.instance.TransitionToSnapshots(snapshotIndices, weights);
        Debug.Log("Player has entered the trigger");
    }
}
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            AudioMixing.instance.TransitionToSnapshots(new int[] {0}, new float[] {1.0f});
            Debug.Log("Player has left the trigger");
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
        AudioMixing.instance.TransitionToSnapshots(snapshotIndices, weights);
        Debug.Log("Player has entered the trigger");
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            AudioMixing.instance.TransitionToSnapshots(new int[] {0}, new float[] {1.0f});
            Debug.Log("Player has left the trigger");
        }
    }
}

