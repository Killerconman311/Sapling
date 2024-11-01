using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public GameObject text;
     public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scrollDEEZ();
    }

    void scrollDEEZ()
    {
        float distanceToMove = speed * Time.deltaTime;
        text.transform.Translate(Vector2.up * distanceToMove);
    }

}
