using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasCollider : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject PlayerCanvasPrefab;
    [SerializeField]
    public GameObject InventoryMine;
    GameObject InventoryOther;
    public GameObject CanvasTrading;
    public GameObject CanvasViewProfile;
    public GameObject ItemSelectUI;
    public GameObject ChatUI;
    public GameObject BackPackUI;
    [SerializeField]
    GameObject TradeReqCanvas;
    //[SerializeField]
    //TMPro.TextMeshProUGUI UserName;
    [SerializeField]
    TMPro.TextMeshProUGUI wood;
    [SerializeField]
    TMPro.TextMeshProUGUI stone;
    [SerializeField]
    TMPro.TextMeshProUGUI coin;
    [SerializeField]
    InputField woodField;
    [SerializeField]
    InputField stoneField;
    [SerializeField]
    TMPro.TextMeshProUGUI gameID;
    [SerializeField]
    TMPro.TextMeshProUGUI name;
    [SerializeField]
    TMPro.TextMeshProUGUI gender;
    [SerializeField]
    TMPro.TextMeshProUGUI woodInputField;
    [SerializeField]
    TMPro.TextMeshProUGUI stoneInputField;
    [SerializeField]
    TMPro.TextMeshProUGUI tradeReq;
    

    //PhotonView photonView;
    NewInputSystem playerControls;
    [SerializeField]
    GameObject PlayerFreezeScreen;
    PlayerOwnWorldData playerOwnWorldData;

    int countWood = 0;
    int countStone = 0;
    PlayerData playerData;
    GameObject player;
    //InputSystemUIInputModule inputSystemUIInputModule;
    public string playerName;
    bool opponentFlag = false;

    public string GameIDOpponent;

    public bool x;
    private bool fech = true;
    //public GameObject TextE;
    // Start is called before the first frame update

    private void Awake()
    {
        //playerControls = new NewInputSystem();
        //Cursor.lockState = CursorLockMode.None;
                playerName = PlayerPrefs.GetString("username");

       // Cursor.visible = true;
        playerOwnWorldData = new PlayerOwnWorldData();
        playerOwnWorldData.playerData = new PlayerOwnWorldData.PlayerDataOwn();
        playerOwnWorldData.listOfFriends = new List<string>();
      //  wood.text = "425";//playerOwnWorldData.playerData.woodCount.ToString();
       // stone.text = "234432";//playerOwnWorldData.playerData.stoneCount.ToString();
       // coin.text = "234423";


    }
    /*
        private void OnEnable()
        {
            playerControls.Enable();
        }


        private void OnDisable()
        {
            playerControls.Disable();
        }
        */
    private void Update()
    {
              
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        /*
           StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
                Debug.Log("Uploading from canvas: "+result);
                
            }));
        */





        if (opponentFlag == true)
        {
            Debug.Log("oppenentFlag: "+playerOwnWorldData.plummie_tag);

            if (fech == true)
            {
                StartCoroutine(playerOwnWorldData.FetchPlayerData(result =>
                {
                    Debug.Log("Fetching Data in Canvas");
                    fech = false;
                    playerOwnWorldData = result;
                    Debug.Log("Feched Data: " + result);

                    Debug.Log("wood: " + playerOwnWorldData.playerData.woodCount + " stone: " + playerOwnWorldData.playerData.stoneCount + " gameID: " + player.GetComponent<PlayerData>().playerName);

                //playerOwnWorldData.playerData.coinCount.ToString();
                // UserName.text =GameIDOpponent;
                   ///
                       



                   ///


                    gameID.text = "Trading with "+ GameIDOpponent;
                    tradeReq.text = "Do you want to trade with " + GameIDOpponent+ " ?";
                    
                    name.text = playerOwnWorldData.name;
                    gender.text = playerOwnWorldData.gender;

                    wood.text = playerOwnWorldData.playerData.woodCount.ToString();
                    stone.text = playerOwnWorldData.playerData.stoneCount.ToString();
                    coin.text = playerOwnWorldData.playerData.coinCount.ToString();

                    opponentFlag = false;
                }));
            }
     
        }
  /*
        StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
            //Debug.Log(result);
        }));
 */
        /*
        Debug.Log(playerControls.Player.Open.ReadValue<float>());
        if (playerControls.Player.Open.ReadValue<float>() > 0)
            x = true;
        */
    }


    public void OnClickAccept()
    {
        CanvasViewProfile.SetActive(true);
        TradeReqCanvas.SetActive(false);
        PlayerFreezeScreen.GetComponent<PlayerInput>().enabled = false;
    }
    public void OnClickDecline()
    {
        TradeReqCanvas.SetActive(false);
        PlayerFreezeScreen.GetComponent<PlayerInput>().enabled = true;
    }

    public void OnClickTradeAccept()
    {
        CanvasViewProfile.SetActive(false);

    }
    public void OnClickTradeDecline()
    {
        CanvasViewProfile.SetActive(false);
        PlayerFreezeScreen.GetComponent<PlayerInput>().enabled = true;

    }

    private void OnTriggerStay(Collider other)
    {

        Debug.Log("Collider Triggered in Canvas");

        if (other.tag == "Player")
        {

            player = other.gameObject;


            GameIDOpponent = player.GetComponent<PhotonView>().Owner.NickName;
            Debug.Log("UserNmae: " + GameIDOpponent);
            if (GameIDOpponent != playerName) {
                opponentFlag = true;
                playerOwnWorldData.plummie_tag = GameIDOpponent;

                Debug.Log("NAME: " + player.GetComponent<PlayerData>().playerName);

                Debug.Log("Collider Tag: " + other.tag);
                // Debug.Log("playerName: "+playerData.playername);
                //PlayerCanvasPrefab.SetActive(true);
                if (CanvasTrading.active == false)
                {
                    //CanvasViewProfile.SetActive(true);
                    TradeReqCanvas.SetActive(true);
                    woodInputField.text = "0";
                    stoneInputField.text = "0";
                }




            }
        }
                    
                 
             if (x == true)            
                {
                   /// ThirdPersonController tp = Player.ThirdPersonController;
                   // tp.enabled = true;
                    //Player.GetComponent<PlayerInput>().SetActive(false);
                    //Player.GetComponent<InputSystemUIInputModule>().SetActive(true);

                  
                   
                }
            else
            {
                    //Player.GetComponent<ThirdPersonController>().SetActive(true);
                    //Player.GetComponent<PlayerInput>().SetActive(true);
                    //Player.GetComponent<InputSystemUIInputModule>().SetActive(false);
            }
                

            
           
            

        
    }
    
    /*
     private void OnCollisionEnter(Collision other)
    {

        Debug.Log("Collider Triggered in Canvas");

        if (other.collider.tag == "Player")
        {

                    player = other.gameObject;
        
                    string GameIDOpponent = player.GetComponent<PhotonView>().Owner.NickName;
                    Debug.Log("UserNmae: "+GameIDOpponent);

                    playerOwnWorldData.plummie_tag = GameIDOpponent;

                   Debug.Log("NAME: "+player.GetComponent<PlayerData>().playerName);
         
                    Debug.Log("Collider Tag: "+ other.gameObject.tag);
                   // Debug.Log("playerName: "+playerData.playername);
                    //PlayerCanvasPrefab.SetActive(true);
                    CanvasPrefab.SetActive(true);
                    StartCoroutine(playerOwnWorldData.FetchPlayerData(result => {
                    Debug.Log("Fetching Data in Canvas");
        
                    playerOwnWorldData = result;
                    Debug.Log("Feched Data: "+result);
                    
                    Debug.Log("wood: "+ playerOwnWorldData.playerData.woodCount+" stone: "+playerOwnWorldData.playerData.stoneCount+ " gameID: "+ player.GetComponent<PlayerData>().playerName);

                    wood.text =playerOwnWorldData.playerData.woodCount.ToString();
                    stone.text = playerOwnWorldData.playerData.stoneCount.ToString(); 
                    coin.text = playerOwnWorldData.playerData.coinCount.ToString(); 
                   // UserName.text =GameIDOpponent;
                    gameID.text = GameIDOpponent;
                    name.text = playerOwnWorldData.name;
                    gender.text = playerOwnWorldData.gender;



        }));
                    
                 
             if (x == true)            
                {
                   /// ThirdPersonController tp = Player.ThirdPersonController;
                   // tp.enabled = true;
                    //Player.GetComponent<PlayerInput>().SetActive(false);
                    //Player.GetComponent<InputSystemUIInputModule>().SetActive(true);

                  
                   
                }
            else
            {
                    //Player.GetComponent<ThirdPersonController>().SetActive(true);
                    //Player.GetComponent<PlayerInput>().SetActive(true);
                    //Player.GetComponent<InputSystemUIInputModule>().SetActive(false);
            }

            
           
            

        }
    }
    */
    private void OnTriggerExit(Collider other)
    {
        //TextE.SetActive(false);
        //PlayerCanvasPrefab.SetActive(false);
        //x = false;
            CanvasViewProfile.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
        
        
    }


    #region close listeners
    public void OnCloseButtonChatClickListener()
    {
        ChatUI.SetActive(false);
    }
     public void OnCloseButtonSelectItemClickListener()
    {
        ItemSelectUI.SetActive(false);
    }
     public void OnCloseButtonTradeClickListener()
    {
        CanvasTrading.SetActive(false);
    }
     public void OnCloseButtonBackpackClickListener()
    {
        BackPackUI.SetActive(false);
    }
    #endregion


    #region trading

    /// New Click Listeners
         
         
    public void OnSlotClickListener()
    {
        Debug.Log("Slot has been clicked");

        ItemSelectUI.SetActive(true);

    }

    public void OnAcceptButtonClickListener()
    {
        Debug.Log("Accepted Button Clicked");
    }

    public void OnDeclineButtonClickListener()
    {
        Debug.Log("Decline Button Clicked");
    }



    ///

    public void onBackToProfileClick()
    {
        CanvasTrading.SetActive(false);
        CanvasViewProfile.SetActive(true);

        Debug.Log("onBackToProfileClick");
    }
    public void onSellWood()
    {
        if (player.GetComponent<OpenWorldInventory>() == null)
        {
            Debug.Log("Null");
        }
        else
            Debug.Log("Not null");


        if (InventoryMine == null)
        {
            Debug.Log("Null M");
        }
        else
            Debug.Log("Not null M");

     //   wood.text = "335";

       /*
        StartCoroutine(playerOwnWorldData.FetchPlayerData(result =>
        {
            Debug.Log("Fetching Data in Canvas");

            playerOwnWorldData = result;
            Debug.Log("Feched Data: " + result);

            Debug.Log("wood: " + playerOwnWorldData.playerData.woodCount + " stone: " + playerOwnWorldData.playerData.stoneCount + " gameID: " + player.GetComponent<PlayerData>().playerName);
            
         //   wood.text = (playerOwnWorldData.playerData.woodCount-100).ToString();
            Debug.Log("woodChanged: " + playerOwnWorldData.playerData.woodCount + " stone: " + playerOwnWorldData.playerData.stoneCount + " gameID: " + player.GetComponent<PlayerData>().playerName);


        }));
        */            
        playerOwnWorldData.playerData.woodCount -= countWood;
        Debug.Log("wood Out: " + playerOwnWorldData.playerData.woodCount);

        StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
            //Debug.Log(result);

            Debug.Log("data Uploaded: " + playerOwnWorldData.playerData.woodCount + "woodCount: "+countWood);
            wood.text = playerOwnWorldData.playerData.woodCount.ToString();
            fech = true;

        }));
        

        InventoryMine.GetComponent<OpenWorldInventory>().LessWood(countWood);
       // player.GetComponent<OpenWorldInventory>().AddWood(countWood, true);


      
      //  Debug.Log(" Opponent Wood: " + player.GetComponent<OpenWorldInventory>().woodAmount);
       // Debug.Log("Own wood: " + InventoryMine.GetComponent<OpenWorldInventory>().woodAmount);
      //  Debug.Log("Opponent wood: " + player.GetComponent<OpenWorldInventory>().woodAmount);

        Debug.Log("onSellWood() ");
    }
    public void onBuyWood()
    {

        playerOwnWorldData.playerData.woodCount += countWood;
        Debug.Log("wood Out: " + playerOwnWorldData.playerData.woodCount);

        StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
            //Debug.Log(result);

            Debug.Log("data Uploaded: " + playerOwnWorldData.playerData.woodCount + "woodCount: " + countWood);
            wood.text = playerOwnWorldData.playerData.woodCount.ToString();
            fech = true;

        }));
        InventoryMine.GetComponent<OpenWorldInventory>().AddWood(countWood, true);
        player.GetComponent<OpenWorldInventory>().LessWood(countWood);


        Debug.Log("onBuyWood()");
    }
    public void onSellStone()
    {
        playerOwnWorldData.playerData.stoneCount -= countStone;
        Debug.Log("wood Out: " + playerOwnWorldData.playerData.stoneCount);

        StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
            //Debug.Log(result);

            Debug.Log("data Uploaded: " + playerOwnWorldData.playerData.stoneCount + "Count: " + countStone);
            stone.text = playerOwnWorldData.playerData.stoneCount.ToString();
            fech = true;

        }));

        InventoryMine.GetComponent<OpenWorldInventory>().LessStone(countStone);
        player.GetComponent<OpenWorldInventory>().AddStone(countStone, true);


        Debug.Log(" onSellStone()");
    }
    public void onBuyStone()
    {
        playerOwnWorldData.playerData.stoneCount += countStone;
        Debug.Log("wood Out: " + playerOwnWorldData.playerData.stoneCount);

        StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
            //Debug.Log(result);

            Debug.Log("data Uploaded: " + playerOwnWorldData.playerData.stoneCount + "woodCount: " + countStone);
            stone.text = playerOwnWorldData.playerData.stoneCount.ToString();
            fech = true;

        }));
        InventoryMine.GetComponent<OpenWorldInventory>().AddStone(countStone, true);
        player.GetComponent<OpenWorldInventory>().LessStone(countStone);
        Debug.Log("onBuyStone()");
    }
    public void onUpStone()
    {
        countStone += 20;
        stoneInputField.text = countStone.ToString();
        Debug.Log("onUpStone()");
    }
    public void onDownStone()
    {
        if (countStone - 20 >= 0)
            countStone -= 20;
        stoneInputField.text = countStone.ToString();
        Debug.Log("onDownStone()");
    }
    public void onUpWood()
    {
        countWood += 20;
        woodInputField.text = countWood.ToString();
        Debug.Log("onUpWood(): "+ countWood.ToString());
    }
    public void onDownWood()
    {
        if (countWood - 20 >= 0)
           countWood -= 20;

        woodInputField.text = countWood.ToString();


        Debug.Log("onDownWood : "+ countWood.ToString());
    }
    #endregion

    #region profile 
    public void onUICloaseClick(){
        CanvasViewProfile.SetActive(false);
        Debug.Log("onUICloaseClick()");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void onTradeClick(){
        Debug.Log(" onTradeClick()");
        CanvasViewProfile.SetActive(false);
        CanvasTrading.SetActive(true);

        woodInputField.text = "0";
        stoneInputField.text = "0";

        //  CanvasViewProfile.SetActive(true);
        //            Cursor.visible = true;
        //           Cursor.lockState = CursorLockMode.None;

    }
    public void onBackFromProfileView(){
       Debug.Log("onBackFromProfileView()");
        CanvasViewProfile.SetActive(true);
        CanvasTrading.SetActive(false);
    }
 
    public void onAddFriend()
    {
        //playerOwnWorldData.plummie_tag
        Debug.Log("On Add Friend: "+ playerOwnWorldData.plummie_tag);
        playerOwnWorldData.listOfFriends.Add("SagarH");

        
    }
    #endregion
}
