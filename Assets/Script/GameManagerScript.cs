using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public RollScript RollManager;
    public AudioSource SFXSource;
    public NeedMonitorScript NeedManager;
    public GuageScript HpG;
    public ValuetoTextScript MoneyT;
    public ValuetoTextScript energyT;
    public ValuetoTextScript powT;
    public ValuetoTextScript intT;
    public ValuetoTextScript lookT;
    public ValuetoTextScript conT;
    public ValuetoTextScript pieT;
    public ValuetoTextScript artT;
    public CollectingEffectScript CollectingManager;

    public SkillData[] SkillDatas;

    void Awake()
    {
        init();
        Singleton.Instance.init(RollManager, SFXSource, NeedManager, CollectingManager, MoneyT, HpG, energyT, powT,intT,lookT,conT,pieT,artT);
        Singleton.Instance.textRefresh();
        Singleton.Instance.Heal(60);
    }

    private void init()
    {
        //PlayerPrefs.DeleteAll();
        //능력치
        if (PlayerPrefs.HasKey("체력") != true)
        {
            PlayerPrefs.SetInt("체력", 100);
            PlayerPrefs.SetInt("최대체력", 100);
            PlayerPrefs.SetInt("힘", 10);
            PlayerPrefs.SetInt("돈", 0);
            PlayerPrefs.SetInt("에너지", 20);
            PlayerPrefs.SetInt("지능", 0);
            PlayerPrefs.SetInt("외모", 0);
            PlayerPrefs.SetInt("화술", 0);
            PlayerPrefs.SetInt("신앙심", 0);
            PlayerPrefs.SetInt("예술", 0);

            // 행동
            PlayerPrefs.SetInt("조깅", 1);
            PlayerPrefs.SetInt("삼각김밥", 1);
            PlayerPrefs.SetInt("자습", 0);
            PlayerPrefs.SetInt("팔굽혀펴기", 0);
            PlayerPrefs.SetInt("얼굴마사지", 0);
            ;

            //슬롯
            PlayerPrefs.SetString("슬롯0", "");
            PlayerPrefs.SetString("슬롯1", "");
            PlayerPrefs.SetString("슬롯2", "");
            PlayerPrefs.SetString("슬롯3", "");
            PlayerPrefs.SetString("슬롯4", "");

            //호감도
            PlayerPrefs.SetInt("고연서호감도", 0);
            PlayerPrefs.SetInt("고연서레벨", 1);

        }
        /*
        PlayerPrefs.GetInt("체력", 100);
        PlayerPrefs.GetInt("최대체력", 100);
        PlayerPrefs.GetInt("힘", 10);
        PlayerPrefs.GetInt("돈", 0);
        PlayerPrefs.GetInt("에너지", 20);
        PlayerPrefs.GetInt("지능", 0);
        PlayerPrefs.GetInt("외모", 0);
        PlayerPrefs.GetInt("화술", 0);
        PlayerPrefs.GetInt("신앙심", 0);
        PlayerPrefs.GetInt("예술", 0);

        // 행동
        PlayerPrefs.SetInt("조깅", 1);
        PlayerPrefs.SetInt("삼각김밥", 1);
        PlayerPrefs.GetInt("자습", 0);
        PlayerPrefs.GetInt("팔굽혀펴기", 0);
        PlayerPrefs.GetInt("얼굴마사지", 0);
;

        //슬롯
        PlayerPrefs.GetString("슬롯0", "");
        PlayerPrefs.GetString("슬롯1", "");
        PlayerPrefs.GetString("슬롯2", "");
        PlayerPrefs.GetString("슬롯3", "");
        PlayerPrefs.GetString("슬롯4", "");

        Debug.Log(PlayerPrefs.GetInt("힘"));
        */
    }
}
