using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camChild : MonoBehaviour
{
    public Quaternion angle;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        angle = Quaternion.Euler(Camera.main.transform.rotation.x, 0f, 0f);
        transform.localRotation = angle;
    }
}
