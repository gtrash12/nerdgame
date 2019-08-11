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
                Cha.sprite = Resources.Load<Sprite>(currentNode.SelectSingleNode("Korean").InnerText);
            }
            getText();
        }
    }
}
