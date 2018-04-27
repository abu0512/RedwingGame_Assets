using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateGroggy : WitchFSMStateBase
{
    private float _delayTime;

    public override void BeginState()
    {
        _delayTime = 0.0f;
        Witch.Anim.speed = 1.0f;
    }

    void Update()
    {
        _delayTime += Time.deltaTime;

        if (_delayTime >= WitchValueManager.I.GroggyDuration)
        {
            _delayTime = 0.0f;
            Witch.SetState(WitchState.GroggyRelease);
            return;
        }
    }

    public override void EndState()
    {
    }
}

