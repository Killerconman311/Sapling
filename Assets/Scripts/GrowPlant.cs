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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldGrow){
            Grow();
        }
    }
    void Grow(){
        transform.localScale = Vector3.Lerp (transform.localScale, newScale, growSpeed * Time.deltaTime);
        transform.localPosition = Vector3.Lerp (transform.localPosition, newPosition, growSpeed * Time.deltaTime);
        transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, newRotation, growSpeed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Seed")){
            shouldGrow = true;
            Destroy(other.gameObject);
        }
    }

}
