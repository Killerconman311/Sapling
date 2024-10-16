using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // random camera shakes
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    private Vector3 newPos;
    private Quaternion newRot;
    [SerializeField] private float lerpSpeed;

    // void Awake()
    // {
    //     newPos = transform.position;
    //     newRot = transform.rotation;
    // }
    void Start()
    {
        newPos = new Vector3(-0.4f, 2.27f, 6.22f);
        newRot = Quaternion.Euler(0,160,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, lerpSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, newPos) < 0.1f)
        {
            GetNewPos();
        }
    }
    private void GetNewPos()
    {
        var x = Random.Range(min.x, max.x);
        var y = Random.Range(min.y, max.y);

        newPos = new Vector3(x, y, transform.position.z);
    }
}
