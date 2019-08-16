using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class ScriptEvent : MonoBehaviour
{
    public TextAsset ScriptXml;
    public BattleScript battlescript;
    public UnityEngine.UI.Text TextBox;
    public UnityEngine.UI.Image Cha;
    public UnityEngine.UI.Image BackGroundImage;
    public GameObject Effect;
    public Animator ChaAnim;
    public AudioSource SFXSource;
    public AudioSource BGMSource;
    public UIScrollScript ScrollPannel;
    public UIScrollScript UnderBar;
    public GameObject PunchBtn;
    private XmlDocument LocalizeFile = new XmlDocument();
    private XmlNodeList EventNodes;
    private XmlNode currentNode;
    private UnityEngine.UI.Button btn;
    private int i;
    string currentkey;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<UnityEngine.UI.Button>();
        PlayerPrefs.SetString("Language", "Korean");
        SetData();
        getEvent("후배1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData()
    {
        LocalizeFile.LoadXml(ScriptXml.text);
    }

    public void getEvent(string key)
    {
        currentkey = key;
        EventNodes = LocalizeFile.SelectNodes("Root/Unit[@key='" + key + "']");
        i = 0;
    }

    public void getText()
    {
        if (i >= EventNodes.Count)
            return;
        btn.enabled = true;
        currentNode = EventNodes.Item(i);
        i++;
        if (currentNode.SelectSingleNode("Type") == null) {
            if(currentNode.SelectSingleNode("Name") != null)
            {
                TextBox.text = "<color=#6799FF>[" + currentNode.SelectSingleNode("Name").InnerText + "]</color>\n";
                TextBox.text += currentNode.SelectSingleNode(PlayerPrefs.GetString("Language")).InnerText;
            }
            else
            {
                TextBox.text = "<color=#6799FF>" + currentNode.SelectSingleNode(PlayerPrefs.GetString("Language")).InnerText + "</color>" ;

            }
           


        }
        else
        {
            TextBox.text = "";
            bool auto = true;
            int type = System.Convert.ToInt16(currentNode.SelectSingleNode("Type").InnerText);
            if(type == 2)
            {
                Cha.enabled = true;
                Cha.sprite = Resources.Load<Sprite>(currentNode.SelectSingleNode("Korean").InnerText) as Sprite;
                ChaAnim.Play("Idle");
            }else if(type == 3)
            {

                ChaAnim.SetTrigger(currentNode.SelectSingleNode("Korean").InnerText);
            }
            else if(type == 4)
            {
                auto = ShortEffect(currentNode.SelectSingleNode("Korean").InnerText);
            }
            else if (type == 5)
            {
                string src = currentNode.SelectSingleNode("Korean").InnerText;
                if (src == "종료")
                    BGMSource.Stop();
                else
                {
                    BGMSource.clip = Resources.Load<AudioClip>(src);
                    BGMSource.Play();
                }

            }
            else if(type == 6)
            {
                SFXSource.PlayOneShot(Resources.Load<AudioClip>(currentNode.SelectSingleNode("Korean").InnerText));
            }
            if (auto == true)
            {
                getText();
            }
        }
    }

    bool ShortEffect(string ef)
    {
        bool auto = true;
        if (ef == "흔들림")
        {
            ShakeScript src = Effect.GetComponent<ShakeScript>();
            src.enabled = true;
            src.ShortShake();
            //src.Invoke("OFF", 1000f);
        }
        else if(ef == "주기적흔들림")
        {
            ShakeScript src = Effect.GetComponent<ShakeScript>();
            src.enabled = true;
            src.PeriodicShake();
        }
        else if(ef == "플래시")
        {
            BrightnessControlScript src = Effect.GetComponent<BrightnessControlScript>();
            src.enabled = true;
            src.Flash();
        }
        else if(ef == "이미지오프")
        {
            Cha.enabled = false;
        }
        else if(ef == "플래시페이드아웃")
        {
            BrightnessControlScript src = Effect.GetComponent<BrightnessControlScript>();
            src.enabled = true;
            src.FlashFadeOut();
        }
        else if (ef == "플래시페이드인")
        {
            BrightnessControlScript src = Effect.GetComponent<BrightnessControlScript>();
            src.enabled = true;
            src.FlashFadeIn();
        }
        else if (ef == "페이드전환")
        {
            btn.enabled = false;
            BrightnessControlScript src = Effect.GetComponent<BrightnessControlScript>();
            src.enabled = true;
            src.FadeSwitch();
            auto = false;
            Invoke("getText", 4f);
        }
        else if(ef == "페이드아웃")
        {
            btn.enabled = false;
            BrightnessControlScript src = Effect.GetComponent<BrightnessControlScript>();
            src.enabled = true;
            src.FadeOut();
            auto = false;
            Invoke("getText", 2f);
        }
        else if(ef == "흔들림종료")
        {
            ShakeScript src = Effect.GetComponent<ShakeScript>();
            src.enabled = true;
            src.OFF();
        }else if(ef == "싸움시작")
        {
            ScrollPannel.WindowDown();
            UnderBar.UnderbarUp();
            battlescript.Ready();
        }
        else if(ef == "싸움세팅")
        {
            battlescript.SetEnemyList(currentkey);
        }
        return auto;
    }

    public void Skip()
    {
        i = EventNodes.Count - 2;
        getText();
    }
}
