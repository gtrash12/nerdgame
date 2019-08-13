using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWindowControlScript : MonoBehaviour
{
    RectTransform myRT;
    public RectTransform Relative;
    // Start is called before the first frame update
    void Start()
    {
        myRT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRT.sizeDelta = new Vector2(myRT.sizeDelta.x, -1000-Relative.anchoredPosition.y);
        Debug.Log(myRT.sizeDelta);
    }
}
