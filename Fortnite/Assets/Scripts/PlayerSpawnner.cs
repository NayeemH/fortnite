using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnner : MonoBehaviour
{
    public GameObject player;
 

    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(player,transform.position,transform.rotation);
    }

}
