using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ItemSelection : MonoBehaviour
{

    [SerializeField]
    TradingData tradingData;
    public List<Sprite> sprites;
    public Button selectedItem;
    public int itemId = -1;
    public int selectedAmount = -1;
    public GameObject amountTMPInputField;
    string amount;


    void Awake()
    {
        itemId = -1;
        selectedAmount = -1;
        selectedItem.GetComponent<Image>().sprite = sprites[3];

    }


    void OnEnable()
    {
        itemId = -1;
        selectedAmount = -1;
        selectedItem.GetComponent<Image>().sprite = sprites[3];

    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickCoinListener()
    {
         selectedItem.GetComponent<Image>().sprite = sprites[0];
         itemId = 0;
    }
    public void OnClickWoodListener()
    {
         selectedItem.GetComponent<Image>().sprite = sprites[1];  
         itemId = 1;
      
    }
    public void OnClickRockListener()
    {
         selectedItem.GetComponent<Image>().sprite = sprites[2];    
         itemId = 2;
    }
    public void OnClickDone()
    {
        amount = amountTMPInputField.GetComponent<TMP_InputField>().text;
        selectedAmount =  int.Parse(amount);
        
        tradingData.selectedId = itemId;
        tradingData.selectedAmount = selectedAmount;



        Debug.Log("ItemID: "+itemId+" amount: "+selectedAmount);
        gameObject.SetActive(false);
    }
}
