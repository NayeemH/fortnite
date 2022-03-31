using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomCs : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject CanvasLoad;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        PhotonNetwork.AutomaticallySyncScene = true;
        CanvasLoad.SetActive(true);

    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To master");
        PhotonNetwork.JoinRandomRoom();
    }

    public void SetPlayerName(string playername) ///making a public class for player input section
    {
        Debug.Log("Entered Set Player Name");
        if (string.IsNullOrEmpty(playername)) ///Checking if the player name is empty or not
        {
            Debug.Log("Player Name is Empty.");
            return;
        }
        PhotonNetwork.LocalPlayer.NickName = playername; //else connecting the name to the server
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Room " + Random.Range(1000, 10000); //generate new random room with max of 20 players
        RoomOptions roomOpt = new RoomOptions();
        roomOpt.IsVisible = true;
        roomOpt.IsOpen = true;
        roomOpt.CleanupCacheOnLeave = true;
        roomOpt.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(roomName, roomOpt);
    }
    public override void OnJoinedRoom()
    {
        string playerName = PlayerPrefs.GetString("username");
        Debug.LogError("Joined Room " + PhotonNetwork.CurrentRoom);
        SetPlayerName(playerName);
        StartCoroutine(waiter());
        CanvasLoad.SetActive(false);
        PhotonNetwork.LoadLevel("WelCome");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Room Left");
    }

    

}
