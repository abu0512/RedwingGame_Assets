using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerParams : CharacterUI 
{
    public static PlayerParams _instance = null;

    protected CPlayerManager _CPlayerManager;
    protected CPlayerManager CPlayerManager { get { return _CPlayerManager; } set { _CPlayerManager = value; } }

    public string names { get; set; }
    public Image HPBar;
    public Image SPBar;
    public GameObject[] PlayerType;
    public GameObject[] PlayerWType;


    public GameObject[] PlayerPawerGauge;
    public int nGauge;

    public override void InitParams()
    {
        PlayerParams._instance = this;

        names = "Player";
        maxHP = CPlayerManager._instance.m_PlayerMaxHp;
        curHP = CPlayerManager._instance.m_PlayerHp;
        maxSP = CPlayerManager._instance.m_PlayerMaxStm;
        curSP = maxSP;

        for (int i = 0; i < PlayerPawerGauge.Length; i++)
            PlayerPawerGauge[i].SetActive(false);
    }

    private void Awake()
    {
        HPBar = GameObject.FindGameObjectWithTag("HP").GetComponentInChildren<Image>();
        SPBar = GameObject.FindGameObjectWithTag("Stm").GetComponentInChildren<Image>();
    }

    public void SetHp()
    {
        curHP = CPlayerManager._instance.m_PlayerHp;
        curHP = Mathf.Clamp(curHP, 0, maxHP);
    }

    public void HPlocalScale()
    {
        HPBar.fillAmount = curHP / maxHP;
    }

    public void SetSp()
    {
        curSP = CPlayerManager._instance.m_PlayerStm;
        curSP = Mathf.Clamp(curSP, 0, maxSP);
    }

    public void SPlocalScale()
    {
        SPBar.fillAmount = curSP / maxSP;
    }

    public void SetPlayerType()
    {
        if(CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            PlayerType[1].SetActive(false);
            PlayerWType[1].SetActive(false);
            PlayerType[0].SetActive(true);
            PlayerWType[0].SetActive(true);
        }

        else
        {
            PlayerType[1].SetActive(true);
            PlayerWType[1].SetActive(true);
            PlayerType[0].SetActive(false);
            PlayerWType[0].SetActive(false);
        }
    }

    public void Gauge()
    {
        int i = CPlayerManager._instance._nPowerGauge;

        if (i >= 50) GaugeActive(0, true);
        if (i >= 100) GaugeActive(1, true);
        if (i >= 150) GaugeActive(2, true);
        if (i >= 200) GaugeActive(3, true);
        if (i >= 250) GaugeActive(4, true);
        if (i >= 300) GaugeActive(5, true);

    }
    void GaugeActive(int type, bool isType)
    {
        PlayerPawerGauge[type].SetActive(isType);
        nGauge = type + 1;
    }
    public void GaugeOff()
    {
        for (int i = 0; i < PlayerPawerGauge.Length; i++)
            PlayerPawerGauge[i].SetActive(false);
    }

    void Update()
    {
        Debug.Log(CPlayerManager._instance.m_PlayerHp);
        // Player 캐릭터의 체력과 스테미너의 값을 받아온다.
        SetHp();
        SetSp();

        // player 캐릭터 HP bar 실시간 UI 상태 변화
        HPlocalScale();

        // player 캐릭터 스테미너 bar 실시간 UI 상태 변화
        SPlocalScale();

        // Player 캐릭터 타입 + 무기 타입 실시간 상태 변화
        SetPlayerType();

        Gauge();
    }
}
