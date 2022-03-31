using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using System;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class VoiceManager : MonoBehaviourPunCallbacks
{
    string appId = "2c84ee6f1fbb4d61b231b7fa4d631a33";

    public static VoiceManager Instance;

    IRtcEngine rtcEngine;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        rtcEngine = IRtcEngine.GetEngine(appId);

        if(rtcEngine== null)
        {
            Debug.Log("rtcEngine is null");
        }
        else
            Debug.Log("rtcEngine is not null");


        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnLeaveChannel;
        rtcEngine.OnError += OnError;

        rtcEngine.EnableSoundPositionIndication(true);


    }

    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;
    }
   
    private void OnError(int error, string msg)
    {
        Debug.LogError("Error with Agora: " + msg + " Code: "+error);
    }

    private void OnLeaveChannel(RtcStats stats)
    {
        Debug.Log("Left channel with duration: " + stats.duration);

    }

    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Joined Channel: " + channelName);
        Hashtable hash = new Hashtable();
        hash.Add("agoraID", uid.ToString());
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }
    
    public override void OnJoinedRoom()
    {

        Debug.Log("Room Name on Voice: " + PhotonNetwork.CurrentRoom.Name);
        rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        rtcEngine.LeaveChannel();
    }
    

    private void OnDestroy()
    {
        IRtcEngine.Destroy();
            
    }
}
