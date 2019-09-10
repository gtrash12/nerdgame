using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWindowControlScript : MonoBehaviour
{
    RectTransform myRT;
    public RectTransform Relative;
    Vector2 nVector2;
    public Vector3 nVector3;
    public float offset_x;
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
            nVector3.x = Relative.anchoredPosition.x - 5760 + offset_x;
        }
        else if (Relative.anchoredPosition.x < offset_x)
        {
            nVector3.x = Relative.anchoredPosition.x + offset_x - 1440;
        }
        else
        {
            nVector3.x = offset_x;
        }
        //nVector2.y =  -950 - Relative.position.y - Relative.anchoredPosition.y/2;
        nVector2.y = 20-Relative.position.y - Relative.sizeDelta.y;
        //Debug.Log(Relative.position.y + "\n" + Relative.anchoredPosition.y);
        //Debug.Log(nVector2);
        myRT.sizeDelta = nVector2;
        myRT.anchoredPosition = nVector3;
    }
}
