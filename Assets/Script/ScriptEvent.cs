using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class ScriptEvent : MonoBehaviour
{
    public TextAsset ScriptXml;
    public UnityEngine.UI.Text TextBox;
    public UnityEngine.UI.Image Cha;
    public UnityEngine.UI.Image BackGroundImage;
    public GameObject Effect;
    public Animator ChaAnim;
    public AudioSource SFXSource;
    private XmlDocument LocalizeFile = new XmlDocument();
    private XmlNodeList EventNodes;
    private XmlNode currentNode;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Language", "Korean");
        SetDB();
        getEvent("후배1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDB()
    {
        LocalizeFile.LoadXml(ScriptXml.text);
    }

    public void getEvent(string key)
    {
        EventNodes = LocalizeFile.SelectNodes("Root/Unit[@key='" + key + "']");
        i = 0;
    }

    public void getText()
    {
        if (i >= EventNodes.Count)
            return;
        currentNode = EventNodes.Item(i);
        i++;
        if (currentNode.SelectSingleNode("Type") == null) {
            if(currentNode.SelectSingleNode("Name") != null)
            {
                TextBox.text = "<color=#6799FF>[" + currentNode.SelectSingleNode("Name").InnerText + "]</color>\n";
            }
            else
            {
                TextBox.text = "";
            }
            TextBox.text += currentNode.SelectSingleNode(PlayerPrefs.GetString("Language")).InnerText;
        }
        else
        {
            int type = System.Convert.ToInt16(currentNode.SelectSingleNode("Type").InnerText);
            if(type == 2)
            {
                Cha.enabled = true;
                Cha.sprite = Resources.Load<Sprite>(currentNode.SelectSingleNode("Korean").InnerText);
            }else if(type == 3)
            {
                SFXSource.PlayOneShot(Resources.Load<AudioClip>(currentNode.SelectSingleNode("Korean").InnerText));
            }else if(type == 4)
            {
                ShortEffect(currentNode.SelectSingleNode("Korean").InnerText);
            }
            getText();
        }
    }

    void ShortEffect(string ef)
    {
        if(ef == "흔들림")
        {
            ShakeScript src = Effect.GetComponent<ShakeScript>();
            src.enabled = true;
            src.ShortShake();
            //src.Invoke("OFF", 1000f);
        }
        else if(ef == "플래시")
        {
            BrightnessControlScript src = Effect.GetComponent<BrightnessControlScript>();
            src.enabled = true;
            src.Flash();
        }
        else if(ef == "바운스")
        {
            ChaAnim.SetTrigger("Bounce");
        }
        else if(ef == "이미지오프")
        {
            Cha.enabled = false;
        }
    }
}
