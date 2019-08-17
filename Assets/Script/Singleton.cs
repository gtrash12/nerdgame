using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private static Singleton instance;
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }

    private Singleton()
    {

    }

    public int hp;

    public void init()
    {
        PlayerPrefs.SetInt("체력", 100);
        PlayerPrefs.GetInt("체력", 100);
        PlayerPrefs.GetInt("돈", 0);
        PlayerPrefs.GetInt("에너지", 20);
        PlayerPrefs.GetInt("지능", 10);
        PlayerPrefs.GetInt("인성", 10);
        PlayerPrefs.GetInt("외모", 10);
        PlayerPrefs.GetInt("유머", 10);
        PlayerPrefs.GetInt("신앙심", 10);
    }
}
