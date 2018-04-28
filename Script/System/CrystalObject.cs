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
    private GameObject _beam;
    // properties
    public Camera ViewCamera { get { return _viewCamera; } }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
