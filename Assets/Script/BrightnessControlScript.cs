using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessControlScript : MonoBehaviour
{
    public CameraFilterPack_Colors_Brightness src;
    float targetB;
    float spd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        src._Brightness = src._Brightness - (src._Brightness - targetB) * spd *Time.deltaTime;
    }

    public void Flash()
    {
        src.enabled = true;
        src._Brightness = 2;
        targetB = 1;
        spd = 10f;
        Invoke("OFF", 1f);
    }

    public void FlashFadeOut()
    {
        src.enabled = true;
        targetB = 2;
        spd = 1f;
        Invoke("OFF", 5f);
    }

    public void FlashFadeIn()
    {
        src.enabled = true;
        src._Brightness = 2;
        targetB = 1;
        spd = 1f;
        Invoke("OFF", 5f);
    }

    public void FadeSwitch()
    {
        src.enabled = true;
        targetB = 0;
        spd = 1f;
        Invoke("FadeIn",5f);
    }

    public void FadeIn()
    {
        src.enabled = true;
        targetB = 1;
        spd = 1f;
        Invoke("OFF", 5f);
    }

    void OFF()
    {
        src.enabled = false;
        enabled = false;
    }
}
