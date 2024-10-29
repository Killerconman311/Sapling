using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform player;
    public Transform spawnPoint;
    public Button playButton;
    public Button startButton;
    public GameObject pauseUI;
    private PlayerMovement moveScript;

    public 
    // Start is called before the first frame update
    void Awake()
    {
        playButton.gameObject.SetActive(false);
        moveScript = player.GetComponent<PlayerMovement>();
        moveScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        moveScript.enabled = true;
        startButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        player.position = spawnPoint.position;
        player.eulerAngles = spawnPoint.eulerAngles;
        pauseUI.SetActive(false);
    }
    public void Unpause(){
        pauseUI.SetActive(false);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
