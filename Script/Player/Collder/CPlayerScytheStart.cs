using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerScytheStart : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen")
        {
            CPlayerManager._instance.PlayerHitCamera(3.0f, 0.2f);
            if (other.tag == "Guard")
            {
                other.GetComponent<MonsterBase>().isHit = true;

                if(CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge);
                else
                    other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheStartSkillDamge);
            }

            else if (other.tag == "Queen")
            {
                other.GetComponent<MonsterBase>().isHit = true;

                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge);
                else
                    other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheStartSkillDamge);
            }

            else
            {
                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge);
                else
                    other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.fScytheStartSkillDamge);
            }
            CPlayerManager._instance._PlayerAni_Contorl.AniStiff();
        }
    }
}

