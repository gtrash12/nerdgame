using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuetoTextScript : MonoBehaviour
{
    public int value;
    public UnityEngine.UI.Text txt;
    // Start is called before the first frame update

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
}
