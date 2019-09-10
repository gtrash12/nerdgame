using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSelectScript : MonoBehaviour
{
    public UnityEngine.UI.Text 지역이름;
    string key;
    public UnityEngine.UI.Button[] Btns;
    public UnityEngine.UI.Text[] Btn_Names;
    public BattleScript bts;
    public RectTransform flag;
    public Animator flag_Anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void set(string key)
    {
        this.key = key;
        지역이름.text = name;
        int count = bts.episodecount(key);
        for(int i = 0; i < count; i++)
        {
            Btns[i].gameObject.SetActive(true);
            Btn_Names[i].text = key+(i+1)+"화";
            if (PlayerPrefs.GetInt(key, 0) > i)
                Btns[i].interactable = true;
            else
                Btns[i].interactable = false;
        }
        for (int i = count; i < Btns.Length; i++)
        {
            Btns[i].gameObject.SetActive(false);
            Btn_Names[i].text = key + (i+1) + "화";
        }
    }

    public void FlagShot(RectTransform target)
    {
        지역이름.text = target.name;
        flag.anchoredPosition = target.anchoredPosition;
        flag_Anim.Rebind();
    }

    public void play(int i)
    {
        bts.Evs.getEvent(key + i);
        bts.Evs.getText();
    }
}
