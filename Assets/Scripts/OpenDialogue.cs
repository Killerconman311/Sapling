using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenDialogue : MonoBehaviour
{
    public string text;
    public GameObject dialogue;
    private TextMeshProUGUI textMeshGUI;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowDialogue(){
        dialogue.GetComponentInChildren<TextMeshProUGUI>().text = text;
        dialogue.SetActive(true);
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            ShowDialogue();
        }
    }
    void OnTriggerExit(Collider other){

    }

}
