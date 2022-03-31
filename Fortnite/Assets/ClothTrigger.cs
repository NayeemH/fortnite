using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothTrigger : MonoBehaviour
{

    public GameObject canvas;
    public GameObject mouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canvas.SetActive(true);
            mouse.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        mouse.SetActive(false);

    }
}
