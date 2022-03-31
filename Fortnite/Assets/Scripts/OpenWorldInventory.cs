using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class OpenWorldInventory : MonoBehaviour
{

    [SerializeField]
    private List<TMPro.TextMeshProUGUI> list;                       //List element0 woodCount, element1 stoneCount, element2 coinCount

    [SerializeField]
    private List<int> resourceAmount;

    private RockControll rockControll;
    [SerializeField]
    GameObject axe;
    [SerializeField]
    Collider mc;
    [HideInInspector]
    public int woodAmount;
    [HideInInspector]
    public int stoneAmount;
    [HideInInspector]
    public int coinAmount;

    private int maxWidth;
    private long maxCap;


    [SerializeField]
    private List<Image> fillBar;                                    //List element0 woodFill, element1 stoneFill, element2 coinFill

    [SerializeField]
    GameObject floatingPoint;

    [SerializeField]
    Vector3 floatingPos;


    PlayerControls playerControls;
    PlayerOwnWorldData playerOwnWorldData;
    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    private float timeForFloat = 1f;
    private float showTime;

    private Collider ownCol;
    int c = 0;
    private bool addFlag = false;
    private float addTime = 5f;



    private void Awake()
    {
        /*
        resourceAmount.Add(woodAmount);
        resourceAmount.Add(stoneAmount);
        resourceAmount.Add(coinAmount);             //resourceAmount ele0 woodAmount, ele1 stoneAmount, ele2 coinAmount
*/

        playerControls = new PlayerControls();
        playerOwnWorldData = new PlayerOwnWorldData();
        playerOwnWorldData.playerData = new PlayerOwnWorldData.PlayerDataOwn();
        
        ownCol = gameObject.GetComponent<Collider>();

        ownCol = mc;


    }

    private void OnEnable()
    {
        playerControls.Enable();
    }


    private void OnDisable()
    {
        playerControls.Disable();
    }



    // Start is called before the first frame update
    void Start()
    {
        /*
         * Need to write code to assign the intial values of woodAmount, stoneAmount, cointAmount when the player loads the game. This data is collected from database.
         * If there is no data present then we will consider this user as a new player and assign the intial value as 0 and update the database. Here I only wrote the script
         * as if the player is a new player
         */
       // woodAmount = playerData.woodCnt;

       woodAmount = playerData.woodCnt;
       stoneAmount = playerData.stoneCnt;
       coinAmount = playerData.coinCnt;
       Debug.Log("Wood Count in Start: " + woodAmount + "  Stone Amount : "+stoneAmount);

        UpdateResources();

        maxWidth = 224;
        maxCap = 10000000000;

        resourceAmount.Add(woodAmount);
        resourceAmount.Add(stoneAmount);
        resourceAmount.Add(coinAmount);

    }

    
    
    private void Update()
    {

         Debug.Log("Wood CNT : "+playerData.woodCnt + " Stone CNT: "+playerData.stoneCnt + " StoneAmount :"+stoneAmount + " WoodAmount : "+woodAmount );
//        if(list[0].text != playerData.woodCnt.ToString() || list[1].text != playerData.stoneCnt.ToString() || list[2].text!= playerData.coinCnt.ToString())

        if(list[0].text != woodAmount.ToString() || list[1].text != stoneAmount.ToString() || list[2].text!= coinAmount.ToString())
        {
            woodAmount = playerData.woodCnt;
            stoneAmount = playerData.stoneCnt;
            coinAmount = playerData.coinCnt;

            resourceAmount.Add(woodAmount);
            resourceAmount.Add(stoneAmount);
            resourceAmount.Add(coinAmount);


            AddWood(0,false);
            AddStone(0,false);
            AddCoin(0,false);

        }
        else
        {
         Debug.Log("list0: "+list[0].text+ " wood :"+woodAmount.ToString());

        }

    
        if(axe.GetComponent<AxeHit>().hitEffect==true && !addFlag)
        {
             Debug.Log("Hit effect true;");
             c++;
             addFlag = true;
            showTime = timeForFloat;
            addTime = 1f;
            int val = Random.Range(100, 999);
            AddStone(val,true);
        }

        if(addTime>0.0f)
            addTime-=Time.deltaTime;
        else
            addFlag = false;
       
        Debug.Log("Wood Amount in inventory : "+woodAmount + " Stone amount: "+stoneAmount);

    }
    



    // Update is called once per frame
    void FixedUpdate()
    {


        for (int i = 0; i < 3; i++)
        {
            int temp;
            string tempText;
            //Image img;

            temp = resourceAmount[i];

       
   
            tempText = "";
            //img = fillBar[i];
            int x;

            x = resourceAmount[i];
            x = x * 2;
            x = x / 100000000;

           if (resourceAmount[i] <= 10000000000)
            {
                fillBar[i].rectTransform.sizeDelta = new Vector2(x, fillBar[i].rectTransform.sizeDelta.y);
            }

            else
            {
                resourceAmount[i] = 1000000;
                fillBar[i].rectTransform.sizeDelta = new Vector2(maxWidth, fillBar[i].rectTransform.sizeDelta.y);
            }
    /*

            if (temp < 1000)
            {
                tempText = "";
            }

            if (temp >= 1000)
            {
                temp = temp / 1000;
                tempText = "K";
            }
            if (temp >= 1000)
            {
                temp = temp / 1000;
                tempText = "M";
            }
            if (temp >= 1000)
            {
                temp = temp / 1000;
                tempText = "B";
            }
            */

            list[i].text = temp.ToString() + tempText;
        }
    }

    #region Public Methods
    public void AddWood(int val,bool flag)
    {
        woodAmount += val;
        UpdateResources();
        if(flag==true)
           Show(val);
        Debug.Log("val: " + val + "woodAmount: " + woodAmount);
        /*GameObject floating = Instantiate(floatingPoint, floatingPos, Quaternion.identity, gameObject.transform) as GameObject;
        floating.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = val.ToString();*/


    }
    public void AddStone(int val,bool flag)
    {
        stoneAmount += val;
        UpdateResources();      
        if(flag==true)
           Show(val);

    }
    public void AddCoin(int val, bool flag)
    {
        coinAmount += val;
        UpdateResources();
        if(flag==true)
          Show(val);
    }

    public void LessWood(int val)
    {
        woodAmount -= val;
        UpdateResources();
    }
    public void LessStone(int val)
    {
        stoneAmount -= val;
        UpdateResources();
    }
    public void LessCoin(int val)
    {
        coinAmount -= val;
        UpdateResources();
    }

    #endregion


    private void UpdateResources()
    {
        resourceAmount[0] = woodAmount;
        resourceAmount[1] = stoneAmount;
        resourceAmount[2] = coinAmount;
    }

    private void Show(int val)
    {
        GameObject floating = Instantiate(floatingPoint, floatingPos, Quaternion.identity, gameObject.transform) as GameObject;
        floating.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "+ " + val.ToString();
    }



}
