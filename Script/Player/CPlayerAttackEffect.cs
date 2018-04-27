using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAttackEffect : MonoBehaviour
{
    public static CPlayerAttackEffect _instance = null;

    CPlayerManager m_manager;

    public GameObject[] _AttackEffect;
    public GameObject[] _ScytheAttackEffect;

    private void Start()
    {
        CPlayerAttackEffect._instance = this;
        m_manager = GetComponent<CPlayerManager>();
    }
    private void Update()
    {
        if (m_manager.m_nAttackCombo == 0 && !m_manager.m_bAttack)
            EffectOff();
    }

    public void EffectOff()
    {
        if(m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            for (int i = 0; i < _AttackEffect.Length; i++)
                _AttackEffect[i].SetActive(false);
        }
        else
        {
            for (int i = 0; i < _ScytheAttackEffect.Length; i++)
                _ScytheAttackEffect[i].SetActive(false);
        }
    }
    public void Effect1()
    {
        if (m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
            _AttackEffect[0].SetActive(true);
        else
            _ScytheAttackEffect[0].SetActive(true);
    }
    public void Effect2()
    {
        if (m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
            _AttackEffect[1].SetActive(true);
        else
            _ScytheAttackEffect[1].SetActive(true);
    }
    public void Effect3()
    {
        if (m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
            _AttackEffect[2].SetActive(true);
        else
            _ScytheAttackEffect[2].SetActive(true);
    }
    public void Effect4()
    {
        _AttackEffect[3].SetActive(true);
    }
    public void Effect5()
    {
        _ScytheAttackEffect[3].SetActive(true);
    }
    public void Effect6()
    {   
        _ScytheAttackEffect[4].SetActive(true);
    }
    public void Effect7()
    {
        _AttackEffect[4].SetActive(true);
    }
    public void Effect8()
    {
        _AttackEffect[5].SetActive(true);
    }
    public void Effect9()
    {
        _AttackEffect[6].SetActive(true);
    }

    public void EffectOnOff(bool one, bool two, bool three, bool four)
    {
        //_AttackEffect[0].SetActive(one);
        //_AttackEffect[1].SetActive(two);
        //_AttackEffect[2].SetActive(three);
        //_AttackEffect[3].SetActive(four);
    }
}
