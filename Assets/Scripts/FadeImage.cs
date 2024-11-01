using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeImage : MonoBehaviour
{
    bool fadeIn = false;
    bool fadeOut = false;

    public float timeToFade = 0f;
    public float timeToChangeScene = 0f;

    [SerializeField] CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn){
            if(canvasGroup.alpha < 1){
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if(canvasGroup.alpha >= 1){
                    fadeIn = false;
                }
            }
        }
        if(fadeOut){
            if(canvasGroup.alpha >= 0){
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if(canvasGroup.alpha == 0){
                    fadeOut = false;
                }
            }
        }
    }

    public void FadeIn(){
        fadeIn = true;
    }
    public void FadeOut(){
        fadeOut = true;
    }
    public void QuitGame(){
        StartCoroutine(FadeOnSceneChange());
    }
    IEnumerator FadeOnSceneChange(){
        FadeOut();
        yield return new WaitForSeconds(timeToFade);
        Application.Quit();
    }
    
}
