using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueObject : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private GameObject _destroy;
    private float _destTime;
    private bool _dest;

    public GameObject DestroyEffect { get { return _destroy; } }

	void Start ()
    {
        _anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        DestroyUpdate();

    }

    private void DestroyUpdate()
    {
        if (!_dest)
            return;

        _destTime += Time.deltaTime;

        if (_destTime < 1.5f)
            return;

        _destroy.SetActive(false);
    }

    public void SetDestroyEffect()
    {
        _destroy.SetActive(true);
        _dest = true;
        _destTime = 0.0f;
        _anim.SetBool("On", true);
    }
}
