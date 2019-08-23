using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedMonitorScript : MonoBehaviour
{
    public List<ValuetoTextScript> Needs = new List<ValuetoTextScript>();
    public ActivityScript selectedItem;
    int cost;

    public void set(ActivityScript item)
    {
        selectedItem = item;
        
        bool deserve = true; 
        offAll();
        List<itAttribute> needlist = Singleton.Instance.getItemNeeds(selectedItem.Key);

        for (int i = 0; i < needlist.Count; i++)
        {
            int index = KeytoIndex(needlist[i].key);
            Needs[index].gameObject.SetActive(true);
            Needs[index].set(needlist[i].value);
            if (PlayerPrefs.GetInt(KeytoValue(needlist[i].key)) < needlist[i].value)
                deserve = false;
            //setCost(i, Resources.Load<Sprite>(KeytoValue(costlist[i].key)), costlist[i].value.ToString());
        }
        if (deserve == true)
        {
            cost = Singleton.Instance.getItemUnlockCost(selectedItem.Key);
            Needs[7].set(cost);
            Needs[7].gameObject.SetActive(true);
        }
        
    }

    void offAll()
    {
        foreach (ValuetoTextScript i in Needs)
        {
            i.gameObject.SetActive(false);
        }
    }

   public void Buy()
    {
        if (PlayerPrefs.GetInt("돈") < cost)
        {
            Singleton.Instance.PlaySFX("오류");
            return;
        }
        PlayerPrefs.SetInt("돈", PlayerPrefs.GetInt("돈") - cost);
        Singleton.Instance.addItem(selectedItem.Key);
        Singleton.Instance.textRefresh();
        selectedItem.gameObject.SetActive(false);
    }




    int KeytoIndex(string key)
    {
        if (key == "NeedHp")
            return 0;
        if (key == "NeedPow")
            return 1;
        if (key == "NeedInt")
            return 2;
        if (key == "NeedLook")
            return 3;
        if (key == "NeedCon")
            return 4;
        if (key == "NeedPie")
            return 5;
        if (key == "NeedArt")
            return 6;
        return -1;
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
}
