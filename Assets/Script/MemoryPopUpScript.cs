using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPopUpScript : MonoBehaviour
{
    public UnityEngine.UI.Image Port;
    public UnityEngine.UI.Text NamtT;
    public UnityEngine.UI.Text LevelT;
    public UnityEngine.UI.Button[] btn1; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(string key)
    {
        NamtT.text = key;
        Port.sprite = Resources.Load<Sprite>(key);
        LevelT.text = PlayerPrefs.GetInt(key + "레벨").ToString();
        int lv = PlayerPrefs.GetInt(key + "레벨");
        for(int i = 0; i < lv; i++)
        {
            btn1[i].interactable = true;
            
        }
        for (int i = lv; i < 5; i++)
        {
            btn1[i].interactable = false;
        }
    }

    public void Set(UnityEngine.UI.Text key)
    {
        NamtT.text = key.text;
        Port.sprite = Resources.Load<Sprite>(key.text);
        LevelT.text = PlayerPrefs.GetInt(key.text + "레벨").ToString();
        int lv = PlayerPrefs.GetInt(key + "레벨");
        for (int i = 0; i < lv; i++)
        {
            btn1[i].interactable = true;

        }
        for (int i = lv; i < 5; i++)
        {
            btn1[i].interactable = false;
        }
    }
}
