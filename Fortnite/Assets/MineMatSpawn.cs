using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMatSpawn : MonoBehaviour
{
    [Header("Material Position")]
    [SerializeField]
    private Vector3 centrePos;
    [SerializeField]
    private float spawnRadius;

    [Header("Mining Prefabs")]
    [SerializeField]
    private List<GameObject> miningmat;

    [Header("Properties")]
    [SerializeField]
    private float spawnCoillisonCheck;
    [SerializeField]
    private float spawnAmount;




    // Start is called before the first frame update
    void Start()
    {
        SpawnMat();
       // centrePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMat()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPoint = new Vector3(centrePos.x + Random.Range(-20, 21), 0, centrePos.z + Random.Range(-20, 21));

            if(true)
            {
                Instantiate(miningmat[Random.Range(0, 6)], spawnPoint, Quaternion.identity).transform.parent = transform;
            }
            else
            {
               
            }
        }
    }


}
