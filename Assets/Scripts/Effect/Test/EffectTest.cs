using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public class EffectTest : MonoBehaviour
    {
        public EffectType effectType;

        public void EffectOn()
        {
            EffectManager.Instance.SetEffect(effectType, transform.position);
        }
    }

}