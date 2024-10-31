using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip stepSound;
    [SerializeField] private float strideLength;
    private Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If the character has moved and the audio is not currently playing
        if (Vector3.Distance(transform.position, lastPosition) > strideLength && !audioSource.isPlaying)
        {
            // Randomly change the playback speed a small amount
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            // Randomly change the volume a small amount
            audioSource.volume = Random.Range(0.4f, 1f);
            // Play the step sound
            audioSource.PlayOneShot(stepSound);
            // Update lastPosition for the next frame
            lastPosition = transform.position;
        }
    }
}