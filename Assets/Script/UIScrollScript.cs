using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScrollScript : MonoBehaviour
{
    public Vector3 pivot;
    private RectTransform scrollBox;
    // Start is called before the first frame update
    void Start()
    {
        scrollBox = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        scrollBox.anchoredPosition = Vector3.Lerp(scrollBox.anchoredPosition, pivot, Time.deltaTime * 8);
    }

    public void changePivot(RectTransform t)
    {
        pivot = t.localPosition;
        pivot.x *= -1;
    }
    public void UnderbarDown()
    {
        pivot.y = -200;
    }
    public void UnderbarUp()
    {
        pivot.y = 200;
    }
}
