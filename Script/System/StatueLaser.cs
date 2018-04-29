using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueLaser : MonoBehaviour
{
    private StatueObject _statue;

    // properties
    public StatueObject Statue { get { return _statue; } set { _statue = value; } }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "StatueObject")
            return;
        if (other.name != "StatueCenter")
            return;
        _statue.LaserCrash();
    }
}
