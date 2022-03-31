using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;
using UnityEngine.InputSystem;


public class SceneLoader : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject PlayerPrefab;
    private GameObject inventory;

    ThirdPersonController tp;
    PlayerInput pi;
    DemoScript dp;
    GameObject test;
    PhotonView photonView;
    Camera MiniCamera;
    MiniS Mini;
    //GameObject CanvasUI;
    //AnimAtorObj anime;

    Animator anim;
    GameObject GO;
    // Start is called before the first frame update

    
    void Start()
    {

      //  inventory.SetActive(false);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PlayerPrefab != null)
            //Instantiating Player
            {
                int randomPoint = Random.Range(700, 720);
                test = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(randomPoint, 20f, randomPoint), Quaternion.identity);//postion to where we want to instantiate

                Debug.Log("ami ekhane");
                

            }
        }
       
        else
        {
            Debug.Log("Place Player Prefab");
        } 
        Debug.Log("IS FOUND: "+ PlayerPrefab.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.name);
        inventory = PlayerPrefab.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
        Debug.Log("In Name: "+inventory.name);

        //CanvasUI = PlayerPrefab.transform.GetChild(2).gameObject.transform.GetChild(9).gameObject;
        //MiniCamera = PlayerPrefab.transform.GetChild(2).GetType().transform.GetChild(8).gameObject;

        test = test.transform.GetChild(2).gameObject;
            tp = test.GetComponent<ThirdPersonController>();
         
            pi = test.GetComponent<PlayerInput>();
            dp = test.GetComponent<DemoScript>();
            anim = GetComponent<Animator>();
            Mini = test.GetComponent<MiniS>();
            inventory.SetActive(false);

        photonView = test.GetComponent<PhotonView>();

       // Debug.Log(MiniCamera.name);
        //Debug.Log(CanvasUI.name);
        //Debug.Log(photonView.IsMine);

        if (photonView.IsMine)
        {
            Debug.Log("YES MINE");
            tp.enabled = true;
            pi.enabled = true;
            dp.playerFollower.SetActive(true);
            inventory.SetActive(true);
            Mini.MiniCam.SetActive(true);
            Mini.canvasUI.SetActive(true);

            //anime.anim.enabled = true;
            //anim = GetComponent<Animator>();
        }

                
       
        

        
            
    }

}