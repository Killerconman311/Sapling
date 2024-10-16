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
    private int lowPriority;
    private int highPriority;
    [SerializeField] public CinemachineVirtualCamera menuCam;
    [SerializeField] public CinemachineFreeLook playerCam;
    
    private bool isPaused = true;
    private PlayerMovement moveScript;
    private GameObject player;
    private GameObject canvasObject;
    private Canvas pauseUI;
    public float pauseDelay = 0.1f;
    public float mechanicDelay = 0.1f;
    

    
    // Start is called before the first frame update. Using it to grab parts of shadow and define priority for use in methods.
    private void Awake() {
        player = GameObject.Find("Sappy");
        moveScript = player.GetComponent<PlayerMovement>();
        canvasObject = GameObject.Find("Canvas");
        pauseUI = canvasObject.GetComponent<Canvas>();
        pauseUI.enabled = true;
    }
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
        PauseGame();
        CheckUI();
        CheckMechanics();
    }

    public void PauseGame()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
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
    public void CheckUI()
    {
        if (isPaused)
        {
            // turn on pause UI
            //Debug.Log("Pause UI go on now");
            StartCoroutine(PauseDelay(pauseDelay));
        }
        else
        {
            pauseUI.enabled = false;
        }

        if (!isPaused)
        {
            pauseUI.enabled = false;
        }
    }
    public void CheckMechanics()
    {
        if (isPaused)
        {
            StartCoroutine(MechanicDelay(mechanicDelay));

        }
        else
        {
            moveScript.enabled = true;
        }
    }
    private IEnumerator MechanicDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveScript.enabled = false;
    }
    private IEnumerator PauseDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pauseUI.enabled = true;
    }
}