using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWindowControlScript : MonoBehaviour
{
    RectTransform myRT;
    public RectTransform Relative;
    Vector2 nVector2;
    public Vector3 nVector3;
    // Start is called before the first frame update
    void Start()
    {
        myRT = GetComponent<RectTransform>();
        nVector2.x = myRT.sizeDelta.x;
        nVector3.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Relative.anchoredPosition.x > 5760)
        {
            nVector3.x = Relative.anchoredPosition.x - 5760;
        }
        else if (Relative.anchoredPosition.x < 0)
        {
            nVector3.x = Relative.anchoredPosition.x;
        }
        else
        {
            nVector3.x = 0;
        }
        nVector2.y =  -950 - Relative.position.y - Relative.anchoredPosition.y/2;
        myRT.sizeDelta = nVector2;
        myRT.anchoredPosition = nVector3;
    }
}
