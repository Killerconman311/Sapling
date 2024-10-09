using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamTransistion : MonoBehaviour
// Script which turns off the shadow and causes it to reappear if the player touches a diamond.
// Then the shadow is always front and center of camera.
{
    [SerializeField] public CinemachineBrain brain;
    private int lowPriority;
    private int highPriority;
    [SerializeField] public CinemachineFreeLook menuCam;
    [SerializeField] public CinemachineFreeLook playerCam;
    
    private bool isPaused = true;
    

    
    // Start is called before the first frame update. Using it to grab parts of shadow and define priority for use in methods.
    void Start()
    {

        lowPriority = 1;
        highPriority = lowPriority + 1;
        menuCam.Priority  = highPriority;
        playerCam.Priority = lowPriority;

    }

    // Update is called once per frame
    void Update()
    {
     
         if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            menuCam.Priority = highPriority;
            playerCam.Priority = lowPriority;
            return;
        }
        else
        {
            menuCam.Priority = lowPriority;
            playerCam.Priority = highPriority;
        }

    }
    
    // Method that is used to check if the player has collected "the light at the end of the tunnel"
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Void"))
        {
            playerCam.Priority = lowPriority;
            menuCam.Priority = highPriority;
        }
    }

}