using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct EffectInfo
{
    public string Name;
    public GameObject Effect;
}

public enum EffectType
{
    Hero_Tanker_Attack1,
    Hero_Tanker_Attack2,
    Hero_Tanker_Attack3,
}

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager I { get { return _instance; } }

    [SerializeField]
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

    public GameObject OnEffect(EffectType type, Transform target, Quaternion rotation, float deadTime = 0.0f)
    {
        return OnEffect(type, target.position, rotation, deadTime);
    }

    public GameObject OnEffect(EffectType type, Vector3 target, Quaternion rotation, float deadTime = 0.0f)
    {
        GameObject effect = GetEffect(type);

        if (effect == null)
        {
            Debug.Log("해당 이펙트는 존재하지 않습니다. typename = " + type.ToString());
            return null;
        }

        effect.SetActive(true);
        effect.transform.position = target;
        effect.transform.rotation = rotation;

        if (deadTime > 0.0f)
        {
            //StartCoroutine(Co_EffectDead(effect, deadTime));
        }

        return effect;
    }

    public GameObject GetEffect(EffectType type)
    {
        foreach (GameObject info in _effectPool[type])
        {
            if (!info.activeSelf)
                return info;
        }
        return CreateEffect(type);
    }

    private GameObject CreateEffect(EffectType type)
    {
        foreach (EffectInfo info in _effectInfos)
        {
            if (info.Name == type.ToString())
            {
                GameObject effect = Instantiate(info.Effect);
                effect.transform.parent = transform;
                effect.SetActive(false);
                _effectPool[type].Add(info.Effect);

                return effect;
            }
        }

        Debug.LogError("해당하는 이펙트가 없습니다. 이펙트가 제대로 들어갔는지 확인 하시고 이름을 Enum과 똑같이 설정해주세요. typename = " + type.ToString());

        return null;
    }

    private IEnumerator Co_EffectDead(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
