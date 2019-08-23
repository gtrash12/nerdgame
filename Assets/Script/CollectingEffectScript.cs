using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingEffectScript : MonoBehaviour
{
    List<MoneyGainScript> vlist = new List<MoneyGainScript>();
    public ValuetoTextScript money;
    public ValuetoTextScript energy;
    // Start is called before the first frame update
    void Start()
    {
        vlist.AddRange(transform.GetComponentsInChildren<MoneyGainScript>());
        foreach(MoneyGainScript i in vlist)
        {
            i.gameObject.SetActive(false);
        }
        money.set(PlayerPrefs.GetInt("돈"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void get(int value, int target,int num)
    {
        MoneyGainScript one;
        Sprite img = Resources.Load<Sprite>("돈");
        for(int i = 0; i < num; i++)
        {
            one = vlist[0];
            one.value = value;
            if (target == 1)
            {
                one.target = money;
            }
            else
            {
                one.target = energy;
            }
            vlist.Remove(one);
            vlist.Add(one);
            one.GetComponent<UnityEngine.UI.Image>().sprite = img;
            one.gameObject.SetActive(true);
            one.spread();
        }
    }

    public void get(int value, ValuetoTextScript target, Sprite img)
    {
        if (value > 0)
        {
            int num;
            int x = 1 + value / 50;
            int d = value % 50;
            if (x == 1)
                num = d;
            else
                num = 50;
            MoneyGainScript one;
            for (int i = 0; i < num; i++)
            {
                one = vlist[0];
                if (i < d)
                    one.value = x;
                else
                    one.value = x - 1;
                one.target = target;
                vlist.Remove(one);
                vlist.Add(one);

                one.GetComponent<UnityEngine.UI.Image>().sprite = img;
                one.gameObject.SetActive(true);
                one.get();
            }
        }
        else
        {
            target.value += value;
            MoneyGainScript one;
            int num;
            if (value < 50)
                num = -value;
            else
                num = 50;
            for (int i = 0; i < num; i++)
            {
                one = vlist[0];
                one.value = 0;
                one.target = target;
                vlist.Remove(one);
                vlist.Add(one);

                one.GetComponent<UnityEngine.UI.Image>().sprite = img;
                one.gameObject.SetActive(true);
                one.Invoke("minus",0.05f*i);
            }
        }
    }
}
