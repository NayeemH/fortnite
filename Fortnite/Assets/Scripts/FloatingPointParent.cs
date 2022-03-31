using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointParent : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyTime;


    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
