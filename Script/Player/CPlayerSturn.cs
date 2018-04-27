using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSturn : MonoBehaviour
{
    public GameObject _SturnEffect;
    public static CPlayerSturn _instance = null;

    public bool isSturn;
    


    private void Awake()
    {
        CPlayerSturn._instance = this;
    }
	
	void Update ()
    {
        Sturn();
    }

    void Sturn()
    {
        if (!isSturn)
        {
            StopCoroutine("SturnCoolTime");
            return;
        }

        StartCoroutine("SturnCoolTime");
    }


    IEnumerator SturnCoolTime()
    {
        _SturnEffect.SetActive(true);
        CPlayerManager._instance._CPlayerAniEvent.MoveTypes(1);
        CPlayerManager._instance.m_isRotationAttack = false;
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fSturnTime);
        _SturnEffect.SetActive(false);
        CPlayerManager._instance._CPlayerAniEvent.MoveTypes(2);
        CPlayerManager._instance.m_isRotationAttack = true;
        isSturn = false;
    }
}
