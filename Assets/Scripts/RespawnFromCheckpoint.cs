using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFromCheckpoint : MonoBehaviour
{
    public Transform checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        other.transform.position = checkpoint.position;
    }
}
