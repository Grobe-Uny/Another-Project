using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothPlayerFollow : MonoBehaviour
{
    public Transform target;
    public float smoothingSpeed = 0.15f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Mathf.Lerp(transform.position.x, target.position.x, 0.1f);
        Mathf.Lerp(transform.position.z, target.position.z, 0.1f);
        Mathf.Lerp(transform.position.y, target.position.y, 0.1f);*/
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, smoothingSpeed), Mathf.Lerp(transform.position.y, target.position.y, smoothingSpeed), Mathf.Lerp(transform.position.z, target.position.z, smoothingSpeed));
    }
}
