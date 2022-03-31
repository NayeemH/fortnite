using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject PlayerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
            
        {
            if (PlayerPrefab != null)
            //Instantiating Player
            {
                int randomPoint = Random.Range(1200, 1205);
                PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(randomPoint, 113f, randomPoint), Quaternion.identity);//postion to where we want to instantiate
            }
        }
        else
        {
            Debug.Log("Place Player Prefab");
        }
    }

}
