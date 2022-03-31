using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerPrefab;
    //public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = playerPrefab.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
        transform.rotation = Quaternion.Euler(90f, playerPrefab.eulerAngles.y, 0f);
        //transform.position = playerPrefab.position + offset;
       // Vector3 rotate = new Vector3(90, playerPrefab.eulerAngles.y, 0);
       // transform.rotation = Quaternion.Euler(rotate);
    }
}
