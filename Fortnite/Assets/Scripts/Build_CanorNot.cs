using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_CanorNot : MonoBehaviour
{

    public bool canBuild;
    //bool flag;
    private BuildSystem buildSystem;

    // Start is called before the first frame update

    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        buildSystem = gameObject.GetComponent<BuildSystem>();
        //flag = false;
        //canBuild = true;
       // buildSystem.SetCanBuild(canBuild);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("BuildingMat"))
        {
            //Debug.Log("Triggered");
            canBuild = false;
            buildSystem.SetCanBuild(canBuild);
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        canBuild = true;
        buildSystem.SetCanBuild(canBuild);
    }

    private void OnDisable()
    {
        canBuild = true;            //Safty case
        buildSystem.SetCanBuild(canBuild);
    }

}
