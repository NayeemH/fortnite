using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class NetworkManagement : MonoBehaviourPunCallbacks
{
   public static NetworkManagement networkManagement;
 //  public GameObject battleButton;

   public void Awake()
   {
       networkManagement = this;
   }

   void Start(){
       PhotonNetwork.ConnectUsingSettings();
   }

     private void Update()
    {
      //  Debug.Log("Connection status: " + PhotonNetwork.NetworkClientState);
    }

   public override void OnConnectedToMaster(){
      Debug.Log("Player has been connected to the master!");
      //battleButton.SetActive(true);
       PhotonNetwork.JoinRandomRoom();
      
   }
   public void OnBattleButtonClicked(){
       PhotonNetwork.JoinRandomRoom();
   }

   public override void OnJoinRandomFailed(short returnCode, string message){
       Debug.Log("Random Room joining failed!");
       CreateRoom();
   }

   void CreateRoom(){
       int randomRoomName = Random.Range(0,10000);
       RoomOptions roomOpts  = new RoomOptions(){ IsVisible = true, IsOpen = true, MaxPlayers = 10};
       PhotonNetwork.CreateRoom("Room"+randomRoomName, roomOpts);
   }

   public override void OnCreateRoomFailed(short returnCode, string message){
       Debug.Log("Room with simillar name error!");
       CreateRoom();
   }

}
