using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPGuageScript : MonoBehaviour
{
    public BattleScript bts;
    public Animator Anim;
    public UnityEngine.UI.Image Guage;
    public UnityEngine.UI.Text HpText;
    Vector2 contV;
    public int currentHp;
    public int maxHp;
    public int curveHp;
    int state=1;
    // Start is called before the first frame update
    void Start()
    {
        Guage = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            currentHp += (int)(maxHp * Time.deltaTime);
            if(currentHp >= maxHp)
            {
                currentHp = maxHp;
                state = 1;
                //bts.Fight();
                bts.fightMotion.SetActive(true);
            }
        }
            HpText.text = currentHp.ToString();
            curveHp += Mathf.RoundToInt((currentHp - curveHp) / 2 * Time.deltaTime * 8);
            Guage.fillAmount = (float)curveHp / maxHp;
        
    }

    public void init(int hp)
    {
        state = 0;
        Anim.Play("체력바등장");
        curveHp = 0;
        maxHp = hp;
        currentHp = 0;
    }

    public void damage(int pow)
    {
        Anim.SetTrigger("흔들");
        currentHp -= pow;
        if(currentHp <= 0)
        {
            Anim.Play("체력바깨짐");
            bts.KnockOff();
        }
    }

    public void OFF()
    {
        Anim.Play("체력바깨짐");
        gameObject.SetActive(false);
    }
}
