using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
     public Transform target;
     public Transform startCamera;
     // Start is called before the first frame update
     void Start()
    {
          transform.position = startCamera.position;
          transform.rotation = startCamera.rotation;
    }

    // Update is called once per frame
    void Update()
    {
          transform.LookAt(target);
    }
}
