using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteSStunBullet : MonoBehaviour {

    private List<EliteShamanAttack2> _EliteS = new List<EliteShamanAttack2>();
    protected EliteShaman _EliteShaman;
    protected Vector3 _direction;
    private Vector3 _target;
    private Transform _from;

    [SerializeField]
    protected float _speed;

    private float DeleteTime;

    void Start()
    {
        DeleteTime = 0;
        foreach (EliteShamanAttack2 eliteshaman in FindObjectsOfType<EliteShamanAttack2>())
        {
            _EliteS.Add(eliteshaman);
        }
    }

    void Update()
    {
        DeleteTime += Time.deltaTime;
        foreach (EliteShamanAttack2 eliteshaman in _EliteS)
        {
            _from = eliteshaman._from.transform;
        }

        transform.Translate(_from.forward * _speed * Time.deltaTime);

        if (DeleteTime >= 5f)
        {
            gameObject.SetActive(false);
            DeleteTime = 0;
        }
    }

    public void InitEliteSStunBullet(EliteShaman eliteshaman, Vector3 from, float rotate)
    {
        _EliteShaman = eliteshaman;
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
                CPlayerManager._instance.PlayerHp(0.2f, 1, _EliteShaman.AttackDamage);
            }
            else if (other.tag == "Shild")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, _EliteShaman.AttackDamage);
            }
            gameObject.SetActive(false);
        }
    }
}
