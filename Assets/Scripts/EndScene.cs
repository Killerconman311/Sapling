using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScene : MonoBehaviour
{

    public void ChangeScene(){
        SceneManager.LoadScene("EndScene");
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            ChangeScene();    
        }
    }

}
