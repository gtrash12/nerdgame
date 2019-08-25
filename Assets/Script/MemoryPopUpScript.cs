using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPopUpScript : MonoBehaviour
{
    public UnityEngine.UI.Image Port;
    public UnityEngine.UI.Text NamtT;
    public UnityEngine.UI.Text LevelT;
    public UnityEngine.UI.Image SkillImg;
    public UnityEngine.UI.Text SkillInfo;
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

    public void Set(GirlSlotScript slot)
    {
        string k = slot.Name;
        int lv = slot.lv;
        NamtT.text = k;
        Port.sprite = Resources.Load<Sprite>(k);
        LevelT.text = lv.ToString();
        SkillImg.sprite = slot.SkillImg.sprite;
        if (lv == 5)
        {
            SkillImg.color = Color.white;
            SkillInfo.text = slot.SkillInfo;
        }
        else
        {
            SkillImg.color = Color.gray;
            SkillInfo.text = "호감도 레벨 5 달성시 획득";
        }
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
