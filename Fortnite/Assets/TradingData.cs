 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;



public class TradingData : MonoBehaviour
{
     public CanvasCollider canvasCollider;
     public List<Sprite> sprites;
     public List<GameObject> slots;
     public List<GameObject> slots2;
     public List<TextMeshProUGUI> amountTexts;
     public List<TextMeshProUGUI> amountTexts2;
     public List<int> coinVal;
     public GameObject ItemSelectUI;
     public TextMeshProUGUI coinValTotal;
     public TextMeshProUGUI coinValTotal2;
     public int selectedId = -1;
     public int selectedAmount = -1;
     public int slotID = -1;
     public int coinPrice = 0;
     public int coinPrice2 = 0;
    


    string playerName;
    string traderName;
    PlayerOwnWorldData playerOwnWorldData;
    PlayerOwnWorldData playerOwnWorldDataOpponent;

    bool tradeFlag = false;
    public int playerTradeCoin, playerTradeWood, playerTradeStone;
    public int traderTradeCoin, traderTradeWood, traderTradeStone;




    void Awake()
    {
        selectedId = -1;
        selectedAmount = -1;
        slotID = -1;
        coinPrice = 0;
        playerName = canvasCollider.playerName;
  
        traderName = canvasCollider.GameIDOpponent;
        
        Debug.Log("PlayerName: "+playerName+ "  Tradername: "+traderName);

        playerOwnWorldData = new PlayerOwnWorldData();
        playerOwnWorldData.playerData = new PlayerOwnWorldData.PlayerDataOwn();
        playerOwnWorldData.trading = new PlayerOwnWorldData.Trading();

        
        playerOwnWorldData.plummie_tag = playerName;/*
        StartCoroutine(playerOwnWorldData.FetchPlayerData(result => {
            Debug.Log("Fetching Data: ");
            Debug.Log(result);
            playerOwnWorldData = result;

            Debug.Log("Stone Amt: "+playerOwnWorldData.playerData.stoneCount+ " Wood Amt: "+ playerOwnWorldData.playerData.woodCount);

            if(playerOwnWorldData.trading.traderId == traderName)
            {
                int slot0ID =  playerOwnWorldData.trading.idOfslot0-1;
                int slot1ID =  playerOwnWorldData.trading.idOfslot1-1;
                int slot2ID =  playerOwnWorldData.trading.idOfslot2-1;
                int slot3ID =  playerOwnWorldData.trading.idOfslot3-1;

                int slot0Amt = playerOwnWorldData.trading.amountOfslot0;
                int slot1Amt = playerOwnWorldData.trading.amountOfslot1;
                int slot2Amt = playerOwnWorldData.trading.amountOfslot2;
                int slot3Amt = playerOwnWorldData.trading.amountOfslot3;
                

               if(slot0ID>0){
                slots[0].GetComponent<Image>().sprite = sprites[slot0ID];
                amountTexts[0].text = slot0Amt.ToString();
                coinPrice += slot0Amt * coinVal[slot0ID];
                coinValTotal.text = coinPrice.ToString();
               }
               if(slot1ID>0){
                slots[1].GetComponent<Image>().sprite = sprites[slot1ID];
                amountTexts[1].text = slot1Amt.ToString();
                coinPrice += slot1Amt * coinVal[slot1ID];
                coinValTotal.text = coinPrice.ToString();
               }
               if(slot2ID>0){
                slots[2].GetComponent<Image>().sprite = sprites[slot2ID];
                amountTexts[2].text = slot2Amt.ToString();
                coinPrice += slot2Amt * coinVal[slot2ID];
                coinValTotal.text = coinPrice.ToString();
               }
               if(slot3ID>0){
                slots[3].GetComponent<Image>().sprite = sprites[slot3ID];
                amountTexts[3].text = slot3Amt.ToString();
                coinPrice += slot3Amt * coinVal[slot3ID];
                coinValTotal.text = coinPrice.ToString();
               }
               

            }

        }));
*/
//////////////////////////////////////////////////////////////////////////////

      

        playerOwnWorldDataOpponent = new PlayerOwnWorldData();
        playerOwnWorldDataOpponent.playerData = new PlayerOwnWorldData.PlayerDataOwn();
        playerOwnWorldDataOpponent.trading = new PlayerOwnWorldData.Trading();



       if(traderName!=null){

        playerOwnWorldDataOpponent.plummie_tag = traderName;}/*
        Debug.Log("Trader in Awake: "+traderName);
        StartCoroutine(playerOwnWorldDataOpponent.FetchPlayerData(result => {
            Debug.Log("Fetching Data: ");
            Debug.Log(result);
            playerOwnWorldDataOpponent = result;

            Debug.Log("Stone Amt: "+playerOwnWorldDataOpponent.playerData.stoneCount+ " Wood Amt: "+ playerOwnWorldDataOpponent.playerData.woodCount);
           Debug.Log("pplayerName: "+playerName+" trdaerID: "+playerOwnWorldDataOpponent.trading.traderId);
            if(playerOwnWorldDataOpponent.trading.traderId == playerName)
            {
                int slot0ID =  playerOwnWorldDataOpponent.trading.idOfslot0-1;
                int slot1ID =  playerOwnWorldDataOpponent.trading.idOfslot1-1;
                int slot2ID =  playerOwnWorldDataOpponent.trading.idOfslot2-1;
                int slot3ID =  playerOwnWorldDataOpponent.trading.idOfslot3-1;

                int slot0Amt = playerOwnWorldDataOpponent.trading.amountOfslot0;
                int slot1Amt = playerOwnWorldDataOpponent.trading.amountOfslot1;
                int slot2Amt = playerOwnWorldDataOpponent.trading.amountOfslot2;
                int slot3Amt = playerOwnWorldDataOpponent.trading.amountOfslot3;
                

               if(slot0ID>0){
                slots2[0].GetComponent<Image>().sprite = sprites[slot0ID];
                amountTexts2[0].text = slot0Amt.ToString();
                coinPrice2 += slot0Amt * coinVal[slot0ID];
                coinValTotal2.text = coinPrice2.ToString();
               }
               if(slot1ID>0){
                slots2[1].GetComponent<Image>().sprite = sprites[slot1ID];
                amountTexts2[1].text = slot1Amt.ToString();
                coinPrice2 += slot1Amt * coinVal[slot1ID];
                coinValTotal2.text = coinPrice2.ToString();
               }
               if(slot2ID>0){
                slots2[2].GetComponent<Image>().sprite = sprites[slot2ID];
                amountTexts2[2].text = slot2Amt.ToString();
                coinPrice2 += slot2Amt * coinVal[slot2ID];
                coinValTotal2.text = coinPrice2.ToString();
               }
               if(slot3ID>0){
                slots2[3].GetComponent<Image>().sprite = sprites[slot3ID];
                amountTexts2[3].text = slot3Amt.ToString();
                coinPrice2 += slot3Amt * coinVal[slot3ID];
                coinValTotal2.text = coinPrice.ToString();
               }
               

            }

        }));


       }
       else{
           Debug.Log("Traders id is null");
       }
*/

    }


