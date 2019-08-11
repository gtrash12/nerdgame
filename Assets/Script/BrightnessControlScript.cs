using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessControlScript : MonoBehaviour
{
    public CameraFilterPack_Colors_Brightness src;
    float targetB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        src._Brightness = src._Brightness - (src._Brightness - targetB) / 3;
    }

    public void Flash()
    {
        src.enabled = true;
        src._Brightness = 2;
        targetB = 1;
        Invoke("OFF", 1f);
    }

    void OFF()
    {
        src.enabled = false;
        enabled = false;
    }
}
