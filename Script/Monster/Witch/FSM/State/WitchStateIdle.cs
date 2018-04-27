using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateIdle : WitchFSMStateBase
{
    private float _distance;

    public override void BeginState()
    {
    }

    void Update ()
    {
		if (Vector3.Distance(transform.position, Witch.Target.transform.position) <= 15.0f)
        {
            Witch.SetState(WitchState.Chase);
        }
	}

    public override void EndState()
    {
    }
}
