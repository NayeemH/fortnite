using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{


    private NewInputSystem playerControls; //class from the created script

    public bool canPickUp;
   
    //public GameObject box;

    private GameObject otherGO;



    

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new NewInputSystem();
    }
    void Start()
    {
        otherGO = new GameObject("Trash");
        canPickUp = false;
    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log(playerControls.Player.DragnDrop.ReadValue<float>());
        Pickup();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bmaterials"))
        {
            canPickUp = true;
            otherGO = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPickUp = false;
    }
    private void Pickup()
    {
        if (canPickUp && playerControls.Player.DragnDrop.ReadValue<float>()>0)
        {
            otherGO.transform.parent = gameObject.transform;
            /*float x = -1*playerControls.Player.Look.ReadValue<Vector2>().x;
            float y = playerControls.Player.Look.ReadValue<Vector2>().y;
            box.transform.localPosition += new Vector3(x, y, transform.localPosition.z);*/

        }
        else if(playerControls.Player.DragnDrop.ReadValue<float>() == 0)
        {
            otherGO.transform.parent = null;
        }
    }
}
