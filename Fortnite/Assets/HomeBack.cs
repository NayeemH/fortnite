using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class HomeBack : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    IEnumerator waitTill()
    {
        yield return new WaitForSeconds(2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PhotonNetwork.LeaveRoom();
            StartCoroutine(waitTill());
            PhotonNetwork.LoadLevel("RoomScene");
        }
    }


}
