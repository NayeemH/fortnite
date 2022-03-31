using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
/*
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif




namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
*/

public class PlayerData : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    public GameObject Inventory;

    
    [SerializeField]
    PhotonView photonView;


    [SerializeField]
    public TMPro.TextMeshProUGUI UserName;


    public OpenWorldInventory openWorldInventory;

    public string playerName;    
    PlayerOwnWorldData playerOwnWorldData;
    int count =0;
    public int woodCnt;
    public int stoneCnt;
    public int coinCnt;

    void Awake()
    {
        openWorldInventory = Inventory.GetComponent<OpenWorldInventory>();
        setPlayerNameCanvasUI();

        playerName = PlayerPrefs.GetString("username");
        //playerNamePanel.text = playerName;
        Debug.Log("UserName: "+playerName);
        playerOwnWorldData = new PlayerOwnWorldData();
        playerOwnWorldData.playerData = new PlayerOwnWorldData.PlayerDataOwn();

       // UserName.text = playerName;
        
        playerOwnWorldData.plummie_tag = playerName;
        StartCoroutine(playerOwnWorldData.FetchPlayerData(result => {
            Debug.Log("Fetching Data: ");
            Debug.Log(result);
            playerOwnWorldData = result;

            Debug.Log("Stone Amt: "+playerOwnWorldData.playerData.stoneCount+ " Wood Amt: "+ playerOwnWorldData.playerData.woodCount);


            woodCnt =playerOwnWorldData.playerData.woodCount;
            stoneCnt = playerOwnWorldData.playerData.stoneCount; 
            coinCnt = playerOwnWorldData.playerData.coinCount; 


            openWorldInventory.woodAmount = woodCnt;
            openWorldInventory.stoneAmount =stoneCnt;
            openWorldInventory.coinAmount = coinCnt;
        }));

    }
    void setPlayerNameCanvasUI()
    {
       
            UserName.text = photonView.Owner.NickName;
        
    }


    private void OnEnable()
    {
        Debug.Log("Player Data Enabled!");
    }

    void FixedUpdate()
    {
        /*
        playerOwnWorldData.plummie_tag = playerName;
        StartCoroutine(playerOwnWorldData.FetchPlayerData(result => {
            Debug.Log("Fetching Data: ");
            Debug.Log(result);
            playerOwnWorldData = result;

            Debug.Log("Stone Amt: " + playerOwnWorldData.playerData.stoneCount + " Wood Amt: " + playerOwnWorldData.playerData.woodCount);


            woodCnt = playerOwnWorldData.playerData.woodCount;
            stoneCnt = playerOwnWorldData.playerData.stoneCount;
            coinCnt = playerOwnWorldData.playerData.coinCount;


            openWorldInventory.woodAmount = woodCnt;
            openWorldInventory.stoneAmount = stoneCnt;
            openWorldInventory.coinAmount = coinCnt;
        }));
        /*
           playerOwnWorldData.playerData.woodCount = openWorldInventory.woodAmount;
           playerOwnWorldData.playerData.stoneCount = openWorldInventory.stoneAmount;
           playerOwnWorldData.playerData.coinCount = openWorldInventory.coinAmount;

           
           /*
           Debug.Log("OpenworldInventory wood: "+openWorldInventory.woodAmount+ " stone: "+openWorldInventory.stoneAmount);

            Debug.Log("updating...");
         
            if(count>=10){
              StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
                //Debug.Log(result);
            }));  
            count = 0;
            }
            
            count++;
       */






    }

}


