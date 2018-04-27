using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerDash : MonoBehaviour
{
    CPlayerManager _CPlayerManager;
    public bool m_isDash;
    public float m_fStartTime;
    public float m_fEndTime;
    public float m_fDashSpped;

    
    private void Awake()
    {
        _CPlayerManager = GetComponent<CPlayerManager>();
        m_isDash = false;
        m_fStartTime = 0;
    }
	
	void Update ()
    {
        if (m_isDash)
        {
            m_fStartTime += Time.deltaTime;
            _CPlayerManager._PlayerController.Move(transform.forward * Time.deltaTime * m_fDashSpped);
            _CPlayerManager.EDITOR_ROTATIONSPEED = _CPlayerManager.EDITOR_ROTATIONSPEED * 4;

            if (Input.GetMouseButton(0) && m_fStartTime > m_fEndTime - 0.05f)
            {
                _CPlayerManager._PlayerAni_Contorl._PlayerAni_State_Shild = PlayerAni_State_Shild.Attack1;
                m_fStartTime = 0;
                m_isDash = false;
            }
            else
            {
                if (m_fStartTime >= m_fEndTime)
                {
                    _CPlayerManager.m_isRotationAttack = true;
                    _CPlayerManager.m_bMove = true;
                }
                if (m_fStartTime >= m_fEndTime + 0.1f)
                {
                    m_fStartTime = 0;
                    m_isDash = false;
                }
            }
        }
	}
}
