﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerManager : MonoBehaviour
{
    public static CPlayerManager _instance = null;

    private CPlayerMove _CPlayerMove = null;
    public CPlayerMove _PlayerMove { get { return _CPlayerMove; } }
    
    private CPlayerAni_Contorl _CPlayerAni_Contorl = null;
    public CPlayerAni_Contorl _PlayerAni_Contorl {  get { return _CPlayerAni_Contorl; } }

    public CPlayerAniEvent _CPlayerAniEvent = null;    

    private CPlayerSwap _CPlayerSwap = null;
    public CPlayerSwap _PlayerSwap { get { return _CPlayerSwap; } }

    private CPlayerShild _CPlayerShild = null;
    public CPlayerShild _PlayerShild { get { return _CPlayerShild; } }

    public CPlayerCountAttack _CPlayerCountAttack;

    // 플레이어 속도
    [SerializeField]
    private float m_fMoveSpeed;
    public float m_MoveSpeed { get { return m_fMoveSpeed; } set { m_fMoveSpeed = value; } }

    // 플레이어 중력
    [SerializeField]
    private float m_fGravity;
    public float m_Gravity { get { return m_fGravity; } set { m_fGravity = value; } }

    // 플레이어 HP
    [SerializeField]
    private float m_fPlayerHp;
    public float m_PlayerHp { get { return m_fPlayerHp; } set { m_fPlayerHp = value; } }

    // 플레이어 MaxHP
    [SerializeField]
    private float m_fPlayerMaxHp;
    public float m_PlayerMaxHp { get { return m_fPlayerMaxHp; } set { m_fPlayerMaxHp = value; } }

    // 플레이어 딜러형 HP
    [SerializeField]
    private float m_fscyPlayerHp;
    public float m_ScyPlayerHp { get { return m_fscyPlayerHp; } set { m_fscyPlayerHp = value; } }

    // 플레이어 딜러형 MaxHP
    [SerializeField]
    private float m_fscyPlayerMaxHp;
    public float m_ScyPlayerMaxHp { get { return m_fscyPlayerMaxHp; } set { m_fscyPlayerMaxHp = value; } }

    // 플레이어 스테미나
    [SerializeField]
    private float m_fPlayerStm;
    public float m_PlayerStm { get { return m_fPlayerStm; } set { m_fPlayerStm = value; } }

    // 플레이어 Max스테미나
    [SerializeField]
    private float m_fPlayerMaxStm;
    public float m_PlayerMaxStm { get { return m_fPlayerMaxStm; } set { m_fPlayerMaxStm = value; } }

    // 플레이어 게이지 (반격,방어 등)
    [SerializeField]
    private float m_fPlayerGauge;
    public float m_PlayerGauge { get { return m_fPlayerGauge; } set { m_fPlayerGauge = value; } }

    // 플레이어 반격공격 데미지
    [SerializeField]
    private int m_nPlayerParryDmg;
    public int m_PlayerParryDmg { get { return m_nPlayerParryDmg; } set { m_nPlayerParryDmg = value; } }

    // 플레이어 검방 공격력
    [SerializeField]
    private int[] m_nPlayerShildHitDmg = new int[5];
    public int[] m_PlayerShildHitDmg { get { return m_nPlayerShildHitDmg; } set { m_nPlayerShildHitDmg = value; } }
    // 플레이어 낫모드 공격력
    [SerializeField]
    private int[] m_nPlayerScytheHitDmg = new int[3];
    public int[] m_PlayerScytheHitDmg { get { return m_nPlayerScytheHitDmg; } set { m_nPlayerScytheHitDmg = value; } }

    // 스왑 모션이 일어났는지를 체크하기 위한 bool
    private bool _isPull;
    public bool isPull { get { return _isPull; } set { _isPull = value; } }

    // 스왑 모션이 일어났는지를 체크하기 위한 bool
    private bool _isPush;
    public bool isPush { get { return _isPush; } set { _isPush = value; } }

    // 플레이어 파워 응축 
    private int nPowerGauge;
    public int _nPowerGauge { get { return nPowerGauge; } set { nPowerGauge = value; } }

    public Quaternion vPlayerQuaternion = Quaternion.identity; // 플레이어 로테이션
    public CharacterController _PlayerController; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    public bool m_bMove; // 플레이어가 이동중인지
    public bool m_bAttack; // 플레이어가 공격중인지 체크
    public int m_nAttackCombo; // 플레이어 타수콤보 체크 ( 1타,2타,3타 연계에 사용함)
    public bool m_bAnimator; // 기본 공격이 아닌 스킬을 사용할때 다른동작을 막기위해 사용
    public bool m_bSwap; // 스왑할때 애니메이션 Idle 안들어가게 막기

    public bool m_isRotation; // 현재 플레이어가 회전중인가
    public bool m_isRotationAttack;
    public float fRotationSave; // 플레이어 공격할때 회전각도 저장
    public bool isDead; // 현재 플레이어가 죽었는지

    public float EDITOR_MOVESPEED;
    public float EDITOR_ROTATIONSPEED;
    public float EDITOR_MOVEANGLE;

    private bool isCountAttack;
    public bool _isCountAttack { get { return isCountAttack; } set { isCountAttack = value; } }

    private bool isPlayerHorn; // 플레이어 무적
    public bool _isPlayerHorn {  get { return isPlayerHorn; } set { isPlayerHorn = value; } }

    void Awake()
    {
        CPlayerManager._instance = this;

        Init();
    }
    void Init()
    {
        _CPlayerMove = GetComponent<CPlayerMove>();
        _CPlayerAni_Contorl = GetComponent<CPlayerAni_Contorl>();
        _PlayerController = GetComponent<CharacterController>();
        _CPlayerSwap = GetComponent<CPlayerSwap>();
        _CPlayerShild = GetComponent<CPlayerShild>();
        _CPlayerAniEvent = GetComponent<CPlayerAniEvent>();
        _CPlayerCountAttack = GetComponent<CPlayerCountAttack>();
        // 플레이어 스탯 설정
        m_fMoveSpeed = 6;
        m_fGravity = 20;
        m_fPlayerMaxHp = 500;
        m_fPlayerHp = m_fPlayerMaxHp;
        m_fscyPlayerMaxHp = m_fPlayerMaxHp / 2;
        m_fscyPlayerHp = m_fscyPlayerMaxHp;
        m_fPlayerMaxStm = 100;
        m_fPlayerStm = m_fPlayerMaxStm;
        m_fPlayerGauge = 100;
        m_nAttackCombo = 0;
        nPowerGauge = 0;
        isDead = false;

        m_bMove = true;
        m_bAnimator = true;
        m_isRotationAttack = true;
        isPlayerHorn = false;
    }
    void Update()
    {
        // 수치조절
        for (int i = 0; i < m_nPlayerShildHitDmg.Length; i++)
        {
            m_nPlayerShildHitDmg[i] = InspectorManager._InspectorManager.nDamgeShild[i];
        }
        for (int i = 0; i < m_nPlayerScytheHitDmg.Length; i++)
        {
            m_nPlayerScytheHitDmg[i] = InspectorManager._InspectorManager.nDamgeScythe[i];
        }

        EDITOR_ROTATIONSPEED = InspectorManager._InspectorManager.fRotation;
        EDITOR_MOVESPEED = InspectorManager._InspectorManager.fMoveSpeed;
        EDITOR_MOVEANGLE = InspectorManager._InspectorManager.fMoveAngle;

        m_fPlayerStm = Mathf.Clamp(m_fPlayerStm, 0, 100.0f);

        if (m_fPlayerHp <= 0)
        {
            gameObject.SetActive(false);
        }

        if( m_bAttack)
        {
            EDITOR_ROTATIONSPEED = EDITOR_ROTATIONSPEED * 1000;
        }
        else
        {
            if (EDITOR_ROTATIONSPEED <= InspectorManager._InspectorManager.fRotation)
                EDITOR_ROTATIONSPEED = InspectorManager._InspectorManager.fRotation;
            else
                EDITOR_ROTATIONSPEED = (InspectorManager._InspectorManager.fRotation + EDITOR_ROTATIONSPEED) / 4;
        }

        PlayerRotationSave();
        PlayerRotation();
        PlayerHornOn();
        m_fPlayerHp = Mathf.Clamp(m_fPlayerHp, 0, m_fPlayerMaxHp);
        nPowerGauge = Mathf.Clamp(nPowerGauge, 0, 300);
    }


    // 플레이어 사망시
    public void PlayerDead()
    {
        if (isDead == true)
        {
            gameObject.SetActive(false);
            return;
        }
    }
    // 플레이어 로테이션을 부드럽게 이동
    public void PlayerRotation()
    {
        if (!m_isRotationAttack)
            return;

        if(CCameraFind._instance.m_bCamera)
        {
            Vector3 Forward = _CPlayerMove.m_moveDir;
            vPlayerQuaternion = transform.rotation;

            if (Forward != Vector3.zero)
            {
                vPlayerQuaternion = Quaternion.RotateTowards(
                    vPlayerQuaternion,
                    Quaternion.LookRotation(Forward),
                    EDITOR_ROTATIONSPEED * Time.deltaTime);
                m_isRotation = true;
            }
        
            transform.rotation = vPlayerQuaternion;
        
            if (Vector3.Distance(Forward, transform.forward) <= EDITOR_MOVEANGLE)
            {
                m_isRotation = false;
            }
        }
    }
 
    // 플레이어 데미지 처리 
    public float PlayerHp(float shake = 0.0f, int type = 1, float sizeHp = 0)
    {
        CCameraShake._instance.shake = shake;

        // type = 1  플레이어 / type = 2 방패
        if (type == 1)
        {
            SoundManager.I.PlaySound(transform, PlaySoundId.Hit_Pc);
            if (!isPlayerHorn) // 플레이어가 무적상태가 아닐때
            {
                // 플레이어가 검방패 모드일때
                if (_PlayerSwap._PlayerMode == PlayerMode.Shield)
                {
                    // hp내림
                    m_fPlayerHp -= sizeHp;
                }
                else // 낫 모드일때
                {
                    // hp 내림
                    m_fscyPlayerHp -= sizeHp;
                }
            }

            // 플레이어가 흘리기 중일경우
            if (_CPlayerAni_Contorl._isSweat)
            {
                // 플레이어가 흘리기도중 반격을 할수있음
                _CPlayerAni_Contorl.isSweatCount = true;
                // 플레이어 무적 시작
                PlayerHornOn();
                // 이펙트 호출
                CPlayerAttackEffect._instance.Effect9();
            }
        }
        // 방패일때 데미지안들어가~
        else if (type == 2)
        {
            SoundManager.I.PlaySound(transform, PlaySoundId.Defense_Shield);
            // 방패모드 맞았을때 hit 출력
            _CPlayerShild.m_bShildCollider = true;
            // 체력대신 스테미너 깎음
            m_fPlayerStm -= sizeHp * InspectorManager._InspectorManager.fShildDamge;
        }
        else
            m_fscyPlayerHp -= sizeHp;

        if (m_fPlayerHp <= 0 || m_fscyPlayerHp <= 0)
            isDead = true;

        return m_fPlayerHp;
    }
    // 카메라 연출 줌,인 연출 함수
    public void PlayerHitCamera(float hitDitance, float shake = 0)
    {
        CCameraRayObj._instance.MaxCamera(hitDitance);
        CCameraShake._instance.shake = shake;
    }
    public void PlayerHitCamera2(float hitDitance)
    {
        CCameraRayObj._instance.MaxCamera(hitDitance);
    }

    public void PlayerRotationSave()
    {
        if (m_bAttack)
        {
            if (fRotationSave != vPlayerQuaternion.y)
            {
                m_isRotationAttack = false;
            }
        }
        else
            return;
    }

    // 스왑 했을때 hp변경
    public void SwapHpType(int type)
    {
        // 2 흑화 -> 실드
        if (type == 1)
        {
            m_fPlayerHp = m_fscyPlayerHp * 2;
        }// 1 실드 -> 흑화
        else
        {
            m_fscyPlayerHp = m_fPlayerHp / 2;
        }
    }
    // 쉴드 상태에서 n초간 카운터 어택 유지
    public void PlayerSound(int type)
    {
        if (CSoundManager._instance == null)
            return;

        CSoundManager._instance.PlaySoundType(type);
    }
    public void SoundStop()
    {
        CSoundManager._instance._AS_Audio.Stop();
    }
    public void StartShildCounter()
    {
        isCountAttack = true;
        StartCoroutine(CountAttackReturn());
    }
    // 카운트어택을 사용할 수있는 시간
    IEnumerator CountAttackReturn()
    {
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fCountAttackReturnTime);
        isCountAttack = false;
    }

    // 플레이어 무적상태호출
    public void PlayerHornOn()
    {
        if (!_CPlayerAni_Contorl._isSweat)
            return;

        // 흘리기도중
        if (_CPlayerAni_Contorl._isSweat)
        {
            // 무적 시작
            isPlayerHorn = true;
            // 일정시간뒤에 무적해제
            StartCoroutine("StartHorn");
        }
    }

    IEnumerator StartHorn()
    {
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fPlayerHornTime);
        isPlayerHorn = false; // 무적해제
    }

    private void PlayStandardAttackSound()
    {
        if (_PlayerSwap._PlayerMode == PlayerMode.Shield)
            SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Original);
        if (_PlayerSwap._PlayerMode == PlayerMode.Scythe)
            SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Scythe);
    }

    private void PlayFinishAttackSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Finish);
    }

    private void PlayCounterAttackSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Counter);
    }

    private void PlayWideCutSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Skill_ScytheWideCut);
    }
}
