﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SStunBullet : MonoBehaviour
{
    private List<QueenMushroomAttack2> _queens = new List<QueenMushroomAttack2>();
    protected QueenMushroom _queenMushroom;
    protected Vector3 _direction;
    private Vector3 _target;
    private Transform _from;

    [SerializeField]
    protected float _speed;

    private float DeleteTime;

    void Start()
    {
        DeleteTime = 0;
        foreach (QueenMushroomAttack2 queen in FindObjectsOfType<QueenMushroomAttack2>())
        {
            _queens.Add(queen);
        }
    }

    void Update()
    {
        DeleteTime += Time.deltaTime;
        foreach (QueenMushroomAttack2 queen in _queens)
        {
            _from = queen._from.transform;
        }

        transform.Translate(_from.forward * _speed * Time.deltaTime);

        if (DeleteTime >= 5f)
        {
            gameObject.SetActive(false);
            DeleteTime = 0;
        }
    }

    public void InitSStunBullet(QueenMushroom queen, Vector3 from, float rotate)
    {
        _queenMushroom = queen;
        transform.position = from;
        transform.rotation = Quaternion.Euler(0, -45f + rotate, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MapObject" ||
              other.tag == "Player" ||
              other.tag == "Shild")
        {
            if (other.tag == "Player")
            {
                CPlayerSturn._instance.isSturn = true;
                CPlayerManager._instance.PlayerHp(0.2f, 1, _queenMushroom.AttackDamage);
            }
            else if (other.tag == "Shild")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, _queenMushroom.AttackDamage);
            }
            gameObject.SetActive(false);
        }
    }
}