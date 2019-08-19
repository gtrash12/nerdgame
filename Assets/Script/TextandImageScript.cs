using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextandImageScript : MonoBehaviour
{
    public UnityEngine.UI.Image Image;
    public UnityEngine.UI.Text Text;
    // Start is called before the first frame update


    public void set(Sprite img, string txt)
    {
        Image.sprite = img;
        Text.text = txt;
    }
}
