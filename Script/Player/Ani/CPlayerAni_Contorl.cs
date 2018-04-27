﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAni_State_Shild
{
    IdleRun = 0,
    Defense_Mode,
    Defense_ModeIdle,
    CountAttack,
    Attack1,
    Attack2,
    Attack3,
    Dash,
    Defense_ModeBack,
    SweatL,
    SweatR,
    ShildRun,
    SweatCount,
    None,
}
public enum PlayerAni_State_Scythe
{
    IdleRun = 0,
    Attack1,
    Attack2,
    Attack3,
    Skill2,
    Skill1
}


public class CPlayerAni_Contorl : CPlayerBase
{
    private CPlayerAniEvent _CPlayerAniEvent;
    private CPlayerDash _CPlayerDash;
    private CPlayerSwap _CPlayerSwap;
    // 애니 초기화
    public PlayerAni_State_Shild _PlayerAni_State_Shild = PlayerAni_State_Shild.IdleRun;
    public PlayerAni_State_Scythe _PlayerAni_State_Scythe = PlayerAni_State_Scythe.IdleRun;

    private Animator _PlayerAniFile;
    public Animator PlayerAniFile { get { return _PlayerAniFile; } set { value = _PlayerAniFile; } }

    public bool m_bDefenseIdle;
    public bool m_bDefenseBack;
    private CPlayerAttackEffect _CPlayerAttackEffect;

    public bool m_bAttack1;
    private float m_fAttack1;

    private float fScytheCameraTime; // 전방베기 카메라연출

    private float fSweatTime;
    private bool isSweat;
    public bool _isSweat { get { return isSweat; } set { value = isSweat; } }

    public bool isSweatCount;
    private bool _isSweatCount { get { return isSweatCount; } set { value = isSweatCount; } }

    void Start ()
    {
        _CPlayerSwap = GetComponent<CPlayerSwap>();
        _CPlayerDash = GetComponent<CPlayerDash>();
        _CPlayerAniEvent = GetComponent<CPlayerAniEvent>();
        _CPlayerAttackEffect = GetComponent<CPlayerAttackEffect>();

        // 방패로 가는 모션 체크
        m_bDefenseIdle = false; 
        // 현재 애니메이터 값 가져오기
        _PlayerAniFile = GetComponent<Animator>();
    }

    void Update()
    {
        if (_PlayerManager.m_bAnimator)
        {
            if (_PlayerManager.m_bSwap == false)
            {
                if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                {
                    ShieldAniGetKey();
                    ShieldAni();
                    SweatCount();
                }
                else
                {
                    ScytheAniGetKey();
                    ScytheAni();
                }
            }
        }
    }
   
