using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomDead : GuardMushroomStateBase
{
    private float DeadTime;

    public override void BeginState()
    {
        DeadTime = 0;
        GuardMushroom.CharacterisDead = true;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        if (GuardMushroom.isDead)
        {
            GuardMushroom.rotAnglePerSecond = 0;
            GuardMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            GuardMushroom.CharacterisDead = true;
            if (DeadTime >= 1.2f)
            {
                GuardMushroom.OnDead();
                return;
            }
        }
    }
}
