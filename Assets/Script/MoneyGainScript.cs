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
    int rotatepower;
    // Start is called before the first frame update
    void Awake()
    {
        scroll = GetComponent<UIScrollScript>();
        height = transform.parent.parent.GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(scroll.pivot.normalized*Time.deltaTime*rotatepower* scroll.distance());
        if (scroll.distance() <= 5)
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
        rotatepower = 10;
        scroll.enabled = true;
        scroll.pivot = new Vector3(Random.Range(200, 1240), Random.Range(-height/4, -height/4*2), 0);
        scroll.transform.position = new Vector3((scroll.pivot.x-720)*0.2f + 720 , (scroll.pivot.y - height/2) * 0.2f + height/2 , 0);
        next = "get";
    }

    void get()
    {
        rotatepower = 0;
        scroll.pivot = new Vector3(100,-100, 0);
        next = "OFF";
    }

    void OFF()
    {
        target.Add(value);
        gameObject.SetActive(false);
    }
}
