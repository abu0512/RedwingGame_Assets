using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABUGameManager : MonoBehaviour
{
    private static ABUGameManager _instance;
    public static ABUGameManager I
    {
        get { return _instance; }
    }


    private MonsterObjectPool _monsterPool;

    //properties
    public MonsterObjectPool MonsterPool { get { return _monsterPool; } set { _monsterPool = value; } }

    private void Awake()
    {
        _instance = this;
        _monsterPool = FindObjectOfType<MonsterObjectPool>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {

    }
}
