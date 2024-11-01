using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    public Vector3 newScale;
    public Vector3 newPosition;
    public Vector3 newRotation;
    public float growSpeed = 2.0f;

    public bool shouldGrow = false;
    public bool canGrow = false;

    public GameObject plant;

    private GameObject seed;
    public GameObject glowController;
    private AudioSource audioSource;
    private bool soundPlayed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canGrow && shouldGrow){
            Grow();
            glowController.GetComponent<ObjectGlow>().DisableGlow(seed.GetComponent<Renderer>().material, 0.1f);
            //Destroy(seed);
        }
        if(Input.GetKey(KeyCode.E)){
            shouldGrow = true;
        }else{
            shouldGrow = false;
        }
    }
    public void Grow(){
        if (!soundPlayed)
            {
                audioSource.PlayOneShot(audioSource.clip);
                soundPlayed = true;
            }
        plant.transform.localScale = Vector3.Lerp (plant.transform.localScale, newScale, growSpeed * Time.deltaTime);
        plant.transform.localPosition = Vector3.Lerp (plant.transform.localPosition, newPosition, growSpeed * Time.deltaTime);
        plant.transform.eulerAngles = Vector3.Lerp (plant.transform.eulerAngles, newRotation, growSpeed * Time.deltaTime);
        glowController.GetComponent<ObjectGlow>().DisableGlow(seed.GetComponent<Renderer>().material, 2f);
    }
    
    void OnTriggerEnter(Collider other){
        Debug.Log("Triggered: " + other.gameObject.name);
        if(other.gameObject.CompareTag("Seed")){
            seed = other.gameObject;
            canGrow = true;
        }
    }
    void OnTriggerExit(Collider other){
        Debug.Log("Trigger Exit: " + other.gameObject.name);
        if(other.gameObject.CompareTag("Seed")){
            seed = null;
            canGrow = false;
        }
    }
}
