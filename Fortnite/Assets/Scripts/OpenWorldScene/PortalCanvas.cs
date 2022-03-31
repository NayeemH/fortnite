using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PortalCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        if (other.gameObject.tag == "Player")
        {
            PhotonNetwork.LoadLevel("OpenWorldScene");
        }
    }
}
