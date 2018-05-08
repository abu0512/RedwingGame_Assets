using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomBAttack : GuardMushroomStateBase
{

    public override void BeginState()
    {
        Dltime = 0f;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void BAttackCheck()
    {
        if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.AttackDistance + 4.5f 
            && GuardMushroom.PlayerisFront)
        {
            GuardMushroom.AttackTimer = 0f;

            if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, GuardMushroom.BerserkerAttackDamage);
            }

            else
            {
                CPlayerManager._instance.PlayerHp(0.2f, 1, GuardMushroom.BerserkerAttackDamage);
            }
        }
    }

    void Update()
    {
        GuardMushroom.TurnToDestination();
        GuardMushroom.GoToPullPush();
        GuardMushroom.NowisHit();
        GuardMushroom.PlayerisDead();

        Dltime += Time.deltaTime;

        if (Dltime > 0.4f)
        {
                GuardMushroom.SetState(GuardMushroomState.BChase);
                Dltime = 0;
                return;
        }
    }
}
