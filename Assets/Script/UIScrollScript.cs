﻿using System.Collections;
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
    public void changePivot(int x)
    {
        pivot.x = x;
    }
    public void SetDefault()
    {
        pivot.y = 0;
        pivot.x = 2880;
    }
    public void UnderbarDown()
    {
        pivot.y = -200;
    }
    public void UnderbarUp()
    {
        pivot.y = 0;
    }
    public void WindowDown()
    {
        pivot.y = -1000;
    }
    public void WindowUp()
    {
        pivot.y = 0;
    }

    public float distance()
    {
        return Vector3.Distance(pivot, scrollBox.anchoredPosition);
    }
}
