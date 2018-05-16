using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateGroggyRelease : WitchFSMStateBase
{
    private GameObject _release;

    public override void BeginState()
    {
        _release = GameObject.Find("WitchSkillRelease");
        _release.SetActive(false);
    }

    void Update()
    {
    }

    public override void EndState()
    {
        _release.transform.position = new Vector3(10000.0f, 0.0f, 0.0f);
    }

    public void OnRelease()
    {
        Vector3 pos = Witch.transform.position;
        pos.y = 0.0f;
        _release.transform.position = pos;
        _release.SetActive(true);
        SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Release);
    }

    public void EndReleaseAnim()
    {
        Witch.SetState(WitchState.Chase);
        return;
    }
}
