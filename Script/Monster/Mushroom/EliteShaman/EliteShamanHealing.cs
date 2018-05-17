using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanHealing : EliteShamanStateBase
{
    EliteShamanEffect _effect;

    public override void BeginState()
    {
       _effect.EffectofHeal(transform.position);
        Dltime = 0;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void HealCheck()
    {
        EliteShaman.HealStart = true;
        EliteShaman.HealTimer = 0;
    }

    private void Awake()
    {
        _effect = GetComponent<EliteShamanEffect>();
    }

    void Update()
    {
        EliteShaman.GroggyCheck();
        EliteShaman.PlayerisDead();
        Dltime += Time.deltaTime;
        EliteShaman.GoToDestination(transform.position, 0, 0);

        if (Dltime > 2f)
        {
            _effect.HealEffect.SetActive(false);
            EliteShaman.SetState(EliteShamanState.Chase);
            return;
        }
    }
}
