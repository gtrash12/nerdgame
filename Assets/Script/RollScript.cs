using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : MonoBehaviour
{
    public List<ActivityScript> slots = new List<ActivityScript>();
    int slotNumber = 5;
    public int rollAVN = 5;
    public int rerollCost = 1;
    public int usedSlot = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        dataload();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void dataload()
    {
        for (int i = 0; i < 5; i++)
        {
            string k = PlayerPrefs.GetString("슬롯" + i.ToString());
            if (k == "") {
                slots[i].Anim.SetInteger("상태", 1);
                usedSlot++;
                continue;
            }
            slots[i].img.GetComponent<UnityEngine.UI.Button>().enabled = true;
            slots[i].set(k);
            slots[i].Anim.Play("슬롯_오른쪽");
            slots[i].Anim.SetInteger("상태", 0);
        }
        if (usedSlot == 5)
            roll();
    }

    public void reroll()
    {
        if (PlayerPrefs.GetInt("에너지") < rerollCost)
            return;
        PlayerPrefs.SetInt("에너지", PlayerPrefs.GetInt("에너지") - 1);
        roll();
        Singleton.Instance.textRefresh();
    }

    public void roll()
    {
        usedSlot = 0;
        for (int i = 0; i < 5; i++)
        {
            List<string> item = dice();
            int r = Random.Range(0, item.Count);
            PlayerPrefs.SetString("슬롯" + i.ToString(), item[r]);
            slots[i].set(item[r]);
            slots[i].Anim.Play("슬롯_오른쪽");
            slots[i].Anim.SetInteger("상태", 1);
            slots[i].Invoke("slideIn", 0.2f*i);
            slots[i].img.GetComponent<UnityEngine.UI.Button>().enabled = true;
        }

    }

    List<string> dice()
    {
        int rvalue = Random.Range(0, 1);
        if (rvalue <= 0.6f)
        {
            return Singleton.Instance.common;
        } else if (rvalue <= 0.9f)
        {
            if (Singleton.Instance.rare.Count == 0)
                return Singleton.Instance.common;
            return Singleton.Instance.rare;
        }
        else if (rvalue <= 0.99f)
        {
            if (Singleton.Instance.epic.Count == 0)
                return Singleton.Instance.common;
            return Singleton.Instance.epic;
        }
        else
        {
            if (Singleton.Instance.legend.Count == 0)
                return Singleton.Instance.common;
            return Singleton.Instance.legend;
        }
    }

    public void use()
    {
        usedSlot++;
        if (usedSlot >= rollAVN)
        {
            roll();
        }
    }
}
