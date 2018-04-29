using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterStatue : MonoBehaviour
{
    private int _laserCnt;
    [SerializeField]
    private Camera _centerCamera;
    private float _cameraSwapTime;
    private bool _laserOn;
    private Animator _anim;

	void Start ()
    {
        _anim = GetComponent<Animator>();
        _laserCnt = 0;
        _cameraSwapTime = 0.0f;
    }

	void Update ()
    {
        CameraSwapUpdate();
	}

    private void CameraSwapUpdate()
    {
        if (_laserCnt < 4)
            return;

        if (_laserOn)
            return;

        _cameraSwapTime += Time.deltaTime;

        if (_cameraSwapTime < 1.9f)
            return;

        _centerCamera.gameObject.SetActive(true);
        _laserOn = true;
    }

    private void LaserOn()
    {
        if (!_laserOn)
            return;


    }

    public void LaserAdd()
    {
        _laserCnt++;

        if (_laserCnt >= 4)
            _anim.SetBool("On2", true);
    }
}
