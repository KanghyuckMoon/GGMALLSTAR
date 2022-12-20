using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Effect
{

    public class EffectObject : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("effectType")] 
        private EffectType _effectType;

        private void OnDisable()
        {
            Pool.PoolManager.AddObjToPool(_effectType.ToString(), gameObject);
        }
	}

}