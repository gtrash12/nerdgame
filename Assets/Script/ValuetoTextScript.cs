using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuetoTextScript : MonoBehaviour
{
    public string connectedValue;
    public int value;
    public UnityEngine.UI.Text txt;
    // Start is called before the first frame update

    private void Start()
    {
        if(connectedValue != "")
        {
            set(PlayerPrefs.GetInt(connectedValue));
        }
    }

    public void set(int v)
    {
        
        value = v;
        txt.text = value.ToString();
    }

    public void Add(int v)
    {
        value += v;
        txt.text = value.ToString();
    }

    string unitCal(int v)
    {
        //if(v >= 100000)
        return "";
    }
}
