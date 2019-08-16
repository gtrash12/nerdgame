using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAnimationScript : MonoBehaviour
{
    public BattleScript bts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void fight()
    {
        bts.Fight();
        //gameObject.SetActive(false);
    }

    void offtive()
    {
        bts.NextEvent();
        //gameObject.SetActive(false);
    }

    void 둥()
    {
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("케이오"));
    }
    void 칭()
    {
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("파이트"));
    }
}
