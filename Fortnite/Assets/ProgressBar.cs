using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{

    [SerializeField]
    private Image _bar;
    private float timer = 15.0f;
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    private void Update()
    {
        _bar.fillAmount += 1.0f / timer * Time.deltaTime;
    }
}
