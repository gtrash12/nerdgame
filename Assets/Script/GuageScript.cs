using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuageScript : MonoBehaviour
{
    public UnityEngine.UI.Image Guage;
    public UnityEngine.UI.Text Text;
    public string conectedValue;
    public string conectedMaxValue;
    public int currentValue;
    public int maxValue;
    public float latencyValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = (currentValue + "/" + maxValue).ToString();
        latencyValue += (currentValue - latencyValue) / 2 * Time.deltaTime * 8;
        Guage.fillAmount = latencyValue / maxValue;
    }

    public void damage(int pow)
    {
        
        currentValue -= pow;
        PlayerPrefs.SetInt(conectedValue, currentValue);
    }

    public void Refresh()
    {
        currentValue = PlayerPrefs.GetInt(conectedValue);
        maxValue = PlayerPrefs.GetInt(conectedMaxValue);
    }
}
