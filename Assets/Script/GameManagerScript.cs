using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public RollScript RollManager;
    public AudioSource SFXSource;
    public GuageScript HpG;
    public UnityEngine.UI.Text energyT;
    public UnityEngine.UI.Text powT;
    public UnityEngine.UI.Text intT;
    public UnityEngine.UI.Text lookT;
    public UnityEngine.UI.Text conT;
    public UnityEngine.UI.Text pieT;
    public UnityEngine.UI.Text artT;

    void Start()
    {
        Singleton.Instance.init(RollManager, SFXSource, HpG, energyT, powT,intT,lookT,conT,pieT,artT);
        Singleton.Instance.textRefresh();
    }
}
