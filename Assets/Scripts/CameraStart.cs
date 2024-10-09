using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraStart : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public float transitionSpeed = 0.5f;

    private bool transitioning = false;

     private Vector3 targetTrackedObjectOffset = new Vector3(0, 1, 0);

    void Start()
    {
        transitioning = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (transitioning)
        {

            
            CinemachineComposer composer = freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineComposer>(); // Get the middle rig
            if (composer != null)
            {
                Vector3 currentOffset = composer.m_TrackedObjectOffset;
                composer.m_TrackedObjectOffset = Vector3.Lerp(currentOffset, targetTrackedObjectOffset, Time.deltaTime * transitionSpeed);
            }

            {
                transitioning = false;
            }
        }
    }

    // public void OnStartButtonPressed()
    // {
    //     // Start transitioning the camera
    //     transitioning = true;
    // }
}
 