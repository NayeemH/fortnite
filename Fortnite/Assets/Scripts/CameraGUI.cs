using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CameraGUI : MonoBehaviour
{
    public InputAction guicontrols;


    [Header("Image")]
    public Image img;
    
    
    
    private Vector3 position;
    // Start is called before the first frame update

    [SerializeField] float count;
   

    void Start()
    {
        
        count = 0;
        position = new Vector3(-425, -180, 0);
    }

    private void OnEnable()
    {
        guicontrols.Enable();
    }
    private void OnDisable()
    {
        guicontrols.Disable();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float currentValue = guicontrols.ReadValue<float>();
        
        if(currentValue>0)
        {
            currentValue = 1;
            ChangeCount(currentValue);
        }
        else if(currentValue<0)
        {
            currentValue = -1;
            ChangeCount(currentValue);
        }

        if (count == 0)
        {
           img.rectTransform.localPosition = position;
        }
        else if (count == 1)
        {
            Vector3 temp = position;
            temp.x += 100;
            img.rectTransform.localPosition = temp;

        }
        else if (count == 2)
        {
            Vector3 temp = position;
            temp.x += 200;
            img.rectTransform.localPosition = temp;
        }

    }

    void ChangeCount(float val)
    {
        count += val;
       
        if(count == 3)
        {
            count = 0;
        }
        if(count == -1)
        {
            count = 2;
        }
    }

    public float CurrentCount()
    {
        return count;
    }
}
