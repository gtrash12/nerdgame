using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class ScriptEvent : MonoBehaviour
{
    public TextAsset ScriptXml;
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
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<UnityEngine.UI.Button>();
        PlayerPrefs.SetString("Language", "Korean");
        SetDB();
        getEvent("후배1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDB()
    {
        LocalizeFile.LoadXml(ScriptXml.text);
    }

    public void getEvent(string key)
    {
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
            }
            else
            {
                TextBox.text = "";
            }
            TextBox.text += currentNode.SelectSingleNode(PlayerPrefs.GetString("Language")).InnerText;
        }
        else
        {
            bool auto = true;
            int type = System.Convert.ToInt16(currentNode.SelectSingleNode("Type").InnerText);
            if(type == 2)
            {
                Cha.enabled = true;
                Cha.sprite = Resources.Load<Sprite>(currentNode.SelectSingleNode("Korean").InnerText) as Sprite;
                Cha.RecalculateClipping();
                //Cha.Rebuild(UnityEngine.UI.CanvasUpdate.Layout);
            }else if(type == 3)
            {
                SFXSource.PlayOneShot(Resources.Load<AudioClip>(currentNode.SelectSingleNode("Korean").InnerText));
            }else if(type == 4)
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
        else if(ef == "바운스" || ef == "흔들" || ef == "갸우뚱")
        {
            ChaAnim.SetTrigger(ef);
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
            PunchBtn.SetActive(true);
            UnderBar.UnderbarUp();
        }
        return auto;
    }
}
