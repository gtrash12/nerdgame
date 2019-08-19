using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : MonoBehaviour
{
    public List<ActivityScript> slots = new List<ActivityScript>();
    int slotNumber = 5;

    

    // Start is called before the first frame update
    void Start()
    {
        roll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void roll()
    {
        for (int i = 0; i < 5; i++)
        {
            List<string> item = dice();
            int r = Random.Range(0, item.Count);
            slots[i].set(item[r]);
            slots[i].img.Play("슬롯_오른쪽");
            slots[i].img.SetInteger("상태", 0);
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
}