    void ShieldAniGetKey()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.A) && _PlayerManager.m_PlayerStm > InspectorManager._InspectorManager.fSweatStm)
            {
                fSweatTime = 0;
                _PlayerAni_State_Shild = PlayerAni_State_Shild.SweatL;
                _PlayerManager.m_PlayerStm -= InspectorManager._InspectorManager.fSweatStm;
            }
            if (Input.GetKey(KeyCode.D) && _PlayerManager.m_PlayerStm > InspectorManager._InspectorManager.fSweatStm)
            {
                fSweatTime = 0;
                _PlayerAni_State_Shild = PlayerAni_State_Shild.SweatR;
                _PlayerManager.m_PlayerStm -= InspectorManager._InspectorManager.fSweatStm;
            }
            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // 현재 콤보가 0일경우
            if (_PlayerManager.m_nAttackCombo == 0)
            {
                // 1타로넘김
                _PlayerAni_State_Shild = PlayerAni_State_Shild.Attack1;
            }
            // 공격중일경우
            if (_PlayerManager.m_bAttack)
            {
                // 1타일때
                if (_PlayerManager.m_nAttackCombo == 1)
                {
                    //2타로넘김
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Attack2;
                }// 2타일때
                else if (_PlayerManager.m_nAttackCombo == 2)
                {
                    //3타로넘김
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Attack3;
                }
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (_PlayerManager.m_PlayerStm > 0)
            {
                _PlayerManager._PlayerMove.SpeedReset();
                if (_PlayerManager.m_nAttackCombo != 0 && _PlayerManager.m_bAttack)
                {
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_Mode;
                }
                else
                {
                    if (m_bDefenseIdle)
                    {
                        _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_ModeIdle;
                    }
                    else
                    {
                        _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_Mode;
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && _PlayerManager.m_bMove == false)
        {
            _CPlayerAniEvent.MoveTypes(2);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && _PlayerManager.m_PlayerStm >= InspectorManager._InspectorManager.fStmDash)
        {
            _PlayerAni_State_Shild = PlayerAni_State_Shild.Dash;
            _PlayerManager.m_PlayerStm -= InspectorManager._InspectorManager.fStmDash;
        }
        else
        {
            m_bDefenseIdle = false;
        }        

        if(m_bDefenseBack)
        {
            _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_ModeBack;
        }
        if (Input.GetMouseButton(1) && _PlayerManager._isCountAttack)
        {
            _PlayerAni_State_Shild = PlayerAni_State_Shild.CountAttack;
            _PlayerManager._CPlayerCountAttack.isCount = true;
            _PlayerManager._isCountAttack = false;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && _PlayerManager.m_PlayerStm > InspectorManager._InspectorManager.fShildRunStm)
        {
            _PlayerAni_State_Shild = PlayerAni_State_Shild.ShildRun;
            _PlayerManager.m_PlayerStm -= InspectorManager._InspectorManager.fShildRunStm;
        }
            
    }
    void ShieldAni()
    {
        switch (_PlayerAni_State_Shild)
        {
            case PlayerAni_State_Shild.None:
                {

                }break;
            case PlayerAni_State_Shild.IdleRun:
                {
                    Animation_Change(0);
                    MoveMent();
                }
                break;
            case PlayerAni_State_Shild.Defense_Mode:
                {
                    Animation_Change(1);
                }break;
            case PlayerAni_State_Shild.Defense_ModeIdle:
                {
                    Animation_Change(2);
                }break;
            case PlayerAni_State_Shild.CountAttack:
                {
                    Animation_Change(3);
                }break;
            case PlayerAni_State_Shild.Attack1:
                {
                    Animation_Change(4);
                }
                break;
            case PlayerAni_State_Shild.Attack2:
                {
                    Animation_Change(5);
                }
                break;
            case PlayerAni_State_Shild.Attack3:
                {
                    Animation_Change(6);
                }
                break;
            case PlayerAni_State_Shild.Dash:
                {
                    Animation_Change(7);
                }
                break;
            case PlayerAni_State_Shild.Defense_ModeBack:
                {
                    CPlayerAttackEffect._instance.Effect9(); 
                    Animation_Change(8);
                }
                break;
            case PlayerAni_State_Shild.SweatL:
                {
                    SweatStart();
                    Animation_Change(9);
                }
                break;
            case PlayerAni_State_Shild.SweatR:
                {
                    SweatStart();
                    Animation_Change(10);
                }
                break;
            case PlayerAni_State_Shild.ShildRun:
                {
                    Animation_Change(11);
                }
                break;
            case PlayerAni_State_Shild.SweatCount:
                {
                    Animation_Change(12);
                }
                break;
        }

    }

    void ScytheAniGetKey()
    {
        if (_PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill1)
            return;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            fScytheCameraTime = 0;
            _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Skill2;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 현재 콤보가 0일경우
                if (_PlayerManager.m_nAttackCombo == 0)
                {
                    // 1타로넘김
                    _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Attack1;
                }
                // 공격중일경우
                if (_PlayerManager.m_bAttack)
                {
                    // 1타일때
                    if (_PlayerManager.m_nAttackCombo == 1)
                    {
                        //2타로넘김                    
                        _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Attack2;
                    }// 2타일때
                    else if (_PlayerManager.m_nAttackCombo == 2)
                    {
                        //3타로넘김
                        _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Attack3;
                    }
                }
            }
        }
    }
    void ScytheAni()
    {
        switch (_PlayerAni_State_Scythe)
        {
            case PlayerAni_State_Scythe.IdleRun:
                {
                    Animation_Change(0);
                    MoveMent();
                }
                break;
            case PlayerAni_State_Scythe.Attack1:
                {
                    Animation_Change(1);
                }
                break;
            case PlayerAni_State_Scythe.Attack2:
                {
                    Animation_Change(2);
                }
                break;
            case PlayerAni_State_Scythe.Attack3:
                {
                    Animation_Change(3);
                }
                break;
            case PlayerAni_State_Scythe.Skill2:
                {
                    fScytheCameraTime += Time.deltaTime;
                    if(fScytheCameraTime < 1.0f)
                        _PlayerManager.PlayerHitCamera2(2.5f);
                    else
                        _PlayerManager.PlayerHitCamera2(5.0f);

                    Animation_Change(4);
                }
                break;

            case PlayerAni_State_Scythe.Skill1:
                {
                    Animation_Change(5);
                }
                break;
        }
    }
    public void Animation_Change(int animation_number)
    {
        _PlayerAniFile = GetComponent<Animator>();
        _PlayerAniFile.SetInteger("motion", animation_number);
    }

    public void DefenseIdle()
    {
        m_bDefenseIdle = true;
        _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_ModeIdle;
    }

    public void DashStart()
    {
        _CPlayerDash.m_isDash = true;
        _PlayerManager.m_bMove = false;
        _PlayerManager.m_nAttackCombo = 0;
        _PlayerManager.m_PlayerStm -= InspectorManager._InspectorManager.fStmDash;
    }

    void MoveMent()
    {
        _PlayerManager.m_nAttackCombo = 0;
        if (_PlayerManager.m_nAttackCombo == 0 && _PlayerManager.m_bMove)
        {
            float horizontal = _PlayerManager._PlayerMove.fHorizontal;
            float vertical = _PlayerManager._PlayerMove.fVertical;
            _PlayerAniFile.SetFloat("MoveSpeed", horizontal * horizontal + vertical * vertical, 0.1f, Time.deltaTime);
        }
    }
    
    void SweatStart()
    {
        _PlayerManager._PlayerShild._isShildCounter = true;
        isSweat = true;
    }

    void SweatCount()
    {
        if (!isSweatCount)
            return;
        
        if (_PlayerManager._isPlayerHorn)
        {
            CPlayerAttackEffect._instance.Effect9();
            if (Input.GetMouseButton(1))
            {
                _PlayerAni_State_Shild = PlayerAni_State_Shild.SweatCount;
            }
        }
    }

    public void AniStiff()
    {
        StartCoroutine("Stiff");
    }
    IEnumerator Stiff()
    {
        _PlayerAniFile.speed = 0;
        //TimeScalManager._instance.TimeScal(0.4f);
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fPlayerAttackStiff);
        //TimeScalManager._instance.TimeScal(1f);
        _PlayerAniFile.speed = 1;
    }

    public void isSweatEvent()
    {
        isSweat = false;
    }
    public void isSweatCountEvent()
    {
        isSweatCount = false;
    }


}