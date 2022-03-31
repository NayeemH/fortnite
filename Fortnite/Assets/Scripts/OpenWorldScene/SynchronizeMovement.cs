using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;
using UnityEngine.InputSystem;
public class SynchronizeMovement : MonoBehaviourPunCallbacks
{
    ThirdPersonController tp;
    PlayerInput pi;
    DemoScript dp;
    
    GameObject player;
    //private Transform abc;
    // Start is called before the first frame update
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        tp = player.GetComponent<ThirdPersonController>();
        pi = player.GetComponent<PlayerInput>();
        dp = player.GetComponent<DemoScript>();

    }
    private void OnEnable()
    {
        Debug.Log("True");
    }
    private void Start()
    {
        if (photonView.IsMine)
        {
            Debug.Log("YES");
            tp.enabled = true;
            pi.enabled = true;
            // gameObject.GetComponent<PlayerData>().enabled = true;
            dp.playerFollower.SetActive(true);

        }

        
    }

    
}
