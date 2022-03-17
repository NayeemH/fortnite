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

    private Material currentMat;



    Transform tempTransparent;
    Transform tempPrefab;
    Transform transform;

    [Header("Misc.")]
    [SerializeField]
    float rayCastDistance = 8f;

    [SerializeField] float count;           //for Scroll GUI purpose//

    public float currentCount;
    public float gridSize;    //grid size 4
    private float ground;
    public float rotationAmount;

    public LayerMask layermask;


    private Vector3 savePos;

    RaycastHit Hit;

    PlayerControls playerControl;
    CameraGUI cameraGUI;


    public bool materialPlaced;            //To place a material only once//
    public bool canBuild;                  //red and cyan color selector//
    public bool isSame;                    //to determine if the previous material is same as current one after one update cycle//
    public bool wantToBuild;               //Check if players wants to build//
    public bool wantToSelect;               //Check if player wants to select//

    public bool isSelected;
    public bool transformSelected;
    public bool wantToDelete;
    public bool trashTransform;
    public bool rotateRight;
    public bool rotateLeft;
    public bool canDoRotate;


    [SerializeField] GameObject Object;

    private GameObject emptyGameObject;


    private void Awake()
    {
        playerControl = new PlayerControls();

    }
    private void Start()
    {
        cameraGUI = Object.GetComponent<CameraGUI>();

        emptyGameObject = new GameObject();
        emptyGameObject.AddComponent<MeshRenderer>().material = transparentBlue;


        tempTransparent = floorTransparent;         //first material to show is our floor//
        tempPrefab = floorPrefab;
        //transform = tempTransparent;

        currentCount = 0;
        layermask = ~layermask;
        ground = gameObject.transform.position.y;

        materialPlaced = false;
        isSame = false;
        canBuild = true;
        wantToBuild = false;
        wantToSelect = false;
        isSelected = false;
        transformSelected = false;
        wantToDelete = false;
        rotateLeft = false;
        rotateRight = false;

        canDoRotate = true;
        trashTransform = true;

        Object.SetActive(false);
        emptyGameObject.SetActive(false);


    }

    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }


    private void Update()
    {
        if (playerControl.Player.Build.triggered && playerControl.Player.Build.ReadValue<float>() == 1)
        {
            wantToBuild = true;
            wantToSelect = false;
        }
        if (playerControl.Player.Select.triggered && playerControl.Player.Select.ReadValue<float>() == 1)
        {
            wantToBuild = false;
            wantToSelect = true;
        }

        if (canDoRotate)
        {
            if (playerControl.Player.Rotate.ReadValue<Vector2>().y > 0)
            {
                rotateRight = true;
                canDoRotate = false;
            }
            if (playerControl.Player.Rotate.ReadValue<Vector2>().y < 0)
            {
                rotateLeft = true;
                canDoRotate = false;
            }
            

        }
        if (playerControl.Player.Rotate.ReadValue<Vector2>().y == 0)
        {
            canDoRotate = true;
        }


    }
    void FixedUpdate()
    {
        if (wantToBuild)
        {
            if (!Object.activeSelf)
            {
                Object.SetActive(true);
            }

            transform = tempTransparent;
            trashTransform = true;
            //count = 0;
            StartBuild();

        }
        else
        {
            Object.SetActive(false);
        }

        if (wantToSelect)
        {
            Object.SetActive(false);
            if (trashTransform)
            {
                transform.gameObject.SetActive(false);
                trashTransform = false;
            }
            else
            {
                // transform.gameObject.SetActive(true);

            }

            if (!isSelected)
                transform = emptyGameObject.transform;
            if (isSelected)
            {
                if (playerControl.Player.Delete.triggered && playerControl.Player.Delete.ReadValue<float>() == 1)
                {
                    wantToDelete = true;
                }
            }

            StartSelect();
        }



    }

    public void StartBuild()
    {
        //transform = tempTransparent;


        count = cameraGUI.CurrentCount();       //Count for selecting desired material//

        if (currentCount != count)
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


        if (!isSame)
        {
            transform = tempTransparent;
            //transform.gameObject.layer = 2;
            //transform.GetChild(0).gameObject.layer = 2;         //layer 2 is igonore ray cast layer, we are making sure our projected material doest block raycast//
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

        MatPositioning();


        /*if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, rayCastDistance))
         {
             transform.position = new Vector3(Mathf.RoundToInt(Hit.point.x) != 0 ? Mathf.RoundToInt(Hit.point.x / gridSize) * gridSize : gridSize,
                                         (Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y / gridSize) * gridSize : 0),
                                        (Mathf.RoundToInt(Hit.point.z) != 0 ? Mathf.RoundToInt(Hit.point.z / gridSize) * gridSize : gridSize));
             /* if (count != 0)
              {
                  transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(gameObject.transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(gameObject.transform.eulerAngles.y * 90) / 90f : 0, 0);

              if (playerControl.Player.DragnDrop.triggered)                        //This statement is going to be used later for BR part//
                  {
                      Instantiate(tempPrefab, transform.position, transform.rotation);
                  }

              }*/

        if (playerControl.Player.DragnDrop.ReadValue<float>() == 1)       //To make sure only once the materials can be placed//
        {
            if (!materialPlaced && canBuild)
            {
                Instantiate(tempPrefab, transform.position, transform.rotation).gameObject.SetActive(true); ;
            }


            materialPlaced = true;
        }
        else if (playerControl.Player.DragnDrop.ReadValue<float>() == 0)
        {
            materialPlaced = false;
        }

        /* }*/
    }

    public void SetCanBuild(bool val)
    {
        canBuild = val;
        //Debug.Log(val);
    }


    public void StartSelect()
    {
        if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, rayCastDistance, layermask))
        {
            if (playerControl.Player.Select.triggered && playerControl.Player.Select.ReadValue<float>() == 1)
            {
                // Debug.Log("Going!");
                if (!isSelected && Hit.transform.gameObject.tag == "BuildingMat")
                {
                    isSelected = true;
                    //Debug.Log("Now you can Code!");

                    transform = Hit.transform;

                    if (transform.parent != null)
                    {
                        transform = transform.parent;
                    }
                   




                    
                    
                    //Debug.Log("Current Transform : " + transform.name);
                    currentMat = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
                    transform.GetChild(0).gameObject.layer = 2;
                    transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = transparentBlue;
                    transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
                    savePos = transform.localPosition;

                }
                else
                {
                    transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = currentMat;
                    transform.GetChild(0).gameObject.layer = 0;
                    transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
                    isSelected = false;
                    return;

                }
            }

            /*if (Hit.transform.gameObject.tag == "BuildingMat" && !isSelected)
            {
                //Debug.Log("Now you can Code!");

                transform = Hit.transform;
                //Debug.Log("Current Transform : " + transform.name);
                currentMat = transform.gameObject.GetComponent<MeshRenderer>().material;

            }*/



        }






        /*if(transformSelected)
        {
            currentMat = transform.gameObject.GetComponent<MeshRenderer>().material;

        }

        if(!transformSelected)
        {
            Debug.Log("Going");
            transform.gameObject.GetComponent<MeshRenderer>().material = currentMat;
            transform.gameObject.layer = 0;
        }*/


        if (isSelected)
        {
            MatPositioning();
            //transform.position += savePos;
        }

        if (isSelected)
        {
            if (playerControl.Player.Delete.triggered && playerControl.Player.Delete.ReadValue<float>() == 1)
                if (wantToDelete)
                {
                    //transform.gameObject.SetActive(false);
                    Destroy(transform.gameObject);
                    isSelected = false;
                    wantToDelete = false;
                    return;
                }

        }

        /*transform.gameObject.GetComponent<MeshRenderer>().material = currentMat;
        transform.gameObject.layer = 0;*/



    }


    private void MatPositioning()
    {
        /*if (isSelected)
        {
            transform.gameObject.layer = layermask;
            transform.gameObject.GetComponent<MeshRenderer>().material = transparentBlue;

        }*/

        //transform.GetChild(0).gameObject.layer = layermask;
        if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, rayCastDistance))
        {
            transform.position = new Vector3(Mathf.RoundToInt(Hit.point.x) != 0 ? Mathf.RoundToInt(Hit.point.x / gridSize) * gridSize : gridSize,
                                        (Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y / gridSize) * gridSize : 0),
                                       (Mathf.RoundToInt(Hit.point.z) != 0 ? Mathf.RoundToInt(Hit.point.z / gridSize) * gridSize : gridSize))/* + savePos*/;






            if (rotateLeft)
            {
               var currentRotation = transform.rotation;

                currentRotation *= Quaternion.Euler(0, -rotationAmount, 0);

                transform.rotation = currentRotation;
                rotateLeft = false;
                //canDoRotate = true;
            }
            if (rotateRight)
            {
                var currentRotation = transform.rotation;

                currentRotation *= Quaternion.Euler(0, rotationAmount, 0);

                transform.rotation = currentRotation;
                rotateRight = false;
                //canDoRotate = true;
            }
            // transform.position += savePos;
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

        }
        //transform.GetChild(0).gameObject.layer = 0;

    }

    private void TransformSelection(bool val)
    {
        transformSelected = val;
    }

}