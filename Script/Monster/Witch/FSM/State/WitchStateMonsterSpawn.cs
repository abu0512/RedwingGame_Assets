using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateMonsterSpawn : WitchFSMStateBase
{

    public override void BeginState()
    {
        Witch.SkillSys.OnTeleport(transform, 2);
    }

    void Update ()
    {
		
	}

    public override void EndState()
    {
    }
}
