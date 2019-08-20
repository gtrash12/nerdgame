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
    public UnityEngine.UI.Text energyT;
    public UnityEngine.UI.Text powT;
    public UnityEngine.UI.Text intT;
    public UnityEngine.UI.Text lookT;
    public UnityEngine.UI.Text conT;
    public UnityEngine.UI.Text pieT;
    public UnityEngine.UI.Text artT;

    public void init(RollScript roll, AudioSource SFXSource, NeedMonitorScript NeedManager, GuageScript hpg, UnityEngine.UI.Text energyt, UnityEngine.UI.Text powt, UnityEngine.UI.Text intt, UnityEngine.UI.Text lookt, UnityEngine.UI.Text cont, UnityEngine.UI.Text piet, UnityEngine.UI.Text artt)
    {
        RollManager = roll;
        this.SFXSource = SFXSource;
        this.NeedManager = NeedManager;
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


        //능력치
        PlayerPrefs.GetInt("체력", 100);
        PlayerPrefs.GetInt("최대체력", 100);
        PlayerPrefs.GetInt("힘", 10);
        PlayerPrefs.GetInt("돈", 0);
        PlayerPrefs.GetInt("에너지", 20);
        PlayerPrefs.GetInt("지능", 0);
        PlayerPrefs.GetInt("외모", 0);
        PlayerPrefs.GetInt("화술", 0);
        PlayerPrefs.GetInt("신앙심", 0);
        PlayerPrefs.GetInt("예술", 0);

        // 행동
        PlayerPrefs.GetInt("조깅", 1);
        PlayerPrefs.GetInt("삼각김밥", 1);
        PlayerPrefs.GetInt("자습", 0);
        PlayerPrefs.GetInt("팔굽혀펴기", 0);
        PlayerPrefs.GetInt("얼굴마사지", 0);

        //슬롯
        PlayerPrefs.GetString("슬롯0", "");
        PlayerPrefs.GetString("슬롯1", "");
        PlayerPrefs.GetString("슬롯2", "");
        PlayerPrefs.GetString("슬롯3", "");
        PlayerPrefs.GetString("슬롯4", "");
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
            Debug.Log(i.Name);
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
            Debug.Log(i.Name);
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
            Debug.Log(i.Name);
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
    }

    public void textRefresh()
    {
        energyT.text = PlayerPrefs.GetInt("에너지").ToString() +" / 20";
        powT.text = PlayerPrefs.GetInt("힘").ToString();
        intT.text = PlayerPrefs.GetInt("지능").ToString();
        lookT.text = PlayerPrefs.GetInt("외모").ToString();
        conT.text = PlayerPrefs.GetInt("화술").ToString();
        pieT.text = PlayerPrefs.GetInt("신앙심").ToString();
        artT.text = PlayerPrefs.GetInt("예술").ToString();
        HpG.Refresh();
    }
}
