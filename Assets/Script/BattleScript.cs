﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EUnit
{
    public string name { get; set; }
    public int hp { get; set; }
    public int power { get; set; }
    public float spd { get; set; }
    public int reward { get; set; }
    public string Idle { get; set; }
    public string Hit1 { get; set; }
    public string Hit2 { get; set; }
    public string Hit3 { get; set; }
    public string Attack { get; set; }
    public string next { get; set; }

    public EUnit()
    {
        next = "";
    }

    public EUnit(string name, int hp, int power, float spd, int reward, string Idle, string Hit1, string Hit2, string Hit3, string Attack, string next)
    {
        this.name = name;
        this.hp = hp;
        this.power = power;
        this.spd = spd;
        this.reward = reward;
        this.Idle = Idle;
        this.Hit1 = Hit1;
        this.Hit2 = Hit2;
        this.Hit3 = Hit3;
        this.Attack = Attack;
        this.next = next;
    }


}

public class BattleScript : MonoBehaviour
{
    public GuageScript PlayerHp;
    public ParticleSystem HitEff;
    public TextAsset EnemyXmlData;
    List<EUnit> EnemList = new List<EUnit>();
    public EnemyHPGuageScript HPGuage;
    public UnityEngine.UI.Image Enem;
    public ScriptEvent Evs;
    public Animator EnemAnim;
    public Animator Punch;
    public Sprite Idle;
    public Sprite Hit1;
    public Sprite Hit2;
    public Sprite Hit3;
    public GameObject atkbtn;
    int prevrand;
    private XmlDocument EnemyXml = new XmlDocument();
    private XmlNodeList NodeList;
    private int enemIndex = 0;
    public GameObject fightMotion;
    public CollectingEffectScript collectingEff;

