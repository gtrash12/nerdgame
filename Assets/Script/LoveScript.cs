using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Favrite
{
    public string stat;
    public int value;
    public int percent;
}
[System.Serializable]
public class GirlData
{
    public string name;
    public Favrite[] fav;
}

public class LoveScript : MonoBehaviour
{
    public GuageScript guage;
    public UnityEngine.UI.Text T_Lv;
    public GirlSlotScript gl;
    public UnityEngine.UI.Image girlimg;
    public GirlData[] girls;
    GirlData current;
    int love;
    int lv;
    int index;

    public UIScrollScript underbar;
    public UIScrollScript sc;
    public GirlSlotManagerScript gm;
    public UnityEngine.UI.Text plusT;
    public Animator plus;

    public void meet()
    {
        index = Random.Range(0, girls.Length - 1);
        current = girls[index];
        love = PlayerPrefs.GetInt(current.name + "호감도");
        lv = PlayerPrefs.GetInt(current.name + "레벨");
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Rebind();
        
        girlimg.sprite = Resources.Load<Sprite>(current.name+"도트");
        guage.maxValue = Singleton.Instance.GirlExpBound(lv);
        guage.currentValue = love;
        T_Lv.text = lv.ToString();
        
        if (Dice() == true)
        {
            PlayerPrefs.SetInt(current.name + "호감도", love + cal(current));
            Invoke("Success", GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            Invoke("Fail", GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void 소리(string n)
    {
        Singleton.Instance.PlaySFX(n);
    }

    public void Success()
    {
        guage.currentValue = PlayerPrefs.GetInt(current.name + "호감도");
        Singleton.Instance.PlaySFX("성공");
        girlimg.sprite = Resources.Load<Sprite>(current.name+"도트-성공");
        gm.cont[index].reFreshValue();

        plusT.text = "+" + (guage.currentValue-love).ToString();
        plus.Rebind();

        Invoke("back", 2);
    }

    public void Fail()
    {
        Singleton.Instance.PlaySFX("실패");
        girlimg.sprite = Resources.Load<Sprite>(current.name + "도트-실패");
        Invoke("back", 2);
    }

    public void back()
    {
        underbar.UnderbarUp();
        sc.pivot.x = 2880;
    }

    public bool Dice()
    {
        float r = Random.Range(0f, 1f);
        Debug.Log(r);
        if (r <= 0.5f)
            return true;
        else
            return false;
    }

    int cal(GirlData target)
    {
        int result = 1;
        foreach(Favrite i in target.fav)
        {
            int v = PlayerPrefs.GetInt(i.stat);
            Debug.Log(i.stat + v);
            if (v > i.value)
                result += i.percent;
            else
                result += (int)(v/(float)i.value*i.percent);
        }
        return result;
    }

    GirlData findGirl(string n)
    {
        foreach(GirlData i in girls)
        {
            if (i.name == n)
                return i;
        }
        return null;
    }
}
