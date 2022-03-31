using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControll : MonoBehaviour
{
    [SerializeField]
    public float speed = 1f;
    public float amount = 1f;
    public int countHit = 0;
    public int destroyHit = 3;
    [SerializeField]
    public OpenWorldInventory openWorldInventory;

    public bool isCollectionNeeded = false;
    Vector3 currentPos;
    Vector3 positionRock;
    [SerializeField]
    GameObject floatingPoint;
    [SerializeField]
    Vector3 floatingPos;
    private float freezTime = 50f;

    bool DoneTheJob = false;
    float _jumpTimeoutDelta;

    NewInputSystem playerControl;

    Rigidbody rg;



    void Awake()
    {
        /*
        positionRock = transform.position;
        currentPos = positionRock;
        */

        playerControl = new NewInputSystem();
        //OWI = new OpenWorldInventory();
        rg = gameObject.GetComponent<Rigidbody>();
        countHit = 0;
    }
    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }


    void Update()
    {
        if(_jumpTimeoutDelta>0.0f)
        {
            _jumpTimeoutDelta-=Time.deltaTime;
            isCollectionNeeded = true;
        }
        else
           isCollectionNeeded = false;


        if(freezTime>0.0f)
        {
            freezTime-= Time.deltaTime;
        }   
        else
           rg.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationY;
            
    }

   
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Axe" && playerControl.Player.Hit.ReadValue<float>() == 1)
        {
            Debug.Log("Axe hitted");
            countHit++;
            isCollectionNeeded = true;
            Debug.Log("Count: " + countHit);
        
            _jumpTimeoutDelta = 5f;


            if (countHit > destroyHit)
            {
                Debug.Log("Destroy");
                Destroy(gameObject);
            }

        }
        else
        {

            isCollectionNeeded = false;
        }

    }
    private void Show(float val)
    {
        GameObject floating = Instantiate(floatingPoint, floatingPos, Quaternion.identity, gameObject.transform) as GameObject;
        floating.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "+ " + val.ToString();
    }



}