    public int power;
    // Start is called before the first frame update
    void Start()
    {
        SetData();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData()
    {
        EnemyXml.LoadXml(EnemyXmlData.text);
    }

    public void Hit()
    {
        int rand = Random.Range(0, 3);
        if (rand == prevrand)
            rand = (rand + 1) % 3;
        prevrand = rand;
        if (rand == 0)
        {
            Enem.sprite = Hit1;
            Punch.gameObject.SetActive(true);
            EnemAnim.Play("피격1");
            Punch.Play("펀치1");
        }
        else if(rand == 1)
        {
            Enem.sprite = Hit2;
            Punch.gameObject.SetActive(true);
            EnemAnim.Play("피격2");
            Punch.Play("펀치2");
        }
        else
        {
            Enem.sprite = Hit3;
            Punch.gameObject.SetActive(true);
            EnemAnim.Play("피격3");
            Punch.Play("펀치3");
        }
        HitEff.transform.position = new Vector2((Input.mousePosition.x - 720) *0.0014f,
            (Input.mousePosition.y - HitEff.transform.parent.GetComponent<RectTransform>().sizeDelta.y / 2) / HitEff.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 10);
        HitEff.Play();
        HPGuage.damage(power);
    }

    public void SetEnemyList(string key)
    {
        enemIndex = 0;
        NodeList = EnemyXml.SelectNodes("Root/Unit[@key='" + key + "']");
        int total = NodeList.Count;
        for (int i = 0; i < total; i++)
        {
            EUnit newEU = new EUnit();
            newEU.name = NodeList.Item(i).SelectSingleNode(PlayerPrefs.GetString("Language")).InnerText;
            newEU.hp = System.Convert.ToInt16(NodeList.Item(i).SelectSingleNode("Hp").InnerText);
            newEU.power = System.Convert.ToInt16(NodeList.Item(i).SelectSingleNode("Power").InnerText);
            newEU.spd = (float)System.Convert.ToDouble(NodeList.Item(i).SelectSingleNode("Spd").InnerText);
            newEU.reward = System.Convert.ToInt16(NodeList.Item(i).SelectSingleNode("Reword").InnerText);
            newEU.Idle = NodeList.Item(i).SelectSingleNode("Idle").InnerText;
            newEU.Hit1 = NodeList.Item(i).SelectSingleNode("Hit1").InnerText;
            newEU.Hit2 = NodeList.Item(i).SelectSingleNode("Hit2").InnerText;
            newEU.Hit3 = NodeList.Item(i).SelectSingleNode("Hit3").InnerText;
            newEU.Attack = NodeList.Item(i).SelectSingleNode("Attack").InnerText;
            if (NodeList.Item(i).SelectSingleNode("Next") != null)
            {
                newEU.next = NodeList.Item(i).SelectSingleNode("Next").InnerText;
            }
            EnemList.Add(newEU);
        }
    }

    public void AddEnem(string name, int hp, int power, float spd, int reward, string Idle, string Hit1, string Hit2, string Hit3, string Attack, string next = "")
    {
        EnemList.Add(new EUnit(name,hp,power,spd,reward,Idle,Hit1,Hit2,Hit3,Attack,next));
    }

    public void ClearEnem()
    {
        EnemList.Clear();
    }

    public void Ready()
    {
        Evs.UnderBar.GetComponent<Animator>().SetInteger("언더바상태", 1);
        HPGuage.gameObject.SetActive(true);
        HPGuage.init(EnemList[enemIndex].hp);
        HPGuage.nametxt.text = EnemList[enemIndex].name;
        Idle = Resources.Load<Sprite>(EnemList[enemIndex].Idle);
        Hit1 = Resources.Load<Sprite>(EnemList[enemIndex].Hit1);
        Hit2 = Resources.Load<Sprite>(EnemList[enemIndex].Hit2);
        Hit3 = Resources.Load<Sprite>(EnemList[enemIndex].Hit3);
        Enem.sprite = Idle;
        EnemAnim.Play("바운스");
        Enem.enabled = true;
        enemIndex++;
        
        power = PlayerPrefs.GetInt("힘");
    }

    public void Fight()
    {
        atkbtn.SetActive(true);
        Invoke("EnemAttack", EnemList[enemIndex-1].spd);
    }

    public void KnockOff()
    {
        CancelInvoke();
        atkbtn.SetActive(false);
        int totalreward = EnemList[enemIndex - 1].reward;
        int effnum = EnemList[enemIndex - 1].reward / 10;
        Debug.Log(effnum);
        PlayerPrefs.SetInt("돈", PlayerPrefs.GetInt("돈")+ totalreward);
        collectingEff.get(10, 1, effnum);
        collectingEff.get(totalreward % 10, 1, 1);
        Time.timeScale = 0.03f;
        Invoke("시간제위치", 0.06f);
        Evs.SFXSource.PlayOneShot(Resources.Load<AudioClip>("칭"));
        Evs.SFXSource.GetComponent<AudioReverbFilter>().enabled = true;
        Evs.SFXSource.GetComponent<AudioEchoFilter>().enabled = true;
    }

    void 시간제위치()
    {
        fightMotion.SetActive(true);
        fightMotion.GetComponent<Animator>().SetTrigger("넉오프");
        EnemAnim.Play("쓰러짐");
        Evs.Effect.GetComponent<BrightnessControlScript>().Flash();
        Time.timeScale = 1f;
        Evs.SFXSource.GetComponent<AudioReverbFilter>().enabled = false;
        Evs.SFXSource.GetComponent<AudioEchoFilter>().enabled = false;
    }

    public void NextEvent()
    {
        if (Evs.getEvent(EnemList[enemIndex - 1].next))
        {
            Evs.ScrollPannel.WindowUp();
            Evs.UnderBar.UnderbarDown();
            Evs.Invoke("getText", 0);
            Evs.UnderBar.GetComponent<Animator>().SetInteger("언더바상태", 0);
        }
        else
        {
            Ready();
        }
    }

    void EnemAttack()
    {
        Evs.SFXSource.PlayOneShot(Resources.Load<AudioClip>("푸식"));
        BrightnessControlScript src = Evs.Effect.GetComponent<BrightnessControlScript>();
        src.enabled = true;
        src.Flash();

        ShakeScript src2 = Evs.Effect.GetComponent<ShakeScript>();
        src2.enabled = true;
        src2.ShortShake();

        PlayerHp.damage(EnemList[enemIndex - 1].power);
        Invoke("EnemAttack", EnemList[enemIndex - 1].spd);
    }

    public int episodecount(string key)
    {
        int count = 1;
        while (EnemyXml.SelectNodes("Root/Unit[@key='" + key + count + "']").Count >0)
        {
            count++;
        }
        return count -1;
    }
}
