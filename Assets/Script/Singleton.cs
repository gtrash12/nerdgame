using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/*
public class Item{
    public string 이름;
    public int 등급;
    public int 언락비용;
    public string 설명;
    public int 비용;
    public int 체력소모;
    public int 요구체력;
    public int 
    public int 체력회복;
    public int 최대체력;
    public int 힘;
    public int 지능;
    public int 외모;


}
*/

public class SkillData
{
    string key;
    string name;
    [TextArea]
    string info;
}

public class itAttribute{
    public string key;
    public int value;

    public itAttribute(string key, int value)
    {
        this.key = key;
        this.value = value;
    }
}
public class Singleton
{
    private static Singleton instance;
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }

    private Singleton()
    {

    }
    private int[] girlEXPBOUND = { 50, 50, 125, 250 , 750, 1500};
    public int hp;
    public List<string> common = new List<string>();
    public List<string> rare = new List<string>();
    public List<string> epic = new List<string>();
    public List<string> legend = new List<string>();
    private XmlDocument ItemXml = new XmlDocument();

    public RollScript RollManager;
    public AudioSource SFXSource;
    public NeedMonitorScript NeedManager;
    public GuageScript HpG;
    public ValuetoTextScript MoneyT;
    public ValuetoTextScript energyT;
    public ValuetoTextScript powT;
    public ValuetoTextScript intT;
    public ValuetoTextScript lookT;
    public ValuetoTextScript conT;
    public ValuetoTextScript pieT;
    public ValuetoTextScript artT;
    public CollectingEffectScript CollectingManager;

    public int Energy;

    public void init(RollScript roll, AudioSource SFXSource, NeedMonitorScript NeedManager, CollectingEffectScript CollectingManager
        , ValuetoTextScript MoneyT, GuageScript hpg, ValuetoTextScript energyt, ValuetoTextScript powt, ValuetoTextScript intt, ValuetoTextScript lookt, ValuetoTextScript cont
        , ValuetoTextScript piet, ValuetoTextScript artt)
    {
        RollManager = roll;
        this.SFXSource = SFXSource;
        this.NeedManager = NeedManager;
        this.MoneyT = MoneyT;
        this.CollectingManager = CollectingManager;
        HpG = hpg;
        energyT = energyt;
        powT = powt;
        intT = intt;
        lookT = lookt;
        conT = cont;
        pieT = piet;
        artT = artt;


        //xml로드
        ItemXml.LoadXml(Resources.Load<TextAsset>("아이템데이터").text);
        LoadItem();
    }

    void LoadItem()
    {
        XmlNodeList items = ItemXml.SelectNodes("Root/Item");
        foreach (XmlNode i in items)
        {
            string key = i.Attributes[0].InnerText;
            if (PlayerPrefs.GetInt(key) == 1)
            {
                if (i.SelectSingleNode("Rank").InnerText == "1")
                {
                    common.Add(key);
                }
                else if (i.SelectSingleNode("Rank").InnerText == "2")
                {
                    rare.Add(key);
                }
                else if (i.SelectSingleNode("Rank").InnerText == "3")
                {
                    epic.Add(key);
                }
                else if (i.SelectSingleNode("Rank").InnerText == "4")
                {
                    legend.Add(key);
                }
            }
        }
    }

    //*[self::c or self::d or self::e]  self::Pow or self::Int or self::Look or self::Con or self::Pie or self::Art
    public List<itAttribute> getItemUps(string key)
    {
        XmlNodeList lst = ItemXml.SelectNodes("Root/Item[@key='" + key + "']/*[self::MaxHp or self::Hp or self::Pow or self::Int or self::Look or self::Con or self::Pie or self::Art]");
        List<itAttribute> atts = new List<itAttribute>();
        foreach(XmlNode i in lst)
        {
            atts.Add(new itAttribute(i.Name, System.Convert.ToInt16(i.InnerText)));
        }
        return atts;
    }

    public List<itAttribute> getItemNeeds(string key)
    {
        XmlNodeList lst = ItemXml.SelectNodes("Root/Item[@key='" + key + "']/*[self::NeedHp or self::NeedPow or self::NeedInt or self::NeedLook or self::NeedCon or self::NeedPie or self::NeedArt]");
        List<itAttribute> atts = new List<itAttribute>();
        foreach (XmlNode i in lst)
        {
            atts.Add(new itAttribute(i.Name, System.Convert.ToInt16(i.InnerText)));
        }
        return atts;
    }

    public List<itAttribute> getItemCosts(string key)
    {
        XmlNodeList lst = ItemXml.SelectNodes("Root/Item[@key='" + key + "']/*[self::Cost or self::CostHp]");
        List<itAttribute> atts = new List<itAttribute>();
        foreach (XmlNode i in lst)
        {
            atts.Add(new itAttribute(i.Name, System.Convert.ToInt16(i.InnerText)));
        }
        return atts;
    }

    public string getLocalItemName(string key)
    {
        return ItemXml.SelectSingleNode("Root/Item[@key='" + key + "']/"+PlayerPrefs.GetString("Language")).InnerText;
    }

    public int getItemUnlockCost(string key)
    {
        return System.Convert.ToInt16(ItemXml.SelectSingleNode("Root/Item[@key='" + key + "']/CostUnlock").InnerText);
    }

    public void addItem(string key)
    {
        XmlNode item = ItemXml.SelectSingleNode("Root/Item[@key='"+key+"']");
        if (item.SelectSingleNode("Rank").InnerText == "1")
        {
            common.Add(key);
        }
        else if (item.SelectSingleNode("Rank").InnerText == "2")
        {
            rare.Add(key);
        }
        else if (item.SelectSingleNode("Rank").InnerText == "3")
        {
            epic.Add(key);
        }
        else if (item.SelectSingleNode("Rank").InnerText == "4")
        {
            legend.Add(key);
        }
        PlayerPrefs.SetInt(key, 1);
    }

    public void textRefresh()
    {
        MoneyT.txt.text = PlayerPrefs.GetInt("돈").ToString();
        energyT.txt.text = PlayerPrefs.GetInt("에너지").ToString() +" / 20";
        powT.txt.text = PlayerPrefs.GetInt("힘").ToString();
        intT.txt.text = PlayerPrefs.GetInt("지능").ToString();
        lookT.txt.text = PlayerPrefs.GetInt("외모").ToString();
        conT.txt.text = PlayerPrefs.GetInt("화술").ToString();
        pieT.txt.text = PlayerPrefs.GetInt("신앙심").ToString();
        artT.txt.text = PlayerPrefs.GetInt("예술").ToString();
        if (PlayerPrefs.GetInt("체력") > PlayerPrefs.GetInt("최대체력"))
            PlayerPrefs.SetInt("체력", PlayerPrefs.GetInt("최대체력"));
        HpG.Refresh();
    }

    public void Heal(int d)
    {
        int hp = PlayerPrefs.GetInt("체력");
        if (hp+d > PlayerPrefs.GetInt("최대체력"))
            PlayerPrefs.SetInt("체력", PlayerPrefs.GetInt("최대체력"));
        else
            PlayerPrefs.SetInt("체력", hp + d);
        HpG.Refresh();
    }

    public void EnergyGain(int d, bool bound = true)
    {
        int e = PlayerPrefs.GetInt("에너지");
        if (bound && e + d > 19)
            e = 20;
        else if (e + d < 0)
            e = 0;
        else
            e += d;
        PlayerPrefs.SetInt("에너지", e);
        energyT.txt.text = e.ToString() + " / 20";
        Energy = e;
    }

    public void PlaySFX(string clip)
    {
        SFXSource.PlayOneShot(Resources.Load<AudioClip>(clip));
    }

    public void StatGain(string k, int v)
    {
        int org = PlayerPrefs.GetInt(k);
        PlayerPrefs.SetInt(k, org + v);
        if (k == "체력" || k == "최대체력")
        {
            HpG.Refresh();
        }
        else
        {
            if (k == "힘")
            {
                CollectingManager.get(v, powT, Resources.Load<Sprite>(k));
            }
            else if (k == "지능")
            {
                CollectingManager.get(v, intT, Resources.Load<Sprite>(k));
            }
            else if (k == "외모")
            {
                CollectingManager.get(v, lookT, Resources.Load<Sprite>(k));
            }
            else if (k == "화술")
            {
                CollectingManager.get(v, conT, Resources.Load<Sprite>(k));
            }
            else if (k == "신앙심")
            {
                CollectingManager.get(v, pieT, Resources.Load<Sprite>(k));
            }
            else if (k == "예술")
            {
                CollectingManager.get(v, artT, Resources.Load<Sprite>(k));
            }
        }
    }


    public int GirlExpBound(int lv)
    {
        return girlEXPBOUND[lv];
    }
}
