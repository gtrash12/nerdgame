using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableScript : MonoBehaviour
{
    public UnityEngine.UI.Image my;
    Color c;


    public void flash()
    {
        my.color = Color.white;
        c = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        c.a -= Time.deltaTime*5;
        if(c.a < 0.01f)
        {
            gameObject.SetActive(false);
        }
        my.color = c;
    }
}
