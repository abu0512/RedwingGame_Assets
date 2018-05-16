﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomisHit : QueenMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
    }

    public override void EndState()
    {
        base.EndState();
        QueenMushroom.AttackTimer = 1.3f;
    }

    void Update()
    {
        QueenMushroom.TurnToHitDestination();
        QueenMushroom.NowisHit();
        QueenMushroom.GoToPullPush();

        Dltime += Time.deltaTime;

        if (Dltime > 0.1f)
        {
            QueenMushroom.SetState(QueenMushroomState.Return);
            return;
        }
    }
}