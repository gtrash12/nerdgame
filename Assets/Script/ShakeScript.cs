using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public CameraFilterPack_FX_EarthQuake src;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShortShake()
    {
        src.enabled = true;
        Invoke("OFF", 0.2f);
    }

    public void PeriodicShake()
    {
        src.enabled = true;
        Invoke("stopShake", 0.2f);
        Invoke("PeriodicShake", 0.4f);
    }

    void stopShake()
    {
        src.enabled = false;
    }

    public void OFF()
    {
        CancelInvoke();
        src.enabled = false;
        enabled = false;
    }

}
