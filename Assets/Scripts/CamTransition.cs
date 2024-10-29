using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CamTransistion : MonoBehaviour
// Script that is used to transition between the player camera and the menu camera.
// also turns off player movement and camera control when paused.
// also turns on pause UI.
{
    [SerializeField] public CinemachineBrain brain;
    private int lowPriority = 0;
    private int highPriority = 10;
    [SerializeField] private CinemachineVirtualCamera menuCam;
    [SerializeField] private CinemachineFreeLook playerCam;    
    private bool isPaused;
    private PlayerMovement moveScript;
    private GameObject player;
    public GameObject pauseUI;
    public float pauseDelay = 0.1f;
    public float unpauseDelay = 0.1f;   
    public float mechanicDelay = 0.1f;
    

    
    // Start is called before the first frame update. Using it to grab parts of shadow and define priority for use in methods.
    private void Awake() {
        Debug.Log("Please");
        isPaused = true;
        player = transform.parent.gameObject;
        moveScript = player.GetComponent<PlayerMovement>();
        pauseUI.SetActive(true);
        menuCam.Priority  = highPriority;
        playerCam.Priority = lowPriority;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Heo");
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            //Debug.Log("Hello");
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        //Debug.Log("Paused: "+isPaused);
        if (isPaused)
        {
            StartCoroutine(PauseTheGame());
        }
        else
        {
            StartCoroutine(UnpauseTheGame());
        }
    }

    private IEnumerator PauseTheGame()
    {
        yield return new WaitForSeconds(mechanicDelay);
        moveScript.enabled = false;
        playerCam.Priority = lowPriority;
        menuCam.Priority = highPriority;
        yield return new WaitForSeconds(pauseDelay);
        pauseUI.SetActive(true);
    }

    private IEnumerator UnpauseTheGame()
    {
        pauseUI.SetActive(false);
        playerCam.Priority = highPriority;
        menuCam.Priority = lowPriority;
        yield return new WaitForSeconds(unpauseDelay);
        moveScript.enabled = true;
    }
}