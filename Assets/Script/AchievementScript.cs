using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string name;
    public string name_E;
    public string name_J;
    public string key;
    public int goal;
    public int reward;
    [TextArea]
    public string info;
    [TextArea]
    public string info_E;
    [TextArea]
    public string info_J;
}

public class AchievementScript : MonoBehaviour
{
    
    public UnityEngine.UI.Text t_name;
    public UnityEngine.UI.Text t_info;
    public UnityEngine.UI.Text t_reward;
    public UnityEngine.UI.Button rewardbtn;
    public string key;
    public Achievement[] Contents;


    // Start is called before the first frame update
    void Start()
    {
        set();
    }

    public void set()
    {
        bool last = false;
        int n = PlayerPrefs.GetInt(key,0);
        if(n >= Contents.Length)
        {
            n--;
            last = true;
        }
        int p = (int)(PlayerPrefs.GetFloat(Contents[n].key)/ Contents[n].goal*100);
        t_name.text = Contents[n].name;
        t_info.text = Contents[n].info + "\n<color=#F29661>("+p+"%)</color>";
        t_reward.text = "x" + Contents[n].reward;
        if (last == false && p >= 100)
            rewardbtn.enabled = true;
        else
            rewardbtn.enabled = false;
    }


    public void getReward()
    {
        
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
        set();
    }


}
