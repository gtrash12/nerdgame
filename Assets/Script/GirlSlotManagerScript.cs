using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlSlotManagerScript : MonoBehaviour
{
    public List<GirlSlotScript> cont = new List<GirlSlotScript>();
    
    // Start is called before the first frame update
    void Start()
    {
        cont.AddRange(GetComponentsInChildren<GirlSlotScript>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
