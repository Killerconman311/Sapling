using UnityEngine;
using UnityEngine.Audio;

public class AudioMixing : MonoBehaviour
{
    public static AudioMixing instance; // Static instance
    public AudioMixerSnapshot[] snapshots; // Array of snapshots
    public float transitionTime = 1.0f; // Transition time
    [SerializeField] private AudioMixer mixer; // Reference to the AudioMixer

    private void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToSnapshots(int[] snapshotIndices, float[] weights)
    {
        if (snapshotIndices.Length != weights.Length)
        {
            Debug.LogError("Snapshot indices and weights arrays must be the same length.");
            return;
        }

        AudioMixerSnapshot[] targetSnapshots = new AudioMixerSnapshot[snapshotIndices.Length];
        for (int i = 0; i < snapshotIndices.Length; i++)
        {
            targetSnapshots[i] = snapshots[snapshotIndices[i]];
        }

        mixer.TransitionToSnapshots(targetSnapshots, weights, transitionTime);
    }
}