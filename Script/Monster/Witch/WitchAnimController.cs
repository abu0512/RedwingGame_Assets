using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimController : MonoBehaviour
{
    private WitchBoss _witch;

    [SerializeField]
    private GameObject _trailRenderer;

    private void Awake()
    {
        _witch = transform.parent.GetComponent<WitchBoss>();
    }

    private void BeginAttack()
    {
        _witch.Collider.Collider.enabled = false;
    }

    private void OnTrail()
    {
        _trailRenderer.SetActive(true);
    }

    private void EndTrail()
    {
        _trailRenderer.SetActive(false);
    }

    private void OnAttack()
    {
        _witch.Collider.Collider.enabled = true;
        //if (_witch.DistanceCheck(_witch.Stat.AttackDistance))
        //{
        //    _witch.Target.PlayerHp(0.2f, 2, 10.0f);
        //    if (_witch.Target._PlayerShild._isShildCounter)
        //    {
        //        _witch.Target._PlayerShild.isCounterTimer = true;
        //        _witch.SetState(WitchState.GuardAttack);
        //    }
        //}
    }

    private void EndAttackTime()
    {
        _witch.Collider.Collider.enabled = false;
    }

    private void EndAttack()
    {
        _witch.EndAttack();
    }

    private void TeleportPoint()
    {
        if (!_witch.TelAttack)
            return;

        _witch.CloseTelCheck = true;
        _witch.Anim.speed = 0.0f;
    }

    private void OnRelease()
    {
        if (_witch.State == WitchState.GroggyRelease)
        {
            ((WitchStateGroggyRelease)_witch.StateSystem.CurrentState).OnRelease();
        }
        else if (_witch.State == WitchState.AttackRelease)
        {
            ((WitchStateAttackRelease)_witch.StateSystem.CurrentState).OnRelease();
        }
    }

    private void EndGuardAttack()
    {
        if (_witch.State != WitchState.GuardAttack)
            return;

        ((WitchStateGuardAttack)_witch.StateSystem.CurrentState).EndAnimation();
    }

    private void EndReleaseAnim()
    {
        if (_witch.State == WitchState.GroggyRelease)
        {
            ((WitchStateGroggyRelease)_witch.StateSystem.CurrentState).EndReleaseAnim();
        }
        else if (_witch.State == WitchState.AttackRelease)
        {
            ((WitchStateAttackRelease)_witch.StateSystem.CurrentState).EndReleaseAnim();
        }
    }
}
