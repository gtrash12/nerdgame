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
        money.set(PlayerPrefs.GetInt("Money"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void get(int value, int target,int num)
    {
        MoneyGainScript one;
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
            one.gameObject.SetActive(true);
        }
    }
}
