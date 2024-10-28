using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZCoord;
    //public float zMultiplier = 1f;
    bool isColliding = false;
    bool mouseDragging = false;
    void OnMouseDown(){

        //scale the final z position
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //Store offset = gameobject world pos - mouse world pos
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        
    }   
    
    private Vector3 GetMouseWorldPos(){
        //pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        //z coordinate of game object being grabbed
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }
    void OnMouseDrag(){
        mouseDragging = true;
        if (!isColliding)
        {
            transform.position = GetMouseWorldPos() + mouseOffset; // + new Vector3(0,0,zMultiplier);
        }
        
    }
     private void OnCollisionEnter(Collision collision)
    {
        if (mouseDragging)
        {
            isColliding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    private void OnMouseUp()
    {
        mouseDragging = false;
        isColliding = false;
    }
}
