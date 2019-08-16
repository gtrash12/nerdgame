using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGainScript : MonoBehaviour
{
    UIScrollScript scroll;
    float height;
    string next;
    public int value;
    public ValuetoTextScript target;
    // Start is called before the first frame update
    void Awake()
    {
        scroll = GetComponent<UIScrollScript>();
        height = transform.parent.parent.GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(scroll.distanceCheck() == true)
        {
            Invoke(next,0);
        }
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 360)));
        spread();   
    }

    void spread()
    {
        scroll.transform.position = new Vector3(720, height / 2, 0);
        scroll.enabled = true;
        scroll.pivot = new Vector3(Random.Range(200, 1240), Random.Range(-height/4, -height/4*2), 0);
        next = "get";
    }

    void get()
    {
        scroll.pivot = new Vector3(100,-100, 0);
        next = "OFF";
    }

    void OFF()
    {
        target.Add(value);
        gameObject.SetActive(false);
    }
}
