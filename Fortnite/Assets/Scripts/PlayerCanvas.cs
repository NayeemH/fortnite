using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerCanvas : MonoBehaviour
{
    public GameObject PlayerCanvasPrefab;
    public GameObject CanvasPrefab;
    //public GameObject TextE;
    // Start is called before the first frame update

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerCanvasPrefab.SetActive(true);
            CanvasPrefab.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //TextE.SetActive(false);
        PlayerCanvasPrefab.SetActive(false);
        CanvasPrefab.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
