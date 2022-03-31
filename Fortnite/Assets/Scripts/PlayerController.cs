using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public Rigidbody player;
    public float force;
    public float jumpForce;

    [HideInInspector]
    public bool f = false;

    [SerializeField]
    TextMeshProUGUI playerNametext;

   private void Start()
    {
        setPlayerUI();
    }
    void setPlayerUI()
    {
        if(playerNametext!=null)
        playerNametext.text = "Nahid";
        //photonView.Owner.NickName;
    }


    private void OnCollisionEnter(Collision collision)
    {
                    Debug.Log("collided "+collision.gameObject.tag+ " "+collision.gameObject.name);


        if(collision.gameObject.tag == "nextPlayer")
        {
            Debug.Log("collided "+collision.gameObject.tag);
        }
    }

    

    void FixedUpdate()
    {
        if (Input.GetKey("j"))
        {
            player.AddForce(0f, 0f, -1f*force * Time.deltaTime);
        }
        else if (Input.GetKey("l"))
        {
            player.AddForce(0f, 0f, 1f * force * Time.deltaTime);
        }
        else if (Input.GetKey("i"))
        {
            player.AddForce(-1*force * Time.deltaTime, 0f, 0f);
        }
        else if (Input.GetKey("k"))
        {
            player.AddForce(1f*force * Time.deltaTime, 0f, 0f);
        }

       
    }
}


