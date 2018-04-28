using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalObject : MonoBehaviour
{
    [SerializeField]
    private Camera _viewCamera;

    [SerializeField]
    private GameObject _destroyEffect;
    [SerializeField]
    private GameObject _beamReady;
    [SerializeField]
    private GameObject _destroyAfterEffect;

    [SerializeField]
    private GameObject _beam;
    [SerializeField]
    private GameObject _beamObejct;

    private int _state;
    private bool _isCrash;

    // properties
    public Camera ViewCamera { get { return _viewCamera; } }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (_isCrash)
        {

        }
	}

    private void KeyInput()
    {
        if (!_isCrash)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            CPlayerManager._instance._PlayerAni_Contorl.InteractionOn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _isCrash = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _isCrash = false;
    }
}
