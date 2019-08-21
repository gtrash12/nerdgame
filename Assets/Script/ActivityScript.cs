using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityScript : MonoBehaviour
{
    public string Key;
    public UnityEngine.UI.Text name;
    public UnityEngine.UI.Image img;
    public Animator Anim;
    public List<TextandImageScript> Costs = new List<TextandImageScript>();
    public List<TextandImageScript> Ups = new List<TextandImageScript>();

    List<itAttribute> costlist = new List<itAttribute>();
    List<itAttribute> uplist = new List<itAttribute>();

    private void Start()
    {
        if (Key != "" && costlist.Count == 0)
        {
            if (PlayerPrefs.GetInt(Key) != 0)
                gameObject.SetActive(false);
            else
                set(Key);
        }
    }

    public void offAll()
    {
        foreach(TextandImageScript i in Costs)
        {
            i.gameObject.SetActive(false);
        }
        foreach (TextandImageScript i in Ups)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void set(string key)
    {
        Key = key;
        img.sprite = Resources.Load<Sprite>(key);
        name.text = Singleton.Instance.getLocalItemName(key);
        offAll();
        costlist = Singleton.Instance.getItemCosts(key);

        for(int i = 0; i < costlist.Count; i ++)
        {
            setCost(i, Resources.Load<Sprite>(KeytoValue(costlist[i].key)), costlist[i].value.ToString());
        }

        uplist = Singleton.Instance.getItemUps(key);
        for (int i = 0; i < uplist.Count; i++)
        {
            setUp(i, Resources.Load<Sprite>(KeytoValue(uplist[i].key)), uplist[i].value.ToString());
        }
    }

    void setUp(int i, Sprite img, string txt)
    {
        Ups[i].gameObject.SetActive(true);
        Ups[i].set(img, txt);
    }
    void setCost(int i, Sprite img, string txt)
    {
        Costs[i].gameObject.SetActive(true);
        Costs[i].set(img, txt);
    }

    public string KeytoValue(string key)
    {
        if (key == "MaxHp" || key == "NeedHp")
            return "최대체력";
        else if (key == "Hp" || key == "CostHp")
            return "체력";
        else if (key == "Cost")
            return "돈";
        else if (key == "Pow" || key == "NeedPow")
            return "힘";
        else if (key == "Int" || key == "NeedInt")
            return "지능";
        else if (key == "Look" || key == "NeedLook")
            return "외모";

        return null;
    }

    public void Use(int index)
    {
        for (int i = 0; i < costlist.Count; i++)
        {
            if(PlayerPrefs.GetInt(KeytoValue(costlist[i].key)) < costlist[i].value)
            {
                return;
            }
        }
        for (int i = 0; i < costlist.Count; i++)
        {
            PlayerPrefs.SetInt(KeytoValue(costlist[i].key), PlayerPrefs.GetInt(KeytoValue(costlist[i].key)) - costlist[i].value);
        }

        for (int i = 0; i < uplist.Count; i++)
        {
            PlayerPrefs.SetInt(KeytoValue(uplist[i].key), PlayerPrefs.GetInt(KeytoValue(uplist[i].key)) + uplist[i].value);
        }
        Anim.GetComponent<UnityEngine.UI.Button>().enabled = false;
        Anim.SetInteger("상태", 1);
        PlayerPrefs.SetString("슬롯" + index.ToString(), "");
        Singleton.Instance.RollManager.use();
        Singleton.Instance.textRefresh();
    }

    public void getNeedInfo()
    {
        Singleton.Instance.NeedManager.set(this);
    }

    public void slideIn()
    {
        Anim.SetInteger("상태", 0);
    }
}
