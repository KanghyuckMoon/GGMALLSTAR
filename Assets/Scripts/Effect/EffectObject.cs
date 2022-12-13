using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{

    public class EffectObject : MonoBehaviour
    {
        [SerializeField] private EffectType effectType;

        private void OnDisable()
        {
            Pool.PoolManager.AddObjToPool(effectType.ToString(), gameObject);
        }
	}

}