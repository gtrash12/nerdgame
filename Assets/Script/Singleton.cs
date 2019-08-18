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
        PlayerPrefs.GetInt("체력", 100);
        PlayerPrefs.GetInt("최대체력", 100);
        PlayerPrefs.GetInt("힘", 10);
        PlayerPrefs.GetInt("돈", 0);
        PlayerPrefs.GetInt("에너지", 20);
        PlayerPrefs.GetInt("지능", 0);
        PlayerPrefs.GetInt("인성", 0);
        PlayerPrefs.GetInt("외모", 0);
        PlayerPrefs.GetInt("유머", 0);
        PlayerPrefs.GetInt("신앙심", 0);
        PlayerPrefs.GetInt("예술", 0);

        // 행동
        PlayerPrefs.GetInt("조깅", 0);
        PlayerPrefs.GetInt("자습", 0);
        PlayerPrefs.GetInt("팔굽혀펴기", 0);
        PlayerPrefs.GetInt("얼굴마사지", 0);
    }
}
