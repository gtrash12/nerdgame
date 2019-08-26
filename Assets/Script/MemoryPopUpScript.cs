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
    public ScriptEvent evt;
    public GirlSlotScript current;

    private string name;

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
        current = slot;
        name = slot.Name;
        int lv = slot.lv;
        NamtT.text = name;
        Port.sprite = Resources.Load<Sprite>(name);
        SkillImg.sprite = slot.SkillImg.sprite;
        if (lv == 6)
        {
            SkillImg.color = Color.white;
            SkillInfo.text = slot.SkillInfo;
            LevelT.text = "MAX";
        }
        else
        {
            SkillImg.color = Color.gray;
            SkillInfo.text = "호감도 레벨 MAX 달성시 획득";
            LevelT.text = lv.ToString();
        }
        for (int i = 1; i < lv; i++)
        {
            btn1[i-1].interactable = true;

        }
        if (slot.ep)
        {
            btn1[lv-1].interactable = true;
            lv++;
        }
        for (int i = lv-1; i < 5; i++)
        {
            btn1[i].interactable = false;
        }
    }

    public void epsisode(int l)
    {
        evt.getEvent(name + l);
        evt.getText();
    }
}
