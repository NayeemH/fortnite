using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorManagement : MonoBehaviour
{

    public bool _isInsideTrigger = false;
    public bool isPressed = false;

    //public Animator _animator;

    



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collider triggered");
            _isInsideTrigger = true;
            
            //OpenPanel.SetActive(true);
        }
    }

    void Update()
    {
        if(isPressed)
        {
            Debug.Log("Is Pressed!");
        }
    }

    

}