    void Update()
    {
        Debug.Log("in Update of trading");
/*
        Debug.Log("TraderID : "+canvasCollider.GameIDOpponent);

            if(canvasCollider.GameIDOpponent!=null)
                traderName = canvasCollider.GameIDOpponent;
*/
        Debug.Log("slotID: "+slotID+ " selectedID: "+selectedId+ " selectedAmount: "+selectedAmount);
        if(slotID != -1 && selectedId != -1 && selectedAmount !=-1)
        {
            
            slots[slotID].GetComponent<Image>().sprite = sprites[selectedId];
            amountTexts[slotID].text = selectedAmount.ToString();

            coinPrice += selectedAmount * coinVal[selectedId];
            coinValTotal.text = coinPrice.ToString();

            Debug.Log("Inventory te full y called");



            ////////////////

            if(slotID==0)
            {
             playerOwnWorldData.trading.traderId = traderName;
             playerOwnWorldData.trading.idOfslot0 = selectedId+1;   
             playerOwnWorldData.trading.amountOfslot0 = selectedAmount;
            }
            if(slotID==1)
            {
             playerOwnWorldData.trading.traderId = traderName;
             playerOwnWorldData.trading.idOfslot1 = selectedId+1;   
             playerOwnWorldData.trading.amountOfslot1 = selectedAmount;
            }
            if(slotID==2)
            {
             playerOwnWorldData.trading.traderId = traderName;
             playerOwnWorldData.trading.idOfslot2 = selectedId+1;   
             playerOwnWorldData.trading.amountOfslot2 = selectedAmount;
            }
            if(slotID==3)
            {
             playerOwnWorldData.trading.traderId = traderName;
             playerOwnWorldData.trading.idOfslot3 = selectedId+1;   
             playerOwnWorldData.trading.amountOfslot3 = selectedAmount;
            }
            
           

            ////////////////

              Debug.Log("player plummie tag: "+playerOwnWorldData.plummie_tag);
             if(playerOwnWorldData != null)
             {
                 Debug.Log("Player Data is not null");   
                 StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
                 Debug.Log("Tag: "+playerOwnWorldData.plummie_tag+" result: "+result);
            }));  

             }
             else{
                 Debug.Log("player data is null");
             }


            
            slotID = -1;
            selectedId = -1;
            selectedAmount = -1;
        }
  
        updateTrader();
        GetItemsCount();

       if(tradeFlag==false) 
        {
          if((playerTradeCoin!=0 || playerTradeStone!=0 || playerTradeWood!=0) && (traderTradeCoin!=0 || traderTradeStone!=0 || traderTradeWood!=0))
        {
            Debug.Log("player: "+playerName+" trader: "+traderName);
            Debug.Log("Oppent Data Before Trade coin: "+playerOwnWorldDataOpponent.playerData.coinCount+" stone: "+playerOwnWorldDataOpponent.playerData.stoneCount+" wood: "+playerOwnWorldDataOpponent.playerData.woodCount);
            Debug.Log("Own Data Before coin: "+playerOwnWorldData.playerData.coinCount+" stone: "+playerOwnWorldData.playerData.stoneCount+" wood: "+playerOwnWorldData.playerData.woodCount);
 

         //if((playerOwnWorldDataOpponent.playerData.coinCount!=0 && 
           updateOwnTrade();
           updateTraderTrade();
           tradeFlag = true;
        }
        }
    }



    
    public void OnClickSlot1()
    {
         slotID = 0;
         ItemSelectUI.SetActive(true);

    }
     public void OnClickSlot2()
    {
         slotID = 1;
         ItemSelectUI.SetActive(true);
        
    }
     public void OnClickSlot3()
    {
         slotID = 2;
         ItemSelectUI.SetActive(true);
        
    }

     public void OnClickSlot4()
    {
         slotID = 3;
         ItemSelectUI.SetActive(true);
        
    }
    public void OnClickAccept()
    {
        
       if( (amountTexts[0].text!="0" || amountTexts[1].text!="0" || amountTexts[2].text!="0") && (amountTexts2[0].text!="0" || amountTexts2[1].text!="0" || amountTexts2[2].text!="0"))
       {
           Debug.Log("Transaction");
           
           updateOwnTrade();
           updateTraderTrade();
       }
      

        

    }
    public void OnClickDecline()
    {

    }


    public void GetItemsCount()
    {
        playerTradeCoin = int.Parse(amountTexts[0].text);
        playerTradeWood = int.Parse(amountTexts[1].text);
        playerTradeStone = int.Parse(amountTexts[2].text);

        traderTradeCoin = int.Parse(amountTexts2[0].text);
        traderTradeWood = int.Parse(amountTexts2[1].text);
        traderTradeStone = int.Parse(amountTexts2[2].text);


        Debug.Log("pCoin: "+playerTradeCoin+"  pStone: "+playerTradeStone+" pWood: "+playerTradeWood);
        Debug.Log("tCoin: "+traderTradeCoin+"  tStone: "+traderTradeStone+" tWood: "+traderTradeWood);

    }


    public void updateTraderTrade()
    {
        /*
                        traderName = canvasCollider.GameIDOpponent;

         playerOwnWorldDataOpponent.plummie_tag = traderName;
        Debug.Log("Trader in Awake: "+traderName);
        StartCoroutine(playerOwnWorldDataOpponent.FetchPlayerData(result => {
            Debug.Log("Fetching Data: ");
            Debug.Log(result);
            playerOwnWorldDataOpponent = result;
        }));
*/


        Debug.Log("Oppent Data  coin: "+playerOwnWorldDataOpponent.playerData.coinCount+" stone: "+playerOwnWorldDataOpponent.playerData.stoneCount+" wood: "+playerOwnWorldDataOpponent.playerData.woodCount);


            

               if(amountTexts[0].text!="0")
                playerOwnWorldDataOpponent.playerData.coinCount+=  int.Parse(amountTexts[0].text);
                
                if(amountTexts[1].text!="0")
                playerOwnWorldDataOpponent.playerData.woodCount+=  int.Parse(amountTexts[1].text);
                
                if(amountTexts[2].text!="0")
                playerOwnWorldDataOpponent.playerData.stoneCount+=  int.Parse(amountTexts[2].text);

                if(amountTexts2[0].text!="0")
                playerOwnWorldDataOpponent.playerData.coinCount-=  int.Parse(amountTexts2[0].text);
                
                if(amountTexts2[1].text!="0")
                playerOwnWorldDataOpponent.playerData.woodCount-=  int.Parse(amountTexts2[1].text);
                
                if(amountTexts2[2].text!="0")
                playerOwnWorldDataOpponent.playerData.stoneCount-=  int.Parse(amountTexts2[2].text);


                Debug.Log("transaction: "+playerOwnWorldDataOpponent.plummie_tag);
                StartCoroutine(playerOwnWorldDataOpponent.SavePlayerData(result => {
                 Debug.Log("Tag: "+playerOwnWorldDataOpponent.plummie_tag+" result: "+result);
            }));  
        Debug.Log("Oppent Data After Trade coin: "+playerOwnWorldDataOpponent.playerData.coinCount+" stone: "+playerOwnWorldDataOpponent.playerData.stoneCount+" wood: "+playerOwnWorldDataOpponent.playerData.woodCount);

    }



    public void updateOwnTrade()
    {
        /*
        playerOwnWorldData = new PlayerOwnWorldData();
        playerOwnWorldData.playerData = new PlayerOwnWorldData.PlayerDataOwn();
        playerOwnWorldData.trading = new PlayerOwnWorldData.Trading();
        playerOwnWorldData.plummie_tag = playerName;
      
         StartCoroutine(playerOwnWorldData.FetchPlayerData(result => {
            Debug.Log("Fetching Data: ");
            Debug.Log(result);
            playerOwnWorldData = result;
         }));  */
                    Debug.Log("Playername: "+playerName+" trdaerID: "+playerOwnWorldData.trading.traderId);

        
                Debug.Log("Own Data  coin: "+playerOwnWorldData.playerData.coinCount+" stone: "+playerOwnWorldData.playerData.stoneCount+" wood: "+playerOwnWorldData.playerData.woodCount);



                if(amountTexts[0].text!="0")
                playerOwnWorldData.playerData.coinCount-=  int.Parse(amountTexts[0].text);
                
                if(amountTexts[1].text!="0")
                playerOwnWorldData.playerData.woodCount-=  int.Parse(amountTexts[1].text);
                
                if(amountTexts[2].text!="0")
                playerOwnWorldData.playerData.stoneCount-=  int.Parse(amountTexts[2].text);

                if(amountTexts2[0].text!="0")
                playerOwnWorldData.playerData.coinCount+=  int.Parse(amountTexts2[0].text);
                
                if(amountTexts2[1].text!="0")
                playerOwnWorldData.playerData.woodCount+=  int.Parse(amountTexts2[1].text);
                
                if(amountTexts2[2].text!="0")
                playerOwnWorldData.playerData.stoneCount+=  int.Parse(amountTexts2[2].text);

                Debug.Log("Transaction stone: "+playerOwnWorldData.playerData.stoneCount+" amount: "+int.Parse(amountTexts2[2].text) +"   " +int.Parse(amountTexts[2].text));


                           Debug.Log("transaction)): "+playerOwnWorldData.plummie_tag);

                StartCoroutine(playerOwnWorldData.SavePlayerData(result => {
                 Debug.Log("Tag: "+playerOwnWorldData.plummie_tag+" result: "+result);
            }));  
                Debug.Log("Own Data After Trade coin: "+playerOwnWorldData.playerData.coinCount+" stone: "+playerOwnWorldData.playerData.stoneCount+" wood: "+playerOwnWorldData.playerData.woodCount);

    }


    public void updateTrader()
    {
                traderName = canvasCollider.GameIDOpponent;
                playerOwnWorldDataOpponent.plummie_tag = traderName;


        
       if(traderName!=null){

        playerOwnWorldDataOpponent.plummie_tag = traderName;
        Debug.Log("Trader in Awake: "+traderName);
        StartCoroutine(playerOwnWorldDataOpponent.FetchPlayerData(result => {
            Debug.Log("Fetching Data: ");
            Debug.Log(result);
            playerOwnWorldDataOpponent = result;

            Debug.Log("Stone Amt: "+playerOwnWorldDataOpponent.playerData.stoneCount+ " Wood Amt: "+ playerOwnWorldDataOpponent.playerData.woodCount);
           Debug.Log("pplayerName: "+traderName+" trdaerID: "+playerOwnWorldDataOpponent.trading.traderId);
            if(playerOwnWorldDataOpponent.trading.traderId == playerName)
            {
                int slot0ID =  playerOwnWorldDataOpponent.trading.idOfslot0-1;
                int slot1ID =  playerOwnWorldDataOpponent.trading.idOfslot1-1;
                int slot2ID =  playerOwnWorldDataOpponent.trading.idOfslot2-1;
                int slot3ID =  playerOwnWorldDataOpponent.trading.idOfslot3-1;

                int slot0Amt = playerOwnWorldDataOpponent.trading.amountOfslot0;
                int slot1Amt = playerOwnWorldDataOpponent.trading.amountOfslot1;
                int slot2Amt = playerOwnWorldDataOpponent.trading.amountOfslot2;
                int slot3Amt = playerOwnWorldDataOpponent.trading.amountOfslot3;
                

               if(slot0ID>0){
                slots2[0].GetComponent<Image>().sprite = sprites[slot0ID];
                amountTexts2[0].text = slot0Amt.ToString();
                coinPrice2 = slot0Amt * coinVal[slot0ID];
                coinValTotal2.text = coinPrice2.ToString();
               }
               if(slot1ID>0){
                slots2[1].GetComponent<Image>().sprite = sprites[slot1ID];
                amountTexts2[1].text = slot1Amt.ToString();
                coinPrice2 = slot1Amt * coinVal[slot1ID];
                coinValTotal2.text = coinPrice2.ToString();
               }
               if(slot2ID>0){
                slots2[2].GetComponent<Image>().sprite = sprites[slot2ID];
                amountTexts2[2].text = slot2Amt.ToString();
                coinPrice2 = slot2Amt * coinVal[slot2ID];
                coinValTotal2.text = coinPrice2.ToString();
               }
               if(slot3ID>0){
                slots2[3].GetComponent<Image>().sprite = sprites[slot3ID];
                amountTexts2[3].text = slot3Amt.ToString();
                coinPrice2 = slot3Amt * coinVal[slot3ID];
                coinValTotal2.text = coinPrice.ToString();
               }
               

            }

        }));


       }
       else{
           Debug.Log("Traders id is null");
       }

    }
    



}
