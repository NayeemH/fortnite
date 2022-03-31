using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{

    public RoomCs Trig;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Trig.GetComponent<RoomCs>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Trig.GetComponent<RoomCs>().enabled = false;
    }
}
