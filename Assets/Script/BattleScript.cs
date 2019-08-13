using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    public UnityEngine.UI.Image Enem;
    public Animator EnemAnim;
    public Animator Punch;
    public Sprite Idle;
    public Sprite Hit1;
    public Sprite Hit2;
    public Sprite Hit3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        int rand = Random.Range(0, 3);
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
    }
}
