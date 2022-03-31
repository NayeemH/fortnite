using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CanvasController : MonoBehaviour
{
   public GameObject Player;
   NewInputSystem playerControl;


    private void Awake()
    {
        playerControl = new NewInputSystem();       

    }


    void Update()
    {
        if(playerControl.Player.Chat.triggered)
        {
               Debug.Log("Click from Chat");
              Player.GetComponent<PlayerInput>().enabled = false;
        }
        else
        {

        }

    }
    












   
   



}
