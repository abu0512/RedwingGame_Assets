using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
<<<<<<< HEAD

public struct EffectInfo
{
    public string Name;
=======
public struct EffectInfo
{
    public EffectType Name;
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
    public GameObject Effect;
}

public enum EffectType
{
    Hero_Tanker_Attack1,
    Hero_Tanker_Attack2,
    Hero_Tanker_Attack3,
<<<<<<< HEAD
}

=======
    Hero_Tanker_Attack4,
    Hero_Tanker_Attack5,
    Hero_Tanker_CounterAttack,
}

[Serializable]
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager I { get { return _instance; } }

<<<<<<< HEAD
    [SerializeField]
=======
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
    public EffectInfo[] _effectInfos;
    private Dictionary<EffectType, List<GameObject>> _effectPool = new Dictionary<EffectType, List<GameObject>>();

    private void Awake()
    {
        _instance = this;

        EffectType[] types = Enum.GetValues(typeof(EffectType)) as EffectType[];

        foreach (EffectType type in types)
            _effectPool[type] = new List<GameObject>();
    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    OnEffect(EffectType.Hero_Tanker_Attack1, CPlayerManager._instance.transform, CPlayerManager._instance.transform.rotation, 5.0f);
        //}
    }

<<<<<<< HEAD
    public GameObject OnEffect(EffectType type, Transform target, Quaternion rotation, float deadTime = 0.0f)
    {
        return OnEffect(type, target.position, rotation, deadTime);
    }

    public GameObject OnEffect(EffectType type, Vector3 target, Quaternion rotation, float deadTime = 0.0f)
=======
    public GameObject OnEffect(EffectType type, Transform target, Quaternion rotation, float deadTime = 0.0f, int posType = 0)
    {
        return OnEffect(type, target.position, rotation, deadTime, posType);
    }

    public GameObject OnEffect(EffectType type, Vector3 target, Quaternion rotation, float deadTime = 0.0f, int posType = 0)
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
    {
        GameObject effect = GetEffect(type);

        if (effect == null)
        {
            Debug.Log("해당 이펙트는 존재하지 않습니다. typename = " + type.ToString());
            return null;
        }

<<<<<<< HEAD
        effect.SetActive(true);
        effect.transform.position = target;
        effect.transform.rotation = rotation;

        if (deadTime > 0.0f)
        {
            //StartCoroutine(Co_EffectDead(effect, deadTime));
=======
        if (posType == 1)
            effect.transform.position = target;
        else
            effect.transform.position += target;

        if (posType == 1)
            effect.transform.rotation = rotation;
        else
        {
            Vector3 angle = effect.transform.rotation.eulerAngles;
            angle += rotation.eulerAngles;
            rotation.eulerAngles = angle;
            effect.transform.rotation = rotation;
        }

        if (deadTime > 0.0f)
        {
            StartCoroutine(Co_EffectDead(effect, deadTime));
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
        }

        return effect;
    }

    public GameObject GetEffect(EffectType type)
    {
        foreach (GameObject info in _effectPool[type])
        {
            if (!info.activeSelf)
<<<<<<< HEAD
                return info;
=======
            {
                info.SetActive(true);
                return info;
            }
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
        }
        return CreateEffect(type);
    }

    private GameObject CreateEffect(EffectType type)
    {
        foreach (EffectInfo info in _effectInfos)
        {
<<<<<<< HEAD
            if (info.Name == type.ToString())
            {
                GameObject effect = Instantiate(info.Effect);
                effect.transform.parent = transform;
                effect.SetActive(false);
=======
            if (info.Name == type)
            {
                GameObject effect = Instantiate(info.Effect);
                effect.transform.parent = transform;
                //   effect.SetActive(false);
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
                _effectPool[type].Add(info.Effect);

                return effect;
            }
        }

<<<<<<< HEAD
        Debug.LogError("해당하는 이펙트가 없습니다. 이펙트가 제대로 들어갔는지 확인 하시고 이름을 Enum과 똑같이 설정해주세요. typename = " + type.ToString());
=======
        Debug.LogError("해당하는 이펙트가 없습니다. 이펙트가 제대로 들어갔는지 확인 하세요. typename = " + type.ToString());
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc

        return null;
    }

    private IEnumerator Co_EffectDead(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
<<<<<<< HEAD
        obj.SetActive(false);
=======
        //obj.SetActive(false);
>>>>>>> 844ed75ea76bdaacde6578d0f6173704377869fc
    }
}
