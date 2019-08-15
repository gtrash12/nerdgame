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
    
    void oftive()
    {
        bts.Fight();
        gameObject.SetActive(false);
    }
}
