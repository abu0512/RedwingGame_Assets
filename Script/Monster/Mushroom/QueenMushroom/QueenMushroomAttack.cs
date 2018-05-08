﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueenMushroomAttack : QueenMushroomStateBase
{
    private float RandomNum;
    private float _imsitime;
    private int _bulletcount;
    private bool _stun;
    private bool _notstun;
    private bool _exitattack;

    public override void BeginState()
    {
        Dltime = 0f;
        _bulletcount = 0;
        _imsitime = 0;
        RandomNum = Random.Range(0, 9f);
    }

    public override void EndState()
    {
        base.EndState();
        _bulletcount = 0;
        _stun = false;
        _notstun = false;
        _exitattack = false;
    }

    /*
    private IEnumerator Bulletset()
    {
        for(int i = 0; i < 1; i++)
        {
            Stage1.I.BulletPool.SetBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
        }
        yield return new WaitForSeconds(0.2f);
        _bulletcount++;
    }

    private IEnumerator StunBulletset()
    {
        yield return new WaitForSeconds(0.2f);
        Stage1.I.BulletPool.SetStunBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
        _bulletcount++;
    }*/

    public void StopCor()
    {
        if (_bulletcount > 4)
        {
            //StopCoroutine(StunBulletset());
            //StopCoroutine(Bulletset());
            _exitattack = true;
        }
    }

    public void SetAttack()
    {
        StopCor();
        _imsitime += Time.deltaTime;
        if (_stun && _bulletcount < 5)
        {
            //StartCoroutine(StunBulletset());
            if (_imsitime > 0.15f)
            {
                Stage1.I.BulletPool.SetStunBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
                _bulletcount++;
                _imsitime = 0;
            }
        }

        else if(_notstun && _bulletcount < 5)
        {
            //StartCoroutine(Bulletset());
            if (_imsitime > 0.15f)
            {
                Stage1.I.BulletPool.SetBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
                _bulletcount++;
                _imsitime = 0;
            }
        }
    }

    public void StartAttack()
    {
        QueenMushroom.AttackStack++;
        if (QueenMushroom.AttackStack > 2f && RandomNum > 2f)
        {
            _stun = true;
            QueenMushroom.AttackStack = 0;
        }

        else
        {
            _notstun = true;
        }

        QueenMushroom.AttackTimer = 0f;
        Dltime = 0f;
    }

    void Update()
    {
        SetAttack();
        QueenMushroom.GoToPullPush();
        QueenMushroom.PlayerisDead();
        QueenMushroom.TurnToDestination();
        QueenMushroom.TimeToHeal();

        if (_exitattack)
        {
            if (QueenMushroom.GetDistanceFromPlayer() > QueenMushroom.MStat.AttackDistance)
            {
                QueenMushroom.SetState(QueenMushroomState.Chase);
                return;
            }

            else
            {
                QueenMushroom.SetState(QueenMushroomState.Return);
                return;
            }
        }
    }
}