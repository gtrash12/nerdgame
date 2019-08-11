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
        pivot = Vector3.zero;
        pivot.y = 200;
    }
    // Update is called once per frame
    void Update()
    {
        scrollBox.position = Vector3.Lerp(scrollBox.position, pivot, Time.deltaTime * 8);
    }

    public void changePivot(RectTransform t)
    {
        pivot = t.localPosition;
        pivot.x *= -1;
        pivot.y = 200;
    }
}
