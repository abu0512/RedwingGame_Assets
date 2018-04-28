using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueObject : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private GameObject _destroy;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _center;
    private float _destTime;
    private bool _dest;
    private bool _laserCrash;

    public GameObject DestroyEffect { get { return _destroy; } }

	void Start ()
    {
        _anim = GetComponent<Animator>();
        _laser.SetActive(false);
    }
	
	void Update ()
    {
        DestroyUpdate();
    }

    private void DestroyUpdate()
    {
        if (_dest)
            return;

        _laser.transform.LookAt(_center.transform.position);
        Vector3 scale = _laser.transform.localScale;
        scale.z += 0.1f;
        _laser.transform.localScale = scale;
        print("AAAAAA");
    }

    public void SetDestroyEffect()
    {
        _destroy.SetActive(true);
        StartCoroutine(Co_EffectOff());
        _dest = true;
        _anim.SetBool("On", true);
    }

    IEnumerator Co_EffectOff()
    {
        yield return new WaitForSeconds(1.5f);
        _destroy.SetActive(false);
        _laser.SetActive(true);
    }
}
