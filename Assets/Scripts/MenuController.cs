using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public Transform player;
    public Transform spawnPoint;
    public Button playButton;
    public Button startButton;
    public GameObject pauseUI;
    public GameObject dialogue;
    private PlayerMovement moveScript;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    public 
    // Start is called before the first frame update
    void Awake()
    {
        playButton.gameObject.SetActive(false);
        moveScript = player.GetComponent<PlayerMovement>();
        moveScript.enabled = false;
        masterSlider.value = 1f;
        musicSlider.value = 0.3339037f;
        sfxSlider.value = 1.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseUI.activeSelf){
            dialogue.SetActive(false);
        }
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
    public void ChangeMasterVolume(float volume){
        mixer.SetFloat("Master Volume", Mathf.Log10(volume) * 20);
    }public void ChangeMusicVolume(float volume){
        mixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
    }public void ChangeSFXVolume(float volume){
        mixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
    }

    
}
