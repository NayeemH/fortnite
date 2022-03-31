///
///First Author : Sudipto
///Second Author : Sakib
///Overall Guidance and Supervision : Nahid & Nayeem
///
 
///This scripts is made to handel the building system of creative part of our game. It will project a material and will instantiate 




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildSystem : MonoBehaviour
{
    [SerializeField]
    Transform CamChild;
   
    [Header("Prefabs")]
    [SerializeField] Transform floorTransparent;
   
    [SerializeField] Transform floorPrefab;

    [SerializeField] Transform wallTransparent;
    
    [SerializeField] Transform wallPrefab;

    [SerializeField] Transform stairTransparent;

    [SerializeField] Transform stairPrefab;


    [Header("Materials")]
    [SerializeField] Material transparentBlue;

    [SerializeField] Material transparentRed;


    Transform tempTransparent;
    Transform tempPrefab;
    Transform transform;

    [Header("Misc.")]
    [SerializeField]
    float rayCastDistance = 8f;

    [SerializeField] float count;           //for Scroll GUI purpose//

    public float currentCount;

    RaycastHit Hit;

    NewInputSystem playerControl;
    CameraGUI cameraGUI;


    public bool materialPlaced;            //To place a material only once//
    public bool canBuild;                  //red and cyan color selector//
    public bool isSame;                    //to determine if the previous material is same as current one after one update cycle//


    [SerializeField] GameObject Object;
    // Start is called before the first frame update

    private void Awake()
    {
        playerControl = new NewInputSystem();       

    }
    private void Start()
    {
        cameraGUI = Object.GetComponent<CameraGUI>();
        tempTransparent = floorTransparent;         //first material to show is our floor//
        tempPrefab = floorPrefab;
        transform = tempTransparent;

        currentCount = 0;


        materialPlaced = false;
        isSame = false;
        canBuild = true;
        


    }

    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //transform = tempTransparent;
        

        count = cameraGUI.CurrentCount();       //Count for selecting desired material//
      
        if(currentCount!=count)
        {

            if (count == 0)
            {

                //transform.gameObject.layer = 8;   
                //transform.GetChild(0).gameObject.layer = 8;       //For full proof invisibility purpose//
                transform.gameObject.SetActive(false);      // Making previous material invisible for players//
                tempTransparent = floorTransparent;
                tempPrefab = floorPrefab;


            }
            else if (count == 1)
            {

                //transform.gameObject.layer = 8;
                //transform.GetChild(0).gameObject.layer = 8;
                transform.gameObject.SetActive(false);
                tempTransparent = wallTransparent;
                tempPrefab = wallPrefab;


            }
            else if (count == 2)
            {

                //transform.gameObject.layer = 8;
                //transform.GetChild(0).gameObject.layer = 8;
                transform.gameObject.SetActive(false);
                tempTransparent = stairTransparent;
                tempPrefab = stairPrefab;


            }
            currentCount = count;
            isSame = false;
        }
       

        if(!isSame)
        {
            transform = tempTransparent;
            transform.gameObject.layer = 2;
            transform.GetChild(0).gameObject.layer = 2;         //layer 2 is igonore ray cast layer, we are making sure our projected material doest block raycast//
            transform.gameObject.SetActive(true);
        }
        

        if (!canBuild)
        {
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = transparentRed;
        }
        
        else
        {
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = transparentBlue;
        }



        if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, rayCastDistance))
            { 
                transform.position = new Vector3(Mathf.RoundToInt(Hit.point.x) != 0 ? Mathf.RoundToInt(Hit.point.x / 0.2f) * 0.2f : 0.2f,
                                            (Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y / 0.2f) * 0.2f : 0),
                                           (Mathf.RoundToInt(Hit.point.z) != 0 ? Mathf.RoundToInt(Hit.point.z / 0.2f) * 0.2f : 0.2f));

            /* if (count != 0)
             {
                 transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(gameObject.transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(gameObject.transform.eulerAngles.y * 90) / 90f : 0, 0);

             if (playerControl.Player.DragnDrop.triggered)                        //This statement is going to be used later for BR part//
                 {
                     Instantiate(tempPrefab, transform.position, transform.rotation);
                 }

             }*/

            /*else
            {*/


            if (playerControl.Player.DragnDrop.ReadValue<float>()==1)       //To make sure only once the materials can be placed//
            {
               if(!materialPlaced && canBuild)
                Instantiate(tempPrefab, transform.position, Quaternion.identity).gameObject.SetActive(true);

                materialPlaced = true;
            }
            else if(playerControl.Player.DragnDrop.ReadValue<float>() == 0)
            {
                materialPlaced = false;
            }
                   
        }
    }

    public void SetCanBuild(bool val)
    {
        canBuild = val;
        Debug.Log(val);
    }

}