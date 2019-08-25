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

        Level.text = lv.ToString();
        Value.text = v + "/" + b;
        LGuage.fillAmount = v / b;
        //Value.text = 
    }

    public void Setskinfo(UnityEngine.UI.Text txt)
    {
        txt.text = SkillInfo;
    }
}
