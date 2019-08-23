using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessControlScript : MonoBehaviour
{
    public CameraFilterPack_Colors_Brightness src;
    float targetB;
    float spd;
    string next;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        src._Brightness -= (src._Brightness - targetB)/Mathf.Abs(src._Brightness - targetB) * spd *Time.deltaTime;
        if(Mathf.Abs(src._Brightness - targetB) < spd * Time.deltaTime)
        {
            src._Brightness = targetB;
            if (next != "")
                Invoke(next,0);
        }
    }

    public void Flash()
    {
        src.enabled = true;
        src._Brightness = 2;
        targetB = 1;
        spd = 3f;
        next = "OFF";
        //Invoke("OFF", 1f);
    }

    public void FlashFadeOut()
    {
        src.enabled = true;
        targetB = 2;
        spd = 0.5f;
        next = "";
        //Invoke("OFF", 5f);
    }

    public void FlashFadeIn()
    {
        src.enabled = true;
        src._Brightness = 2;
        targetB = 1;
        spd = 0.5f;
        next = "OFF";
        //Invoke("OFF", 5f);
    }

    public void FadeSwitch()
    {
        src.enabled = true;
        targetB = 0;
        spd = 0.5f;
        next = "FadeIn";
        //Invoke("FadeIn",5f);
    }

    public void FadeIn()
    {
        src.enabled = true;
        src._Brightness = 0;
        targetB = 1;
        spd = 0.5f;
        next = "OFF";
        // Invoke("OFF", 5f);
    }
    public void FadeOut()
    {
        src.enabled = true;
        targetB = 0;
        spd = 0.5f;
        next = "";
    }

    public void Dark()
    {
        src.enabled = true;
        targetB = 0;
        spd = 5;
        next = "";
    }

    void OFF()
    {
        src.enabled = false;
        enabled = false;
    }
}
