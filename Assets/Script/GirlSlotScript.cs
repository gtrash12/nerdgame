using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlSlotScript : MonoBehaviour
{

    public string Name;
    [TextArea]
    public string SkillInfo;
    public UnityEngine.UI.Text Level;
    public UnityEngine.UI.Text Value;
    public UnityEngine.UI.Image LGuage;
    public UnityEngine.UI.Image SkillImg;

    public int lv;
    public bool ep;
    // Start is called before the first frame update
    void Start()
    {
        reFreshValue();
    }

    public void reFreshValue()
    {
        lv = PlayerPrefs.GetInt(Name + "레벨");
        int v = PlayerPrefs.GetInt(Name + "호감도");
        int b = Singleton.Instance.GirlExpBound(lv);
        if (lv == 5)
            SkillImg.color = Color.white;
        else
            SkillImg.color = Color.gray;
        if (lv == 6)
            Level.text = "MAX";
        else
            Level.text = lv.ToString();
        if (v >= b)
        {
            Value.text = "에피소드" + lv + "를 감상할 수 있습니다.";
            ep = true;
        }
        else
        {
            ep = false;
            Value.text = v + "/" + b;
        }
        LGuage.fillAmount = (float)v / b;
        //Value.text = 
    }

    public void Setskinfo(UnityEngine.UI.Text txt)
    {
        txt.text = SkillInfo;
    }
}
