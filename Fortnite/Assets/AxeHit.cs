using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AxeHit : MonoBehaviour
{
    
    NewInputSystem playerControl;
    public bool hitEffect = false;

    private float hitEffectTime; 
    [SerializeField]
    private float timeForFloat = 0.1f;

    void Awake()
    {
        playerControl = new NewInputSystem();
        hitEffect = false;
    }

     private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }


    void Update()
    {
        if(hitEffectTime>0.0f)
        {
            hitEffectTime-= Time.deltaTime;
        }
        else
        {
            hitEffect = false;
        }
    }




     void OnCollisionEnter(Collision other)
    {   
         //&& playerControls.Player.Hit.ReadValue<float>() == 1
        if (other.collider.tag == "MiningMat" && playerControl.Player.Hit.ReadValue<float>() == 1 && !hitEffect)        
        {
                 Debug.Log("Axe hit from axe: "+ other.collider.tag);

           
            float val = Random.Range(100, 999);

            hitEffectTime = timeForFloat;

            hitEffect = true;

  
/*

            if (countHit > destroyHit)
            {
                Debug.Log("Destroy");
                Destroy(gameObject);
            }
*/
        }
       

    }
}
