using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISizeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform y = GetComponent<RectTransform>();
        y.sizeDelta = new Vector2(y.sizeDelta.x, Screen.height - 350);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
